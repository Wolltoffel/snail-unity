using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerSettingsManager : MonoBehaviour
{
    public PlayerSettings settings;
    SaveSystem saver;
    string savePath;

    private void Awake()
    {
        saver = new SaveSystem();
        savePath = Application.streamingAssetsPath + "/PlayerSettings";
        loadSettings();
    }

    void loadSettings()
    {
        string[] filePaths = System.IO.Directory.GetFiles(savePath, "*settings.json");
        settings = saver.LoadData<PlayerSettings>(filePaths[0]);
    }

    void saveSettings(PlayerSettings settings)
    {   
        saver.SaveData(settings, savePath + "/settings.json");
    }

}
