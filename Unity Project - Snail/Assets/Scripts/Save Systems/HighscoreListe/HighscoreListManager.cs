using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreListManager : MonoBehaviour
{
    HighscoreList highScoreList;
    string savePath;

    public void Awake()
    {
        savePath = Application.streamingAssetsPath + "/HighscoreList";
        loadHighscoreList();
    }

    public HighscoreData[] giveCurrentHighscoreList(HighscoreData highscoreData)
    {
        attemptToAddToHighScoreList(highscoreData);
        return highScoreList.highscoreDataList;
    }

    void loadHighscoreList()
    {
        SaveSystem saver = new SaveSystem();

        string[] filePaths = System.IO.Directory.GetFiles(savePath, "*highscorelist.json");
        if (filePaths.Length != 0)
            highScoreList = saver.LoadData<HighscoreList>(filePaths[0]);
        else
        {
            highScoreList = new HighscoreList();
        }
    }

    void saveHighscoreList()
    {
        SaveSystem saver = new SaveSystem();
        saver.SaveData(highScoreList, savePath+"/highscorelist.json");
    }

    void attemptToAddToHighScoreList(HighscoreData newData)
    {
        HighscoreData[] highscoreDataList = highScoreList.highscoreDataList;

        for (int i = 0; i < highscoreDataList.Length; i++)
        {
            if (highscoreDataList[i]==null||highscoreDataList[i].winnerScore < newData.winnerScore)
            {
                highscoreDataList[i] = newData;
                saveHighscoreList();
                return;
            }
        }
    }

    void attemptToAddToHighScoreListImproved(HighscoreData newData)
    {
        HighscoreData[] highscoreDataList = highScoreList.highscoreDataList;

        for (int i = 0; i < highscoreDataList.Length; i++)
        {
            if (highscoreDataList[i].winnerScore < newData.winnerScore)
            {
                highscoreDataList[i] = newData;
                saveHighscoreList();
                return;
            }
        }
    }
}
