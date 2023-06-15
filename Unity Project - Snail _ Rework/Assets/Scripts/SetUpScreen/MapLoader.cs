using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

/// <summary>
/// Handles loading and validation of map data.
/// </summary>
public class MapLoader
{
    SaveSystem saver;
    string savePath;
    public MapLoader()
    {
        saver = new SaveSystem();
        savePath = Application.streamingAssetsPath + "/maps";
    }

    /// <summary>
    /// Loads the map data from saved files.
    /// </summary>
    /// <returns>A list of loaded map data.</returns>
    public List<MapData> LoadMaps()
    {
        string[] filePaths = System.IO.Directory.GetFiles(savePath, "*.json");
        List<MapData> maps = new List<MapData>();
        MapData[] mapArray = new MapData[filePaths.Length];

        for (int i = 0; i < filePaths.Length; i++)
        {
            mapArray[i] = saver.LoadData<MapData>(filePaths[i]);

            if (mapArray[i] != null)
            {
                mapArray[i].name = Path.GetFileNameWithoutExtension(filePaths[i]);
                maps.Add(mapArray[i]);
            }
        }
        
        maps = maps.OrderBy(map => map.name).ToList<MapData>();

        return maps;
    }

    /// <summary>
    /// Checks the validity of a map data.
    /// </summary>
    /// <param name="mapData">The map data to check.</param>
    /// <returns>A string indicating the validity of the map data.</returns>
    public string CheckMapValidity(MapData mapData)
    {
        int mapFieldCount = mapData.size.x * mapData.size.y;
        PlayerSettings playersettings = GameData.playersettings;
        string mapName = $"<b>{mapData.name}</b>";

        if (mapData.contents.Length != mapFieldCount)
            return $"{mapName}'s tiles are exceeding or receding the specified mapFieldCount.";
        if (playersettings.requireSquareMap && mapData.size.x != mapData.size.y)
            return $"{mapName}is not square.";
        if (mapData.size.x > playersettings.maxMapSize |
            mapData.size.y > playersettings.maxMapSize |
            mapData.size.x < playersettings.minMapSize |
            mapData.size.y < playersettings.minMapSize)
            return $"{mapName} does not fulfill the required size requirements.";
        for (int i = 0; i < mapData.contents.Length; i++)
        {
            if (mapData.contents[i] != 129 && mapData.contents[i] != 130 && mapData.contents[i] != 64 && mapData.contents[i] != 0)
                return $"{mapName} is using unkwon numbers. Pls update your map to only include the numbers: 129, 130, 64 and 0.";
        }
        
        if (!CheckPlayerValidity(mapData.contents))
            return $"{mapName} has too many /too few players or is missing one player";

        return "Valid";
    }

    /// <summary>
    /// Checks the validity of player positions in the map data.
    /// </summary>
    /// <param name="contents">The map contents.</param>
    /// <returns>True if the players are correctly set up; otherwise, false.</returns>
    bool CheckPlayerValidity(int[] contents)
    {
        int playerOneCount = 0;
        int playerTwoCount = 0;

        for (int i = 0; i < contents.Length; i++)
        {
            if (contents[i] == 129)
                playerOneCount++;

            if (contents[i] == 130)
                playerTwoCount++;
        }

        if (playerOneCount==1 && playerTwoCount==1)
            return true;
        return false;
    }

}
