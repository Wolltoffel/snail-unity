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
    List <Object> playerSprites;
    List<Tile> tiles;

    private void Awake()
    {
        tiles = new List<Tile>();
        loadSprites();
        loadMap();
    }

    void loadSprites()
    {
        ground = Resources.Load("Sprites/" + "ground");
        impassable = Resources.Load("Sprites/" + "impassable");
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
                    break;
                case 130: //Spawn Player 2
                    Instantiate(playerSprites[1], position, Quaternion.Euler(Vector3.zero));
                    break;
                default:
                    break;
            }

            //Add Tiles
            Tile newTile = new Tile(new Vector2(i - (rowNumber - 1) * size.x, rowNumber), position, passable);
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


}
