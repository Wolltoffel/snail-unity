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
    public string loserName;
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
        this.loserName = looserName;
        this.winnerScore = winnerScore;
        this.loserScore = loserScore;
        this.agentWinner =agentWinner;
        this.agentLoser = agentLoser;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="Highscore"/>.
    /// </summary>
    /// <param name="obj">The object to compare with the current <see cref="Highscore"/>.</param>
    /// <returns><c>true</c> if the specified object is equal to the current <see cref="Highscore"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj)
    {
        if (obj is Highscore)
        {
            return GetHashCode() == ((Highscore)obj).GetHashCode();
        }
        return false;
    }

    /// <summary>
    /// Gets the hash code for the current <see cref="Highscore"/>.
    /// </summary>
    /// <returns>A hash code for the current <see cref="Highscore"/>.</returns>
    public override int GetHashCode()
    {
        int hashCode = 17; // Multiply by prime numbers to reduce hashCodeCollissions
        hashCode = hashCode * 23 + mapName.GetHashCode();
        hashCode = hashCode * 23 + winnerName.GetHashCode();
        hashCode = hashCode * 23 + loserName.GetHashCode();
        hashCode = hashCode * 23 + winnerScore.GetHashCode();
        hashCode = hashCode * 23 + loserScore.GetHashCode();
        hashCode = hashCode * 23 + agentWinner.GetHashCode();
        hashCode = hashCode * 23 + agentLoser.GetHashCode();
        return hashCode;
    }

    /// <summary>
    /// Determines whether two specified <see cref="Highscore"/> objects are equal.
    /// </summary>
    /// <param name="highscore1">The first <see cref="Highscore"/> to compare.</param>
    /// <param name="highscore2">The second <see cref="Highscore"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="Highscore"/> objects are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Highscore highscore1, Highscore highscore2)
    {
        return highscore1.Equals(highscore2);
    }

    /// <summary>
    /// Determines whether two specified <see cref="Highscore"/> objects are not equal.
    /// </summary>
    /// <param name="highscore1">The first <see cref="Highscore"/> to compare.</param>
    /// <param name="highscore2">The second <see cref="Highscore"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="Highscore"/> objects are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Highscore highscore1, Highscore highscore2)
    {
        return !highscore1.Equals(highscore2);
    }

}
