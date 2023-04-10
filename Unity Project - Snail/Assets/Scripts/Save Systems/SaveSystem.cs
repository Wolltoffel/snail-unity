using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem
{
    void Awake()
    {
    }

    public void SaveData(SaveData saveFile, string savePath)
    {
        string json = JsonUtility.ToJson(saveFile);
       
        try
        {
            StreamWriter writer = new StreamWriter(savePath);
            writer.Write(json);
            writer.Close();
            writer = null;
            Debug.Log("SavedData to " + savePath);
        }
        catch (IOException)
        {
            Debug.Log("Could not save. Try again in a few seconds");
        }
      
    }  

    public T LoadData<T>(string savePath) where T:SaveData
    {
        StreamReader reader = new StreamReader(savePath);
        string json = reader.ReadToEnd();

        try
        {
            return JsonUtility.FromJson<T>(json);
        }
        catch(System.Exception ex)
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
