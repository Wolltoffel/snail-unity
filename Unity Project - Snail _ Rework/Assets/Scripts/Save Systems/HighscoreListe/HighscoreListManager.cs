using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class HighscoreListManager : MonoBehaviour
{
    public HighscoreData[] highscoreList;
    string savePath;
    string fileName;
    int maxHighScoreCount;

    public void Awake()
    {
        savePath = Application.streamingAssetsPath + "/HighscoreList";
        fileName = "highscoreData";
        maxHighScoreCount = 3;
        loadHighscoreList();
    }

    public HighscoreData[] giveCurrentHighscoreList(HighscoreData highscoreData)
    {
        attemptToAddToHighScoreList(highscoreData);
        return highscoreList;
    }

    void loadHighscoreList()
    {
        string[] filePaths = System.IO.Directory.GetFiles(savePath, $"{fileName}*.json");
        highscoreList = new HighscoreData[maxHighScoreCount];

        for (int i= 0; i < filePaths.Length; i++)
        {
            if (i > maxHighScoreCount-1)
                return;
            SaveSystem saver = new SaveSystem();
            highscoreList[i] = saver.LoadData<HighscoreData>(filePaths[i]);
        }
    }

    void saveHighscoreList()
    {
        SaveSystem saver = new SaveSystem();
        for (int i = 0; i < highscoreList.Length; i++)
        {
            if (highscoreList[i]!=null)
                saver.SaveData(highscoreList[i], savePath + $"/{fileName}_{i}.json");
        }
    }

    void attemptToAddToHighScoreList(HighscoreData newData)
    {
        HighscoreData[] highscoreListTemp = highscoreList;

        for (int i = 0; i < highscoreList.Length; i++)
        {
            if (highscoreList[i]==null||highscoreList[i].winnerScore < newData.winnerScore)
            {
                addToHighscoreList(newData, i);
                saveHighscoreList();
                return;
            }
        }
    }

    void addToHighscoreList(HighscoreData newData, int index)
    {
        List<HighscoreData> sortedList = new List<HighscoreData>();
        sortedList.AddRange(highscoreList);
        sortedList.Add(newData);
        sortedList = sortedList.OrderByDescending(highscore => highscore.winnerScore).ToList<HighscoreData>();
       
        for (int i = 0; i < highscoreList.Length; i++)
        {
            highscoreList[i] = sortedList[i];
        }
    }

}
