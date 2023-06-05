using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerSettingsManager
{
    public PlayerSettings settings;
    SaveSystem saver;
    string savePath;

    public  PlayerSettingsManager()
    {
        saver = new SaveSystem();
        savePath = Application.streamingAssetsPath + "/PlayerSettings";
        loadSettings();
    }

    void loadSettings()
    {
        string[] filePaths = System.IO.Directory.GetFiles(savePath, "*settings.gcf");
        if (filePaths.Length!=0)
            settings = saver.LoadData<PlayerSettings>(filePaths[0]);
        else
        {
            settings = new PlayerSettings();
            settings.loadDefaultValues();
        }
    }

    void saveSettings(PlayerSettings settings)
    {   
        saver.SaveData(settings, savePath + "/settings.gcf");
    }

}
