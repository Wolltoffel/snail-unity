using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour
{
    [SerializeField]Vector2 upperLeftCorner;
    static public int tileSize = 1;
    [HideInInspector]static public Vector2 centerPosition;
    [HideInInspector]static public Vector2 size;
    [HideInInspector] static public MapData activeMap;
    Object ground;
    Object impassable;
    static Object highlight;
    List <Object> playerSprites;
    public static List<Tile> tiles;

    private void Start()
    {
        tiles = new List<Tile>();
        loadSprites();
        loadMap();
        manageTiles();
    }

    void loadSprites()
    {
        ground = Resources.Load("Sprites/" + "ground");
        impassable = Resources.Load("Sprites/" + "impassable");
        highlight = Resources.Load("Sprites/" + "empty_256x256");
        playerSprites = new List<Object>();
        playerSprites.Add(Resources.Load("Sprites/" + "snail_blue_256x256"));
        playerSprites.Add(Resources.Load("Sprites/" + "snail_orange_256x256"));
    }

    void loadMap()
    {
       //MapData selected = MapManager.selectedMap;
       //Load provisional Map
       int[] provisionalContents = new int[18];
       provisionalContents[4] = 64;
       provisionalContents[9] = 129;
       provisionalContents[11] = 130;
       MapData activeMap = new MapData(new Vector2Int(6, 3), provisionalContents, "Provisional");

       int[] contents = activeMap.contents;
       size = activeMap.size;

       centerPosition = upperLeftCorner + new Vector2 (size.x/2,-size.y/2);

        Vector2 nextPosition = upperLeftCorner+new Vector2(0.5f*tileSize,-0.5f*tileSize);
        int rowCounter=0;
        int rowNumber = 1;
        bool passable = true;
        Player player = null;

        for (int i = 0; i<contents.Length; i++)
        {
            //Spawn Ground
            Vector3 position = new Vector3(nextPosition.x, nextPosition.y, 0);
            Instantiate(ground, position,Quaternion.Euler(Vector3.zero));

            switch (contents[i])
            {
                case 64:
                    Instantiate(impassable, position, Quaternion.Euler(Vector3.zero));
                    passable = false;
                    break;
                case 129: //Spawn Player 1
                    Instantiate(playerSprites[0], position, Quaternion.Euler(Vector3.zero));
                    player = PlayerConfig.player[0];
                    break;
                case 130: //Spawn Player 2
                    Instantiate(playerSprites[1], position, Quaternion.Euler(Vector3.zero));
                    player = PlayerConfig.player[1];
                    break;
                default:
                    break;
            }

            //Add Tiles
            Tile newTile = new Tile(new Vector2(i - (rowNumber - 1) * size.x, rowNumber), position, passable,player);
            tiles.Add(newTile);
            player = null;

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

    }

    void manageTiles()
    {
        foreach (Tile tile in tiles)
        {

            //Right Tile
            if (tile.right == null)
            {
                foreach (Tile item in tiles)
                {
                    if (item.position.x == tile.position.x + 1)
                    {
                        tile.right = item;
                        item.left = tile;
                    }
                }
            }

            //Left Tile
            if (tile.left == null)
            {
                foreach (Tile item in tiles)
                {
                    if (item.position.x == tile.position.x - 1)
                    {
                        tile.left = item;
                        item.right = tile;
                    }
                }
            }

            //Up Tile
            if (tile.up == null)
            {
                foreach (Tile item in tiles)
                {
                    if (item.position.y == tile.position.y + 1)
                    {
                        tile.up = item;
                        item.down = tile;
                    }
                }
            }

            //Down Tile
            if (tile.down == null)
            {
                foreach (Tile item in tiles)
                {
                    if (item.position.y == tile.position.y - 1)
                    {
                        tile.down = item;
                        item.up = tile;
                    }
                }
            }

        }

    }

    public static void PlaceHighlight(Tile tile)
    {
        tile.highLightSlot = Instantiate(highlight, tile.worldPosition, Quaternion.Euler(Vector3.zero))as GameObject;
        tile.highLightSlot.name = $"Highlight x {tile.position.x} {tile.position.y}";
    }
}
