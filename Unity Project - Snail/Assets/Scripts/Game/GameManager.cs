using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List <Player> player;
    MapData activeMap;
    PlayerSettings settings;
    List<Tile> tiles;

    private void Start()
    {
        player = PlayerConfig.player;
        tiles = Map.tiles;

        Map.markPassableFields(RoundManager.activePlayer().activeTile, RoundManager.activePlayer());
    }

    public static void movePlayer(Player activePlayer, Tile tile)
    {
        Tile previousTile = activePlayer.activeTile;
        activePlayer.sprite.transform.position = tile.worldPosition;
        activePlayer.activeTile.playerSlot = null;
        Map.AddSlime(activePlayer.activeTile, activePlayer);
        activePlayer.activeTile = tile;

        foreach (Tile item in Map.highlightedTiles)
        {
            item.highLightSlot.SetActive(false);
        }
        tile.playerSlot = activePlayer;
        
        if (tile.checkSlime(activePlayer))
        {
            Tile nextSlideTile = previousTile.giveNextSlideTile(tile, activePlayer);;
            if (nextSlideTile != null)
            {
                movePlayer(activePlayer, nextSlideTile);
            }
            else
            {
                endTurn(activePlayer);
            }
        }
        else
        {
            endTurn(activePlayer);
        }
    }

    static void endTurn(Player activePlayer)
    {
        RoundManager.switchTurns();
        activePlayer = RoundManager.activePlayer();
        Tile currentTile = activePlayer.activeTile;
        Map.markPassableFields(currentTile, activePlayer);
    }







}
