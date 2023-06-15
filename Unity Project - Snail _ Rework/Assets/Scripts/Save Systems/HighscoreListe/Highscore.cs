using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Represents a high score entry.
/// </summary>
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

    /// <summary>
    /// Initializes a new instance of the <see cref="Highscore"/> struct with the specified data.
    /// </summary>
    /// <param name="mapName">The name of the map associated with the high score entry.</param>
    /// <param name="winnerName">The name of the winner.</param>
    /// <param name="loserName">The name of the loser.</param>
    /// <param name="winnerScore">The score of the winner.</param>
    /// <param name="loserScore">The score of the loser.</param>
    /// <param name="agentWinner">The agent representing the winner.</param>
    /// <param name="agentLoser">The agent representing the loser.</param>
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
