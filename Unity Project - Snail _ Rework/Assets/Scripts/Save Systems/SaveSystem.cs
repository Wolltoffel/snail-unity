using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem
{
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
