using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MapBuilder
{
    [SerializeField]Vector2 upperLeftCorner;
    static public int tileSize = 1;
    static public Vector2 centerPosition;
    static public Vector2 size;
    private MapData activeMap;
    
    static List<Tile> highlightedTiles;
    static List<Tile> tiles;
    public static Tile[,] arrayTiles;

    ActivePlayerGiver activePlayerGiver;

    public MapBuilder()
    {
        RoundManager.switchTurn += switchRounds;
        GameManager.endGame += emptyMap;
        tiles = new List<Tile>();
        activePlayerGiver = new ActivePlayerGiver();
        loadMap();
        calculateCenterPosition();
    }

    void loadMap()
    {
       MapData activeMap= MapManager.selectedMap;
       int[] contents = activeMap.contents;
       
       size = activeMap.size;
       Vector2Int sizeToInt = new Vector2Int((int)size.x, (int)size.y);
       arrayTiles = new Tile[sizeToInt.x, sizeToInt.y];

        Vector2 nextPosition = upperLeftCorner+new Vector2(0.5f*tileSize,-0.5f*tileSize);
        int rowIndex=0;
        int index=0;

        for (int y = 0; y<size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                //Add Tiles
                Vector3 worldPosition = new Vector3(nextPosition.x, nextPosition.y, 0);
                int xPosition = x;
                xPosition = Mathf.RoundToInt(xPosition % size.x - 1) + 1;
                Vector2Int position = new Vector2Int(xPosition, rowIndex);

                Tile newTile = new Tile(worldPosition, position);
                newTile.insertContent(contents[index]);
                index++;

                //Add Tiles to List
                tiles.Add(newTile);
                arrayTiles[position.x, position.y] = newTile;
                nextPosition.x++;
            }
            nextPosition.y--;
            nextPosition.x = upperLeftCorner.x + 0.5f * tileSize;
            rowIndex++;
        }
    }

    public void markPassableTiles(Tile currentTile, Player player)
    {
        List<Tile> passableTiles = givePassableTiles(currentTile, player);
        highlightedTiles = passableTiles;
        foreach (Tile tile in passableTiles)
        {
            tile.setHighlight(true);
        }
    }

    List<Tile> givePassableTiles(Tile currentTile, Player player)
    {
        List<Tile> neighbourTiles = giveNeighbours(currentTile);
        List<Tile> freeTiles = new List<Tile>();
     
        foreach (Tile tile in neighbourTiles)
        {
            if (tile != null)
            {
                if (tile.checkPassable(player))
                {
                    Tile toBeAdded = giveNextSlideTile(player.activeTile, tile, player);
                    if (tile != toBeAdded)
                    {
                        toBeAdded.highLightSlot.GetComponent<HighLight>().switchToSlideMode();
                    }
                    freeTiles.Add(toBeAdded);
                }
            }
        }
        return freeTiles;
    }



    static List<Tile> giveNeighbours(Tile currentTile)
    {
        List<Tile> neighbourTiles = new List<Tile>();

        int x = currentTile.position.x;
        int y = currentTile.position.y;

        //Left Neighbour
        if (x - 1 >= 0)
            neighbourTiles.Add(arrayTiles[x - 1, y]);
        else
            neighbourTiles.Add(null);
        //Right Neighbour
        if (x + 1 < size.x)
            neighbourTiles.Add(arrayTiles[x + 1, y]);
        else
            neighbourTiles.Add(null);
        //Top Neighbour
        if (y - 1 >= 0)
            neighbourTiles.Add(arrayTiles[x, y - 1]);
        else
            neighbourTiles.Add(null);
        //Bottom Neighbour
        if (y + 1 < size.y)
        {
            neighbourTiles.Add(arrayTiles[x, y + 1]);
        }
        else
            neighbourTiles.Add(null);

        return neighbourTiles;
    }

    public static void unmarkTiles()
    {
        foreach (Tile tile in highlightedTiles)
        {
            tile.setHighlight(false);
        }
    }

    public static Tile giveNextSlideTile(Tile previousTile, Tile activeTile, Player player)
    {
        if (activeTile.checkSlime(player))
        {
          Tile[] previousTileNeighbours= giveNeighbours(previousTile).ToArray();
          Tile[] activeTileNeighbours= giveNeighbours(activeTile).ToArray();

            for (int i = 0; i < previousTileNeighbours.Length; i++)
            {
                if (previousTileNeighbours[i] == activeTile && activeTileNeighbours[i] != null)
                {
                    if (activeTileNeighbours[i].checkSlime(player))
                        return giveNextSlideTile(activeTile, activeTileNeighbours[i], player);
                }
            }
        }
        return activeTile;
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
 
        Player activePlayer = activePlayerGiver.giveActivePlayer();
        Tile currentTile = activePlayer.activeTile;
        markPassableTiles(currentTile, activePlayer);
    }

    public static bool checkForAllTilesOccupied()
    {
        foreach (Tile tile in tiles)
        {
            if (tile.slime==null && tile.impassableSlot==null) {
                return false;
            }
        }
        return true;
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
