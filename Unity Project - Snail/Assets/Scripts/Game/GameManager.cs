using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
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

            Tile nextSlideTile = previousTile.giveNextSlideTile(tile, activePlayer);;
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
        Debug.Log("End Game");
    }










}
