using Snails.Agent;
using Snails;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an action performed by the AI agent.
/// </summary>
public class AIAction
{
    public Vector2Int direction;
    public ActionType actionType;

    public AIAction(ActionType move, Vector2Int direction)
    {
        this.actionType = move;
        this.direction = direction;
    }

    /// <summary>
    /// Converts the AI action to a player action.
    /// </summary>
    /// <param name="playerPostion">The current position of the player.</param>
    /// <returns>The converted player action.</returns>
    public PlayerAction ConvertToPlayerAction(Vector2Int playerPostion)
    {
        return new PlayerAction(actionType, playerPostion + direction);
    }
}

/// <summary>
/// Represents an AI agent for making AI-controlled moves.
/// </summary>
public class AIAgent
{
    int activePlayerIndex;
    public AIAction action;

    /// <summary>
    /// Initializes a new instance of the <see cref="AIAgent"/> class with the specified active player index.
    /// </summary>
    /// <param name="activePlayerIndex">The index of the active player.</param>
    public AIAgent (int activePlayerIndex)
    {
       this.activePlayerIndex = activePlayerIndex;
    }

    /// <summary>
    /// Runs the AI agent to compute the next action asynchronously.
    /// </summary>
    /// <param name="size">The size of the game board.</param>
    /// <param name="turn">The current turn number.</param>
    /// <param name="tiles">The 2D array of game tiles.</param>
    /// <returns>An enumerator for the asynchronous computation.</returns>
    public IEnumerator Run(Vector2Int size, int turn, Tile[,] tiles)
    {
        IWorldState state = ConvertToWorldState(size, activePlayerIndex, turn, tiles);
        NegamaxAgent agent = new NegamaxAgent();
        var move = agent.ComputeNextMoveAsync(state, new Constraints((long)GameData.playersettings.maxComputationTimePerTurn, long.MaxValue));
        yield return new WaitUntil(() => move.IsCompleted);
        if (move.IsCompletedSuccessfully)
        {
            var result = move.Result;
            var direction = result.Delta;
            action = new AIAction(ActionType.Move, new Vector2Int(direction.X, direction.Y));
        }
        else
        {
            action = new AIAction(ActionType.Skip, Vector2Int.zero);
        }
    }

    public AIAction GetAIAction() { return action; }

    /// <summary>
    /// Converts the game state to a world state representation.
    /// </summary>
    /// <param name="size">The size of the game board.</param>
    /// <param name="activePlayerNumber">The active player's number.</param>
    /// <param name="turn">The current turn number.</param>
    /// <param name="tiles">The 2D array of game tiles.</param>
    /// <returns>The converted world state.</returns>
    IWorldState ConvertToWorldState(Vector2Int size, int activePlayerNumber, int turn, Tile[,] tiles)
    {
        WorldState worldState = new WorldState(new Vec2Int(size.x, size.y), (uint)activePlayerNumber + 1, turn);

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                var data = new TileData(ConvertData(tiles[i, j].tileState), (uint)tiles[i, j].playerNumber);
                worldState[i, j] = data;
            }
        }

        return worldState;
    }

    /// <summary>
    /// Converts a tile state to a content type.
    /// </summary>
    /// <param name="tilestate">The tile state to convert.</param>
    /// <returns>The converted content type.</returns>
    ContentType ConvertData(TileState tilestate)
    {
        switch (tilestate)
        {
            case TileState.Default:
                return ContentType.Empty;
            case TileState.Impassable:
                return ContentType.Impassable;
            case TileState.Slime:
                return ContentType.Trail;
            case TileState.Snail:
                return ContentType.Snail;
            default:
                throw new ArgumentOutOfRangeException(nameof(tilestate));
        }
    }
}
