using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapData: SaveData
{
    public Vector2Int size;
    public int []contents;
    public string name;

    public MapData(Vector2Int size, int[]contents, string name)
    {
        this.size = size;
        this.contents = contents;
        this.name = name;
    }

    public override void loadDefaultValues()
    {
        string path = Application.streamingAssetsPath + "/map/default.json";

        StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        MapData defaultMap = JsonUtility.FromJson<MapData>(json);
        size = defaultMap.size;
        contents = defaultMap.contents;

        Debug.Log("Loading Default Map");
        Debug.Log(json);
    }



  
}
