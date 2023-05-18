using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snails.Agent;
using Snails;
using System.Threading.Tasks;
using System;

public class AIHandler
{
    Player player;
    WorldState worldState;
    NegamaxAgent negamaxAgent;

    Task<IPlayerCommand> task;

    Vector2Int targetPosition;


    public AIHandler()
    {
        negamaxAgent = new NegamaxAgent();
    }

    public Vector2Int giveNextMove(Tile[,] tiles , uint playerindex)
    {
        remapTiles(tiles,playerindex);
        return giveNextMovePosition();
    }

    void remapTiles(Tile[,] tiles,uint index) {
        Vector2 mapSize = MapBuilder.size;
        Vec2Int mapSizeToInt = new Vec2Int((int)mapSize.x, (int)mapSize.y);
        Debug.Log(mapSizeToInt.ToString());
        worldState = new WorldState(mapSizeToInt, index +1, RoundManager.turnCounter);
        uint currentPlayerIndex = 0;
        string board = "";

        for (int y= 0; y < MapBuilder.size.y; y++)
        {
            for (int x = 0; x < MapBuilder.size.x; x++)
            {
                ContentType contents = ContentType.Empty;
                if (tiles[x,y].checkSlime(player))
                    contents = ContentType.Trail;
                if (tiles[x,y].playerSlot != null)
                    contents = ContentType.Snail;
                if (!tiles[x,y].checkPassable(player))
                    contents = ContentType.Impassable;
                if (tiles[x, y].playerSlot != null)
                    currentPlayerIndex = (uint)tiles[x, y].playerSlot.index;
                else
                    currentPlayerIndex = 1;


                worldState[x, y] = new TileData(contents, currentPlayerIndex);
                board += " " + worldState[x, y].ToString();
            }
            board += "\n";
        }
        Debug.Log(board);
    }

    public IEnumerator calculateNextMove()
    {
        PlayerSettings settings = PlayerSettingsManager.settings;
        Constraints constraints = new Constraints(Convert.ToInt64(settings.maxComputationTimePerTurn), Convert.ToInt64(settings.miniCPUAgentTurnDuration));
        task = negamaxAgent.ComputeNextMoveAsync(worldState, constraints);
        yield return new WaitUntil(() => task.IsCompleted );
        Vec2Int delta = task.Result.Delta;
        Vec2Int origin = task.Result.Origin;
        Vec2Int newPosition = origin + delta;
        newPosition.Deconstruct(out int x, out int y);
        targetPosition = new Vector2Int(x,y);
        yield break;
    }

    Vector2Int giveNextMovePosition() {
        return targetPosition;
    }

    public Task giveNextMoveTask()
    {
        return task;
    }

}
