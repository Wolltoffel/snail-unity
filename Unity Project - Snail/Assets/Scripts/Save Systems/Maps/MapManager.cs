using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class MapManager : MonoBehaviour
{
    public List<MapData> maps;
    SaveSystem saver;
    string savePath;

    public static MapData selectedMap;

    private void Awake()
    {
        loadMaps();
    }

    void loadMaps()
    {
        saver = new SaveSystem();

        savePath = Application.streamingAssetsPath + "/maps";
        string[] filePaths = System.IO.Directory.GetFiles(savePath, "*.json");
        maps = new List<MapData>();
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
        if (selectedMap == null)
        {
            selectedMap = maps[0];
        }

        maps = maps.OrderBy(map => map.name).ToList<MapData>();
    }

    void setSelectdMap() {
    }

    string checkMapValidity(MapData mapData)
    {
        int mapFieldCount = mapData.size.x * mapData.size.y;

        if (mapData.contents.Length != mapFieldCount)
            return $"{mapData.name}'s tiles are exceeding or receding the specified mapFieldCount .";
        if (PlayerSettingsManager.settings.requireSquareMap && mapData.size.x != mapData.size.y)
            return $"{mapData.name}is not square.";
        for (int i = 0; i < mapData.contents.Length; i++)
        {
            if (mapData.contents[i] != 129 && mapData.contents[i] != 130 && mapData.contents[i] != 64 && mapData.contents[i] != 0)
                return $"{mapData.name} is using unkwon numbers. Pls update your map to only include the numbers: 129, 130, 64 and 0";
        }
        if (checkPlayerValidity(mapData.contents))
            return $"{mapData.name} is missing one or more players";
        return "Valid";
    }

    bool checkPlayerValidity(int[] contents)
    {
        for (int i = 0; i < contents.Length; i++)
        {
            if (contents[i] == 130)
            {
                for (int j = 0; j < contents.Length; j++)
                {
                    if (contents[i] == 129)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
