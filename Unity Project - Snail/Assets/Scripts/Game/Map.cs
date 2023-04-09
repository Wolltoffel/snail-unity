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

    private void Awake()
    {
        ground = Resources.Load("Sprites/" + "ground");
        impassable = Resources.Load("Sprites/" + "impassable");
        loadMap();
    }

    void loadMap()
    {
       //MapData selected = MapManager.selectedMap;
       //Load provisional Map
       int[] provisionalContents = new int[9];
       provisionalContents[4] = 64;
       MapData activeMap = new MapData(new Vector2Int(6, 6), provisionalContents, "Provisional");

       int[] contents = activeMap.contents;
       size = activeMap.size;

       centerPosition = upperLeftCorner + new Vector2 (size.x/2,-size.y/2);

        Vector2 nextPosition = upperLeftCorner+new Vector2(0.5f*tileSize,-0.5f*tileSize);
        int rowCounter=0;

        for (int i = 0; i<contents.Length; i++)
        {
            //Spawn Ground
            Instantiate(ground, new Vector3(nextPosition.x, nextPosition.y, 0),Quaternion.Euler(Vector3.zero));
            nextPosition.x++;
            rowCounter++;
            //Spawn Impassable
            if (contents[i] == 64)
            {
                Instantiate(impassable, new Vector3(nextPosition.x, nextPosition.y, 0), Quaternion.Euler(Vector3.zero));
            }
            //Switch Rows
            if (rowCounter >= size.x) {
                nextPosition.y--;
                nextPosition.x = upperLeftCorner.x+0.5f*tileSize;
                rowCounter = 0;
            }
        }

    }


}
