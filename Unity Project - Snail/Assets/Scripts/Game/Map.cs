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

    private void Awake()
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
       MapData activeMap= MapManager.selectedMap;
       //Load provisional Map
       int[] provisionalContents = new int[18];
       provisionalContents[4] = 64;
       provisionalContents[9] = 129;
       provisionalContents[11] = 130;
       //MapData activeMap = new MapData(new Vector2Int(6, 3), provisionalContents, "Provisional");

       int[] contents = activeMap.contents;
       size = activeMap.size;

       centerPosition = upperLeftCorner + new Vector2 (size.x/2,-size.y/2);

        Vector2 nextPosition = upperLeftCorner+new Vector2(0.5f*tileSize,-0.5f*tileSize);
        int rowCounter=0;
        int rowNumber = 1;

        for (int i = 0; i<contents.Length; i++)
        {
            //Spawn Ground and Add Tiles
            Vector3 position = new Vector3(nextPosition.x, nextPosition.y, 0);

            Tile newTile = new Tile(position, new Vector2(i - (rowNumber - 1) * size.x+1, rowNumber));

            newTile.grassField = Instantiate(ground, position,Quaternion.Euler(Vector3.zero)) as GameObject;
            newTile.grassField.name = $"Grass ({newTile.position.x}/{newTile.position.y})";
            InsertHighlight(newTile, newTile.grassField);

            switch (contents[i])
            {
                case 64://Spawn Impassable Rocks
                    newTile.impassable = Instantiate(impassable, position, Quaternion.Euler(Vector3.zero)) as  GameObject;
                    newTile.impassable.name = $"Impassable ({newTile.position.x}/{newTile.position.y})";
                    newTile.impassable.transform.parent = newTile.grassField.transform;
                    break;
                case 129: //Spawn Player 1
                    newTile.playerSlot =  PlayerConfig.player[0] == null ?new Player("Player 1"):PlayerConfig.player[0];
                    newTile.playerSlot.sprite = Instantiate(playerSprites[0], position, Quaternion.Euler(Vector3.zero)) as GameObject;
                    newTile.playerSlot.sprite.transform.parent = newTile.grassField.transform;
                    newTile.playerSlot.sprite.name = $"{newTile.playerSlot.name} Sprite";
                    newTile.playerSlot.activeTile = newTile;
                    break;
                case 130: //Spawn Player 2
                    newTile.playerSlot = PlayerConfig.player[1] == null ? new Player("Player 2") : PlayerConfig.player[1];
                    newTile.playerSlot.sprite = Instantiate(playerSprites[1], position, Quaternion.Euler(Vector3.zero)) as GameObject;
                    newTile.playerSlot.sprite.transform.parent = newTile.grassField.transform;
                    newTile.playerSlot.sprite.name = $"{newTile.playerSlot.name} Sprite";
                    newTile.playerSlot.activeTile = newTile;
                    break;
                default:
                    break;
            }

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
                    if (tile.position.x+1 == item.position.x && item.position.y == tile.position.y)
                    {
                        tile.right = item;
                        item.left = tile;
                    }
                }
            }
            //Left
            if (tile.left == null)
            {
                foreach (Tile item in tiles)
                {
                    if (tile.position.x - 1 == item.position.x && item.position.y == tile.position.y)
                    {
                        tile.left = item;
                        item.right = tile;
                    }
                }
            }
            //Up Tile / Down Item
            if (tile.up == null)
            {
                foreach (Tile item in tiles)
                {
                    if (tile.position.y+1==item.position.y && item.position.x == tile.position.x)
                    {
                        tile.up = item;
                        item.down = tile;
                    }
                }
            }
            //Down Tile / Up Item
            if (tile.down == null)
            {
                foreach (Tile item in tiles)
                {
                    if (tile.position.y - 1 == item.position.y && item.position.x == tile.position.x)
                    {
                        tile.down = item;
                        item.up = tile;
                    }
                }
            }

        }

    }

    public static void InsertHighlight(Tile tile, GameObject parent)
    {
        tile.highLightSlot = Instantiate(highlight, tile.worldPosition, Quaternion.Euler(Vector3.zero))as GameObject;
        tile.highLightSlot.name = $"Highlight ({tile.position.x}/{tile.position.y})";
        tile.highLightSlot.SetActive(false);
        tile.highLightSlot.AddComponent<BoxCollider2D>();
        tile.highLightSlot.AddComponent<HighLight>();
        tile.highLightSlot.GetComponent<HighLight>().tile = tile;
        tile.highLightSlot.transform.parent = parent.transform;
    }


}
