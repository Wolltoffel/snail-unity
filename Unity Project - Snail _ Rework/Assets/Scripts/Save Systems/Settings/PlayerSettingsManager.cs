using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerSettingsManager
{
    public PlayerSettings settings;
    SaveSystem saver;
    string savePath;

    public PlayerSettingsManager()
    {
        saver = new SaveSystem();
        savePath = Application.streamingAssetsPath + "/PlayerSettings";
        LoadSettings();
    }

    void LoadSettings()
    {
        string[] filePaths = System.IO.Directory.GetFiles(savePath, "*settings.gcf");
        if (filePaths.Length != 0)
        {
            settings = saver.LoadData<PlayerSettings>(filePaths[0]);
            settings.CorrectInvalidEntries();
        }     
        else
        {
            settings = new PlayerSettings();
            settings.LoadDefaultValues();
        }
        SaveSettings(settings);

    }

    void SaveSettings(PlayerSettings settings)
    {   
        saver.SaveData(settings, savePath + "/settings.gcf");
    }

}
