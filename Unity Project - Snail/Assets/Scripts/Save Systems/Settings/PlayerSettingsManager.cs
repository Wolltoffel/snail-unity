using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerSettingsManager : MonoBehaviour
{
    public static PlayerSettings settings;
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
        string[] filePaths = System.IO.Directory.GetFiles(savePath, "*settings.gcf");
        if (filePaths.Length!=0)
            settings = saver.LoadData<PlayerSettings>(filePaths[0]);
        else
        {
            settings = new PlayerSettings();
            settings.loadDefaultValues();
        }

       
    }

    public void saveSettings(PlayerSettings settings)
    {   
        saver.SaveData(settings, savePath + "/settings.gcf");
    }

}
