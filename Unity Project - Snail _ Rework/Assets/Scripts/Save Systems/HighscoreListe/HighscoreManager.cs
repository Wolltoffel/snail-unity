using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[Serializable]
public class HighscoreData: SaveData
{
    public Highscore[] highscores;

    public HighscoreData(int maHighScoreCount)
    {
        highscores = new Highscore[maHighScoreCount];
    }

    public override void LoadDefaultValues() { }
}

public class HighscoreManager
{

    public HighscoreData highscoreData;
    string savePath;
    string fileName;
    int maxHighScoreCount;

    /// <summary>
    /// Initializes a new instance of the <see cref="HighscoreManager"/> class with the specified map name.
    /// </summary>
    /// <param name="mapName">The name of the map associated with the highscores.</param>
    public HighscoreManager(string mapName)
    {
        savePath = Application.streamingAssetsPath + "/HighscoreList";
        fileName = mapName+"_highscoreData";
        maxHighScoreCount = 3;
        highscoreData = new HighscoreData(maxHighScoreCount);
        LoadHighscoreList();
    }

    /// <summary>
    /// Loads the highscore list from the file system.
    /// </summary>
    void LoadHighscoreList()
    {
        string[] filePaths = System.IO.Directory.GetFiles(savePath, $"{fileName}*.json");

        if (filePaths.Length>0)
        {
            SaveSystem saver = new SaveSystem();
            highscoreData = saver.LoadData<HighscoreData>(filePaths[0]);
        }
    }

    public HighscoreData GetHighscoreData()
    {
        return highscoreData;
    }

    public void SaveHighscoreData()
    {
        SaveSystem saver = new SaveSystem();

        saver.SaveData(highscoreData, savePath + $"/{fileName}.json");
    }

    /// <summary>
    /// Attempts to add new highscore data to the highscore list.
    /// </summary>
    /// <param name="newData">The new highscore data to add.</param>
    public void AttemptToAddToHighscoreData(Highscore newData)
    {
        Highscore[] highscores = highscoreData.highscores;

        for (int i = 0; i < highscores.Length; i++)
        {
            if (highscores[i].winnerScore < newData.winnerScore)
            {
                AddToHighscoreData(newData);
                break;
            }
        }
    }

    /// <summary>
    /// Adds new highscore data to the highscore list at the specified index.
    /// </summary>
    /// <param name="newData">The new highscore data to add.</param>
    void AddToHighscoreData(Highscore newData)
    {
        List<Highscore> highscores = new List<Highscore>();
        highscores.AddRange(highscoreData.highscores);
        highscores.Add(newData);
        highscores = highscores.OrderByDescending(highscore => highscore.winnerScore).ToList<Highscore>();

        for (int i = 0; i < maxHighScoreCount; i++)
        {
            highscoreData.highscores[i] = highscores[i];
        }

        SaveHighscoreData();
    }
}
