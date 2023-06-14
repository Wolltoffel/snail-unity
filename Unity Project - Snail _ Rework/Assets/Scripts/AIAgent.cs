using Snails.Agent;
using Snails;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    IEnumerator Run(long maxTimePerTurn, Vector2Int size, PlayerNumber player, int turn, Tile[,] tiles)
    {
        IWorldState state = ConvertToWorldState(size, player, turn, tiles);
        NegamaxAgent agent = new NegamaxAgent();
        var move = agent.ComputeNextMoveAsync(state, new Constraints((long)GameData.playersettings.maxComputationTimePerTurn, long.MaxValue));
        yield return new WaitUntil(() => move.IsCompleted);
        if (move.IsCompletedSuccessfully)
        {
            var result = move.Result;
            var direction = result.Delta;
            CarryOutPlayerAction(new PlayerAction(ActionType.Move,new Vector2Int(direction.X,direction.Y)));
            yield break;
        }
        CarryOutPlayerAction(new PlayerAction(ActionType.Skip,Vector2Int.zero));
    }

    private void CarryOutPlayerAction(PlayerAction playeraction)
    {
        Debug.Log(playeraction);
    }

    IWorldState ConvertToWorldState(Vector2Int size, PlayerNumber player, int turn, Tile[,] tiles)
    {
        WorldState worldState = new WorldState(new Vec2Int(size.x, size.y), (uint)player+1, turn);

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                var data = new TileData(ConvertData(tiles[i, j].tileState), (uint)player);
                worldState[i, j] = data;
            }
        }

        return worldState;
    }

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
