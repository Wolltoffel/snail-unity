using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreList : SaveData
{
    HighscoreData[] highscoreData;

    private void Start()
    {
        highscoreData = new HighscoreData[3];
    }

    public void AddHighScore(HighscoreData newData)
    {
        HighscoreData[] highscoreDataTemp = highscoreData;

        for (int i= 0; i < highscoreData.Length; i++)
        {
            if (highscoreData[i].winnerScore>newData.winnerScore)
            {
                highscoreData[0] = newData;
                if (i != 0)
                    highscoreData[i] = highscoreDataTemp[i-1]; 
            }
        }
    }

    void SaveHighScore()
    {
        SaveSystem saver = new SaveSystem();
        saver.SaveData(this, "/HighScoreList/highscorelist.json");
    }

    public override void loadDefaultValues()
    {
        
    }



}
