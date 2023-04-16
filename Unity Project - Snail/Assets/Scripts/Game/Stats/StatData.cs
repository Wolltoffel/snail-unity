using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatData
{
    public int rounds;
    public int winnerScore;
    public string winner;

    public StatData(int rounds, int winnerScore,string winner) 
    {
        this.rounds = rounds;
        this.winnerScore = winnerScore;
        this.winner = winner;
    }
}
