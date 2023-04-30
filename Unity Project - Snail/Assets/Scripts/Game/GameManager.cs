using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event EventHandler<StatData> endGame;
    private Controls controls;
    private MapBuilder mapBuilder;
    private static ActivePlayerGiver activePlayerGiver;

    private void Start()
    {
        controls = new Controls();
        activePlayerGiver = new ActivePlayerGiver();
        Player player = activePlayerGiver.giveActivePlayer();
        mapBuilder = new MapBuilder();
        mapBuilder.markPassableTiles(player.activeTile, player);
    }

    private void Update()
    {
        controls.checkKeyInputs();
    }

    public static void tryExecuteTurn(Tile tile, ActionInfo actionInfo)
    {
        if (actionInfo.player != activePlayerGiver.giveActivePlayer())
            return;
        ExecuteTurn(tile, actionInfo);
    }

    static void ExecuteTurn(Tile tile, ActionInfo actionInfo)
    {

        switch (actionInfo.actionType)
        {
            case ActionType.skip:
                RoundManager.skipRound();
                RoundManager.switchTurnsEvent(actionInfo);
                break;
            case ActionType.surrender:
                Player loser = actionInfo.player;
                Player winner = RoundManager.inactivePlayer();
                GameManager.endGameplay(winner, loser, ResultInfo.surrender);
                break;
            default:
                movePlayer(actionInfo.player, tile, actionInfo);
                MapBuilder.unmarkTiles();
                break;
        }

        checkForAllOccupiedTiles();
    }

    static void checkForAllOccupiedTiles()
    {
        if (MapBuilder.checkForAllTilesOccupied())
            endGameplay(giveWinnerByScore(), giveLoserByScore(), ResultInfo.score);
    }


    static void movePlayer(Player activePlayer, Tile tile, ActionInfo actionInfo)
    {
        Tile previousTile = activePlayer.activeTile;

        if (actionInfo.actionType != ActionType.slide)
        {
            if (tile.checkSlime(activePlayer))  //Check Tile for slime
                actionInfo.actionType = ActionType.empty;
            else
                activePlayer.captureField(tile);
        }
       
        activePlayer.move(tile, actionInfo);

    }

    public static void endGameplay(Player winner, Player loser, ResultInfo resultInfo)
    {
        StatData stats = new StatData(RoundManager.turnCounter+1,winner.score,winner.name,loser.name,loser.score);
        StatManager.stats = stats;
        StatManager.resultInfo = resultInfo;
        endGame?.Invoke(null, stats);
        SceneManager.LoadScene("End");
    }

    public static Player giveWinnerByScore()
    {
        List<Player> players = PlayerManager.player;
        Player winner = players[0];
        foreach (Player player in players)
        {
            if (player.score > winner.score)
                winner = player;
        }
        return winner;
    }

    public static Player giveLoserByScore()
    {
        List<Player> players = PlayerManager.player;
        Player loser = players[0];
        foreach (Player player in players)
        {
            if (player.score < loser.score)
                loser = player;
        }
        return loser;
    }
}

