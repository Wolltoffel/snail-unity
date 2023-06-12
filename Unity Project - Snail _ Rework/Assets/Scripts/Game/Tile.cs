using JetBrains.Annotations;
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
    public Vector3 worldPosition;
    public PlayerNumber playerNumber;
    public TileState tileState;

    public Tile(Vector2Int position,Vector3 worldPosition, PlayerNumber playerNumber, TileState tileState)
    {
        this.position = position;
        this.playerNumber = playerNumber;
        this.tileState = tileState;
        this.worldPosition = worldPosition;
    }

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
