using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapManager : MonoBehaviour
{
    public MapData[] maps;
    SaveSystem saver;
    string savePath;

    public MapData selectedMap;

    private void Awake()
    {
        saver = new SaveSystem();

       savePath = Application.streamingAssetsPath + "/maps";
       string[] filePaths  = System.IO.Directory.GetFiles(savePath, "*.json");
       maps = new MapData[filePaths.Length];

        for (int i = 0; i < filePaths.Length; i++)
        {
            maps[i] = saver.LoadData<MapData>(filePaths[i]);
            maps[i].name = Path.GetFileNameWithoutExtension(filePaths[i]);
        }

        selectedMap = maps[0];
    }
}
