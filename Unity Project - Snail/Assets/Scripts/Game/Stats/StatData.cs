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

    public StatData(int rounds, int winnerScore,string winner,string loser,int loserScore) 
    {
        this.rounds = rounds;
        this.winnerScore = winnerScore;
        this.winner = winner;
    }
}
