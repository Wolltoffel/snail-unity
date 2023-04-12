using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List <Player> player;
    MapData activeMap;
    PlayerSettings settings;
    List<Tile> tiles;
    static List<Tile> highlightedTiles;

    private void Start()
    {
        player = PlayerConfig.player;
        tiles = Map.tiles;
    }

    private void Update()
    {
        markPassableFields(RoundManager.activePlayer().activeTile,RoundManager.activePlayer());
    }

    public static  void movePlayer(Player activePlayer, Tile tile) {


        if (Vector2.Distance(activePlayer.sprite.transform.position, tile.worldPosition) < 0.0001f)
        {
            activePlayer.activeTile = tile;
            activePlayer.sprite.transform.Translate(tile.worldPosition - activePlayer.sprite.transform.position * Time.deltaTime * 10);
        }

        foreach (Tile item in highlightedTiles)
        {
            item.highLightSlot.SetActive(false);
        }
    

    }

    void markPassableFields(Tile currentTile, Player player)
    {
        List<Tile> passableTiles = checkPassableTiles(currentTile, player);
        highlightedTiles = passableTiles;
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
            if (tile != null){
                if (tile.checkTile(player))
                    freeTiles.Add(tile);
            }
        }
        return freeTiles;
    }



}
