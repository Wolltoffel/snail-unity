using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Represents the state of a tile on the game board.
/// </summary>
public enum TileState
{
    Slime, Impassable, Default, Snail
}

/// <summary>
/// Represents the player occupying a tile on the game board.
/// </summary>
public enum PlayerNumber
{
    Empty, PlayerOne, PlayerTwo
}

/// <summary>
/// Represents a tile on the game board.
/// </summary>
public class Tile
{
    /// <summary>
    /// The position of the tile on the game board.
    /// </summary>
    public Vector2Int position;
    /// <summary>
    /// The world position of the tile in 3D space.
    /// </summary>
    public Vector3 worldPosition;
    public PlayerNumber playerNumber;
    public TileState tileState;

    /// <summary>
    /// Indicates whether slime has been spawned on the tile.
    /// </summary>
    public bool spawnedSlime = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="Tile"/> class.
    /// </summary>
    /// <param name="position">The position of the tile on the game board.</param>
    /// <param name="worldPosition">The world position of the tile in 3D space.</param>
    /// <param name="playerNumber">The player occupying the tile.</param>
    /// <param name="tileState">The state of the tile.</param>
    public Tile(Vector2Int position,Vector3 worldPosition, PlayerNumber playerNumber, TileState tileState)
    {
        this.position = position;
        this.playerNumber = playerNumber;
        this.tileState = tileState;
        this.worldPosition = worldPosition;
    }

    /// <summary>
    /// Checks if the tile has slime based on the player index.
    /// </summary>
    /// <param name="playerIndex">The index of the player.</param>
    /// <returns><c>true</c> if the tile has slime for the specified player index; otherwise, <c>false</c>.</returns>
    public bool CheckSlime (int playerIndex)
    {
        if (tileState == TileState.Slime)
        {
            if (playerIndex==0 && playerNumber == PlayerNumber.PlayerOne)
                return true;
            if (playerIndex==1 && playerNumber == PlayerNumber.PlayerTwo)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Sets the player number of the tile based on the player index.
    /// </summary>
    /// <param name="playerIndex">The index of the player.</param>
    public void SetPlayerNumber(int playerIndex)
    {
        if (playerIndex == 0)
            playerNumber = PlayerNumber.PlayerOne;
        else if (playerIndex == 1)
            playerNumber = PlayerNumber.PlayerTwo;
        else
            playerNumber = PlayerNumber.Empty;
    }


}
