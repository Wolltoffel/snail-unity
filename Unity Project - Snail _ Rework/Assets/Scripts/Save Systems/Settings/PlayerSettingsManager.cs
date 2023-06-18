using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Manages the player settings, including loading and saving them.
/// </summary>
public class PlayerSettingsManager
{
    public PlayerSettings settings;
    SaveSystem saver;
    string savePath;

    /// <summary>
    /// Initializes a new instance of the PlayerSettingsManager class.
    /// </summary>
    public PlayerSettingsManager()
    {
        saver = new SaveSystem();
        savePath = Application.streamingAssetsPath + "/PlayerSettings";
        LoadSettings();
    }

    /// <summary>
    /// Loads the player settings from the saved file, or creates default settings if no saved file exists.
    /// </summary>
    public void LoadSettings()
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

    /// <summary>
    /// Saves the player settings to a file.
    /// </summary>
    /// <param name="settings">The player settings to save.</param>
    public void SaveSettings(PlayerSettings settings)
    {   
        saver.SaveData(settings, savePath + "/settings.gcf");
    }

}
