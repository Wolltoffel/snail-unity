using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


/// <summary>
/// Provides functionality for saving and loading data to/from files.
/// </summary>
public class SaveSystem
{
    /// <summary>
    /// Saves the data to a file at the specified path.
    /// </summary>
    /// <param name="saveFile">The SaveData object to save.</param>
    /// <param name="savePath">The path to save the file.</param>
    public void SaveData(SaveData saveFile, string savePath)
    {
        string json = JsonUtility.ToJson(saveFile);
       
        try
        {
            using StreamWriter writer = new StreamWriter(savePath);
            writer.Write(json);
            Debug.Log("SavedData to " + savePath);
        }
        catch (IOException)
        {
            Debug.Log("File is in use. Trying again");
            SaveData(saveFile, savePath);
        }
    }

    /// <summary>
    /// Loads the data from a file at the specified path.
    /// </summary>
    /// <typeparam name="T">The type of the SaveData to load.</typeparam>
    /// <param name="savePath">The path to load the file from.</param>
    /// <returns>The loaded SaveData object or null if loading fails.</returns>
    public T LoadData<T>(string savePath) where T:SaveData
    {
        using StreamReader reader = new StreamReader(savePath);
        string json = reader.ReadToEnd();

        try
        {
            return JsonUtility.FromJson<T>(json);
        }
        catch(System.Exception)
        {
            return null;
        }
        
    }

    /// <summary>
    /// Finds a save file with the specified file name in the given path.
    /// </summary>
    /// <param name="fileNameInput">The name of the file to find.</param>
    /// <param name="path">The directory path to search for the file.</param>
    /// <returns>The contents of the found file, or an empty string if the file is not found.</returns>
    public string FindSave(string fileNameInput,string path)
    {
        string [] fileNames =  System.IO.Directory.GetFiles(path);

        for (int i = 0; i < fileNames.Length; i++)
        {
            if (fileNames[i].Equals(fileNameInput))
            {
                StreamReader reader = new StreamReader(path);
                return  reader.ReadToEnd();
            }
        }
        return "";
    }

}
