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
        tile.playerSlot = activePlayer;
        
        if (tile.checkSlime(activePlayer))
        {
            Tile nextSlideTile = previousTile.giveNextSlideTile(tile, activePlayer);;
            if (nextSlideTile != null)
            {
                movePlayer(activePlayer, nextSlideTile);
                return;
            }
        }
        RoundManager.switchTurns();
    }









}
