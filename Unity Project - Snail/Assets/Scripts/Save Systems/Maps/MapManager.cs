using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapManager : MonoBehaviour
{
    public List<MapData> maps;
    SaveSystem saver;
    string savePath;

    public MapData selectedMap;

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
                mapArray[i].name = Path.GetFileNameWithoutExtension(filePaths[i]);
                maps.Add(mapArray[i]);
            }
        }

        selectedMap = maps[0];
    }
}
