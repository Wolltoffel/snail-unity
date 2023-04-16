using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
            {   actionInfo.action = ActionInfo.Action.slide;
                movePlayer(activePlayer, nextSlideTile,actionInfo);
                return;
            }
        }
        
        RoundManager.switchTurnsEvent(actionInfo);
    }

    public static void EndGame()
    {
        /*StatData stats = new StatData(RoundManager.roundCounter,);
        endGame.Invoke(null,stats);*/
    }

    //static Player winner()
    //{
       /* List<Player> players = PlayerConfig.player;
        Player winner = players[0];
        for(int i=0;i<players.Capacity;i++)
        {
            if (players[i].score>players[i+1].score)

        }*/
    }

