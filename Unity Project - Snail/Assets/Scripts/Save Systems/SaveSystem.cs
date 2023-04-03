using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }

    void Awake()
    {
    }

    public void saveData(SaveData saveFile, string savePath)
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

    public void loadData(SaveData saveFile,string savePath)
    {
        StreamReader reader = new StreamReader(savePath);
        string json = reader.ReadToEnd();

        saveFile = JsonUtility.FromJson<PlayerSettings>(json);

        Debug.Log("Loading Data");
        Debug.Log(json);
    }

    public string findSave(string fileNameInput,string path)
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

    public string[] returnAllSaves (string path)
    {
        return System.IO.Directory.GetFiles(path);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
           Debug.Log ( returnAllSaves(Application.streamingAssetsPath));
        }
    }
}
