using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the type of player agent.
/// </summary>
public enum PlayerAgent
{
    Human, Ai
}

/// <summary>
/// Represents a player in the game.
/// </summary>
public class Player
{
    public string name;
    public int score;
    public int index;
    public PlayerAgent playerAgent;
    public Vector2Int position;
    PlayerVisual playerVisual;
    public int turnsWithoutCapture = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="Player"/> class.
    /// </summary>
    /// <param name="name">The name of the player.</param>
    /// <param name="playerAgent">The type of player agent.</param>
    /// <param name="score">The player's score.</param>
    /// <param name="position">The player's initial position.</param>
    /// <param name="playerVisual">The visual representation of the player.</param>
    /// <param name="index">The index of the player.</param>
    /// <param name="mapParent">The parent object for the player's visual representation.</param>
    /// <param name="gameController">The game controller.</param>
    public Player(string name,PlayerAgent playerAgent, int score, Vector2Int position, PlayerVisual playerVisual, int index, GameObject mapParent, GameController gameController)
    {
        this.name = name;
        this.playerAgent = playerAgent;
        this.score = score;
        this.position = position;
        this.playerVisual = playerVisual;
        this.index = index;

        playerVisual.SpawnPlayerObjects(new Vector3(position.x, -position.y, 0),mapParent, name,gameController,playerAgent);
    }

    /// <summary>
    /// Spawns slime visuals on the specified tile.
    /// </summary>
    /// <param name="tile">The tile to spawn slime visuals on.</param>
    /// <param name="parent">The parent object for the slime visuals.</param>
    public void SpawnSlimeVisuals(Tile tile,GameObject parent) {
        playerVisual.SpawnSlime(tile,parent);
    }

    /// <summary>
    /// Sets the skip button active/inactive.
    /// </summary>
    /// <param name="active">The flag indicating whether the skip button should be active.</param>
    public void SetSkipButtonActive(bool active)
    {
        playerVisual.SetSkipButtonActive(active);
    }

    public void IncreaseTurnsWithoutCapture() {
        turnsWithoutCapture++;
    }

    public void IncreaseScore()
    {
        score++;
    }

    /// <summary>
    /// Moves the player to the target position.
    /// </summary>
    /// <param name="target">The target position.</param>
    /// <returns>An enumerator for the movement animation.</returns>
    public IEnumerator Move(Vector2Int target)
    {
        position = target;
        yield return playerVisual.Move(new Vector3 (target.x,-target.y,0));
    }

    /// <summary>
    /// Resets the playerVisual to its initial state.
    /// </summary>
    public void Reset()
    {
        playerVisual.Reset();
    }

}
