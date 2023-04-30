using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatData: EventArgs
{
    public int rounds;
    public int winnerScore;
    public int loserScore;
    public string winner;
    public string loser;
    public string mapName;
    public ActiveAgent agentWinner;
    public ActiveAgent agentLoser;

    public StatData(int rounds, int winnerScore,string winner,string loser,int loserScore, string mapName, ActiveAgent agentWinner, ActiveAgent agentLoser) 
    {
        this.rounds = rounds;
        this.winnerScore = winnerScore;
        this.winner = winner;
        this.loserScore = loserScore;
        this.mapName = mapName;
        this.loser = loser;
    }
}
