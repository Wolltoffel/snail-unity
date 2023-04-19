using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MapBuilder : MonoBehaviour
{
    [SerializeField]Vector2 upperLeftCorner;
    static public int tileSize = 1;
    static public Vector2 centerPosition;
    static public Vector2 size;
    private MapData activeMap;
    
    static List<Tile> highlightedTiles;
    static List<Tile> tiles;


    private void Awake()
    {
        RoundManager.switchTurn += switchRounds;
        GameManager.endGame += emptyMap;
        tiles = new List<Tile>();
        loadMap();
    }

    void loadMap()
    {
       MapData activeMap= MapManager.selectedMap;
       int[] contents = activeMap.contents;
       size = activeMap.size;

        Vector2 nextPosition = upperLeftCorner+new Vector2(0.5f*tileSize,-0.5f*tileSize);
        int rowCounter=0;
        int rowNumber = 1;

        for (int i = 0; i<contents.Length; i++)
        {
            //Add Tiles
            Vector3 worldPosition = new Vector3(nextPosition.x, nextPosition.y, 0);
            Tile newTile = new Tile(worldPosition, new Vector2(i - (rowNumber - 1) * size.x+1, rowNumber));
            newTile.insertContent(contents[i]);

            //Add Tiles to List
            tiles.Add(newTile);

            nextPosition.x++;
            rowCounter++;

            //Switch Rows
            if (rowCounter >= size.x) {
                nextPosition.y--;
                nextPosition.x = upperLeftCorner.x+0.5f*tileSize;
                rowCounter = 0;
                rowNumber++;
            }
        }

        manageTiles();
        calculateCenterPosition();
    }


    void manageTiles()
    {
        foreach (Tile tile in tiles)
        {
            foreach (Tile item in tiles)
            {
                Vector2 right = tile.position + Vector2.right;
                Vector2 left = tile.position + Vector2.left;
                Vector2 down = tile.position + Vector2.down;
                Vector2 up = tile.position + Vector2.up;

                if (tile.right == null && item.position == right)
                {
                    tile.right = item;
                    item.left = tile;
                }
                else if (tile.left == null && item.position == left)
                {
                    tile.left = item;
                    item.right = tile;
                }
                else if (tile.down == null && item.position == down)
                {
                    tile.down = item;
                    item.up = tile;
                }
                else if (tile.up == null && item.position == up)
                {
                    tile.up = item;
                    item.down = tile;
                }
            }
        }

    }
    public static void markPassableTiles(Tile currentTile, Player player)
    {
        List<Tile> passableTiles = checkPassableTiles(currentTile, player);
        highlightedTiles = passableTiles;
        foreach (Tile tile in passableTiles)
        {
            tile.setHighlight(true);
        }
    }

    private static List<Tile> checkPassableTiles(Tile currentTile, Player player)
    {
        List<Tile> proxomityTiles = new List<Tile>();
        List<Tile> freeTiles = new List<Tile>();

        if(currentTile.left!=null)
            proxomityTiles.Add(currentTile.left);
        if (currentTile.right != null)
            proxomityTiles.Add(currentTile.right);
        if (currentTile.up != null)
            proxomityTiles.Add(currentTile.up);
        if (currentTile.down != null)
            proxomityTiles.Add(currentTile.down);

        foreach (Tile tile in proxomityTiles)
        {
            if (tile != null)
            {
                if (tile.checkPassable(player))
                    freeTiles.Add(tile);
            }
        }
        return freeTiles;
    }

    void calculateCenterPosition()
    {
        centerPosition = upperLeftCorner + new Vector2(size.x / 2, -size.y / 2);
    }


    void switchRounds(object sender, EventArgs e)
    {
        foreach (Tile item in highlightedTiles)
        {
            item.highLightSlot.SetActive(false);
        }
 
            Player activePlayer = RoundManager.activePlayer();
            Tile currentTile = activePlayer.activeTile;
            markPassableTiles(currentTile, activePlayer);
    }


    public static bool checkTiles()
    {
        if (highlightedTiles.Count < 1) {
            GameManager.EndGame(GameManager.giveWinner(),GameManager.giveLoser());
            return false;
        }

        foreach (Tile tile in tiles)
        {
            if (tile.slime==null && tile.impassableSlot==null) {
                return true;
            }
        }
        GameManager.EndGame(GameManager.giveWinner(),GameManager.giveLoser());
        return false;

    }

    void emptyMap(object sender, EventArgs e)
    {
        foreach (Tile tile in tiles)
        {
            tile.emptyTile();
        }

        RoundManager.switchTurn -= switchRounds;
        GameManager.endGame -= emptyMap;

        tiles.Clear();
    }
}
