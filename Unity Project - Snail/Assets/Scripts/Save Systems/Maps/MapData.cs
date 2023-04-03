using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapData: SaveData
{
    Vector2 size;
    int []contents;

    public MapData(Vector2 size, int[]contents)
    {
        this.size = size;
        this.contents = contents;
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
