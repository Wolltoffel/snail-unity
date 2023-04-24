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

    private void Start()
    {
        controls = new Controls();
        Player player = RoundManager.activePlayer();
        mapBuilder = new MapBuilder();
        MapBuilder.markPassableTiles(player.activeTile, player);
    }

    private void Update()
    {
        controls.checkInputs();
    }

    public static void excuteTurn(Player activePlayer, Tile tile, ActionInfo actionInfo)
    {
        Tile previousTile = activePlayer.activeTile;

        activePlayer.move(tile, actionInfo);

        if (tile.checkSlime(activePlayer))  //Check Tile for slime
        {
            if (actionInfo.actionType != ActionType.slide)
                actionInfo.actionType = ActionType.empty;
            
            //In case an adjacent Tile lies one tile away in the same direction as the player has just moved and it has slime it will return the adjacent tile
            Tile nextSlideTile = MapBuilder.giveNextSlideTile(previousTile,tile, activePlayer);
            if (nextSlideTile != null)
            {   actionInfo.actionType = ActionType.slide;
                excuteTurn(activePlayer, nextSlideTile, actionInfo);
                return;
            }
        }
        else
        {
            tile.AddSlime();
            activePlayer.increaseScore(); //Increase score if current field has no slime
        }

        if (MapBuilder.checkTiles()) //Checks whether the game has ended
        {
            RoundManager.switchTurnsEvent(actionInfo);
        }

    }

    public static void EndGame(Player winner, Player loser)
    {
        StatData stats = new StatData(RoundManager.turnCounter+1,winner.score,winner.name,loser.name,loser.score);
        StatManager.stats = stats;
        endGame?.Invoke(null, stats);
        SceneManager.LoadScene("End");
    }

    public static Player giveWinner()
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

    public static Player giveLoser()
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

