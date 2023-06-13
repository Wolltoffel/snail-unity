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

    public HighscoreManager(string mapName)
    {
        savePath = Application.streamingAssetsPath + "/HighscoreList";
        fileName = mapName+"_highscoreData";
        maxHighScoreCount = 3;
        highscoreData = new HighscoreData(maxHighScoreCount);
        LoadHighscoreList();
    }

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

    public bool AttemptToAddToHighscoreData(Highscore newData)
    {

        Highscore[] highscores = highscoreData.highscores;

        for (int i = 0; i < highscores.Length; i++)
        {
            if (highscores[i].winnerScore < newData.winnerScore)
            {
                AddToHighscoreData(newData, i);
                return true;
            }
        }
        return false;
    }

    void AddToHighscoreData(Highscore newData, int index)
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
