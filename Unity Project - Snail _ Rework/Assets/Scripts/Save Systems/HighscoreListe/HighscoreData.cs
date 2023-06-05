using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Playeragent
{

}

public class HighscoreData: SaveData
{
    public string mapName;
    public string winnerName;
    public string looserName;
    public int winnerScore;
    public int loserScore;
    public Playeragent agentWinner;
    public Playeragent agentLoser;

    public HighscoreData(string mapName, string winnerName, string looserName, int winnerScore, int loserScore, Playeragent agentWinner, Playeragent agentLoser)
    {
        this.mapName = mapName;
        this.winnerName = winnerName;
        this.looserName = looserName;
        this.winnerScore = winnerScore;
        this.loserScore = loserScore;
        this.agentWinner =agentWinner;
        this.agentLoser = agentLoser;
    }

    public override void loadDefaultValues() { }

}
