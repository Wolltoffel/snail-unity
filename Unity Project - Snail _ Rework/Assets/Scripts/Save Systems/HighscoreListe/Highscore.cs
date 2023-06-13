using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct Highscore

{
    public string mapName;
    public string winnerName;
    public string looserName;
    public int winnerScore;
    public int loserScore;
    public PlayerAgent agentWinner;
    public PlayerAgent agentLoser;

    public Highscore(string mapName, string winnerName, string looserName, int winnerScore, int loserScore, PlayerAgent agentWinner, PlayerAgent agentLoser)
    {
        this.mapName = mapName;
        this.winnerName = winnerName;
        this.looserName = looserName;
        this.winnerScore = winnerScore;
        this.loserScore = loserScore;
        this.agentWinner =agentWinner;
        this.agentLoser = agentLoser;
    }


}
