using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Represents the data of a map.
/// </summary>
public class MapData: SaveData
{
    public Vector2Int size;
    public int []contents;
    public string name;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapData"/> class.
    /// </summary>
    /// <param name="size">The size of the map.</param>
    /// <param name="contents">The contents of the map.</param>
    /// <param name="name">The name of the map.</param>
    public MapData(Vector2Int size, int[]contents, string name)
    {
        this.size = size;
        this.contents = contents;
        this.name = name;
    }

    /// <summary>
    /// Loads the default values for the map.
    /// </summary>
    public override void LoadDefaultValues()
    {
        // Define the path to the default map JSON file
        string path = Application.streamingAssetsPath + "/map/default.json";

        // Read the contents of the JSON file
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        // Deserialize the JSON into a MapData object
        MapData defaultMap = JsonUtility.FromJson<MapData>(json);
        size = defaultMap.size;
        contents = defaultMap.contents;

    }
}
