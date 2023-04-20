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

    public bool trySettingSelectedMap(MapData mapData) {
        string mapValidity = checkMapValidity(mapData);
        if (mapValidity== "Valid")
        {
            selectedMap = mapData;
            return true;
        }
        else
        {
           PopUpManager popUpManager  =  new PopUpManager("PopUpTemplates/PopUp_Template_1");
           popUpManager.showPopUp(mapValidity, 5);
            return false;
        }
    }

    string checkMapValidity(MapData mapData)
    {
        int mapFieldCount = mapData.size.x * mapData.size.y;
        PlayerSettings playersettings = PlayerSettingsManager.settings;

        if (mapData.contents.Length != mapFieldCount)
            return $"{mapData.name}'s tiles are exceeding or receding the specified mapFieldCount.";
        if (playersettings.requireSquareMap && mapData.size.x != mapData.size.y)
            return $"{mapData.name}is not square.";
        if (mapData.size.x>playersettings.maxMapSize| 
            mapData.size.y > playersettings.maxMapSize| 
            mapData.size.x < playersettings.minMapSize| 
            mapData.size.y < playersettings.minMapSize)
            return $"{mapData.name} does not fulfill the required size requirements.";
        for (int i = 0; i < mapData.contents.Length; i++)
        {
            if (mapData.contents[i] != 129 && mapData.contents[i] != 130 && mapData.contents[i] != 64 && mapData.contents[i] != 0)
                return $"{mapData.name} is using unkwon numbers. Pls update your map to only include the numbers: 129, 130, 64 and 0.";
        }
        if (checkPlayerValidity(mapData.contents))
            return $"{mapData.name} is missing one or more players.";
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

