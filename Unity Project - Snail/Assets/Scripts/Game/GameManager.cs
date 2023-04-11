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
    }

    private void  movePlayer(Player activePlayer) { 


    }

    void markPassableFields(Tile currentTile, Player player)
    {
        List<Tile> passableTiles = checkPassableFields(currentTile, player);
        foreach(Tile tile in passableTiles)
        {
            Map.PlaceHighlight(tile);
        }
    }

    private List<Tile> checkPassableFields(Tile currentTile,Player player)
    {
        List<Tile> freeTiles = new List<Tile>();
        Tile left = currentTile.left;
        Tile right = currentTile.right;
        Tile up = currentTile.up;
        Tile down = currentTile.down;

        freeTiles.Add(currentTile.left);
        freeTiles.Add(currentTile.right);
        freeTiles.Add(currentTile.up);
        freeTiles.Add(currentTile.down);

        foreach(Tile tile in freeTiles)
        {
            if (!checkField(tile, player))
                freeTiles.Remove(tile);
        }

        return freeTiles;
    }

    bool checkField(Tile tile,Player player)
    {
        if (tile.generallyPassable 
            && tile.slimeOwner==null
            && tile.playerSlot != player)
        {return true;}
        else
        {return false; }
    }

}
