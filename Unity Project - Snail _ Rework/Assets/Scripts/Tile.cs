using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TileState
{
    Slime, Impassable, Default, Snail
}

public enum PlayerNumber
{
    Empty, PlayerOne, PlayerTwo
}
public class Tile
{
    public Vector2Int position;
    public PlayerNumber playerNumber;
    public TileState tileState;

    public Tile(Vector2Int position, PlayerNumber playerNumber, TileState tileState)
    {
        this.position = position;
        this.playerNumber = playerNumber;
        this.tileState = tileState;
    }
}
