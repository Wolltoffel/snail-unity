using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event EventHandler<StatData> endGame;
    public static event EventHandler <ActionType>endRound;
    private Controls controls;
    private MapBuilder mapBuilder;

    private void Start()
    {
        controls = new Controls();
        Player player = RoundManager.activePlayer();
        mapBuilder = new MapBuilder();
        mapBuilder.markPassableTiles(player.activeTile, player);
    }

    private void Update()
    {
        controls.checkInputs();
    }

    public static void excuteTurn(Player activePlayer, Tile tile, ActionInfo actionInfo)
    {
        if (actionInfo.actionType == ActionType.skip)
        {
            RoundManager.switchTurnsEvent(actionInfo);
            return;
        }

        else
        {
            movePlayer(activePlayer,tile,actionInfo);
            MapBuilder.unmarkTiles();
        }
    }

    static void movePlayer(Player activePlayer, Tile tile, ActionInfo actionInfo)
    {
        Tile previousTile = activePlayer.activeTile;

        if (tile.checkSlime(activePlayer))  //Check Tile for slime
        {
            //Override tile in case of possible slide
            Tile tileBackup = tile;
            tile = MapBuilder.giveNextSlideTile(previousTile, tile, activePlayer);
            if (tile != tileBackup)
                actionInfo.actionType = ActionType.slide;
            else
            {
                tile = tileBackup;
                actionInfo.actionType = ActionType.empty;
            }
        }

        //Capture Field
        else
            activePlayer.captureField(tile);

        activePlayer.move(tile, actionInfo);

    }


    public static void EndGame(Player winner, Player loser)
    {
        StatData stats = new StatData(RoundManager.turnCounter+1,winner.score,winner.name,loser.name,loser.score);
        StatManager.stats = stats;
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

