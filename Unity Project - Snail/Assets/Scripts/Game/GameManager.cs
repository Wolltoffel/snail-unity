using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event EventHandler<StatData> endGame;

    private void Start()
    {
        Map.markPassableTiles(RoundManager.activePlayer().activeTile, RoundManager.activePlayer());
    }

    public static void movePlayer(Player activePlayer, Tile tile, ActionInfo actionInfo)
    {
        Tile previousTile = activePlayer.activeTile;
        activePlayer.move(tile);

        if (tile.checkSlime(activePlayer))
        {
            if (actionInfo.action != ActionInfo.Action.slide)
                actionInfo.action = ActionInfo.Action.empty;

            Tile nextSlideTile = previousTile.giveNextSlideTile(tile, activePlayer);
            if (nextSlideTile != null)
            { actionInfo.action = ActionInfo.Action.slide;
                movePlayer(activePlayer, nextSlideTile, actionInfo);
                return;
            }
        }

        if (Map.checkTiles())
        {
            RoundManager.switchTurnsEvent(actionInfo);
        }

    }

    public static void EndGame()
    {
        Player winner = giveWinner();
        Player loser = giveLooser();
        StatData stats = new StatData(RoundManager.turnCounter+1,winner.score,winner.name,loser.name,loser.score);
        StatManager.stats = stats;
        endGame?.Invoke(null, stats);
        SceneManager.LoadScene("End");
    }

    static Player giveWinner()
    {
        List<Player> players = PlayerConfig.player;
        Player winner = players[0];
        foreach (Player player in players)
        {
            if (player.score > winner.score)
                winner = player;
        }
        return winner;
    }

    static Player giveLooser()
    {

        List<Player> players = PlayerConfig.player;
        Player loser = players[0];
        foreach (Player player in players)
        {
            if (player.score < loser.score)
                loser = player;
        }
        return loser;
    }
}

