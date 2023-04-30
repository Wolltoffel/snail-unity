using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreList : SaveData
{
    public HighscoreData[] highscoreDataList;

    public HighscoreList()
    {
        highscoreDataList = new HighscoreData[3];
    }


    public override void loadDefaultValues()
    {
    }



}
