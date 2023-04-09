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
        saver = new SaveSystem();

       savePath = Application.streamingAssetsPath + "/maps";
       string[] filePaths  = System.IO.Directory.GetFiles(savePath, "*.json");
       maps = new List<MapData>();
       MapData[] mapArray = new MapData[filePaths.Length];

        for (int i = 0; i < filePaths.Length; i++)
        {
            mapArray[i] = saver.LoadData<MapData>(filePaths[i]);

            if (mapArray[i] != null)
            {
                int mapFieldCount = mapArray[i].size.x * mapArray[i].size.y;
                
                if (mapArray[i].contents.Length == mapFieldCount)
                {
                    mapArray[i].name = Path.GetFileNameWithoutExtension(filePaths[i]);
                    maps.Add(mapArray[i]);
                }
                else
                {
                    Debug.Log("Map not formatted correctly");
                }
                
            }
        }

        selectedMap = maps[0];

        maps = maps.OrderBy(map => map.name).ToList<MapData>();
    }
}
