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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            markPassableFields(tiles[0], player[0]);
        }
    }

    private void  movePlayer(Player activePlayer) { 


    }

    public void markPassableFields(Tile currentTile, Player player)
    {
        List<Tile> passableTiles = checkPassableTiles(currentTile, player);
        foreach(Tile tile in passableTiles)
        {
            tile.setHighlight(true);
        }
    }

    private  List<Tile> checkPassableTiles(Tile currentTile,Player player)
    {
        List<Tile> proxomityTiles = new List<Tile>();
        List<Tile> freeTiles = new List<Tile>();


        proxomityTiles.Add(currentTile.left);
        proxomityTiles.Add(currentTile.right);
        proxomityTiles.Add(currentTile.up);
        proxomityTiles.Add(currentTile.down);
       
        foreach(Tile tile in proxomityTiles)
        {
            if (tile.checkTile(player))
                freeTiles.Add(tile);
 
        }

        return freeTiles;
    }



}
