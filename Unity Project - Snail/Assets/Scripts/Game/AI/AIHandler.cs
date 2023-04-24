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

    public AIHandler()
    {
        negamaxAgent = new NegamaxAgent();
    }

    public Vector2Int giveNextMove(Tile[,] tiles , uint playerindex)
    {
        remapTiles(tiles,playerindex);
        return calculateNextMove();
    }

    void remapTiles(Tile[,] tiles,uint index) {
        Vector2 mapSize = MapBuilder.size;
        Vec2Int mapSizeToInt = new Vec2Int((int)mapSize.x, (int)mapSize.y);
        worldState = new WorldState(mapSizeToInt, 1, RoundManager.turnCounter);

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
                
                worldState[x, y] = new TileData(contents, 1);
            }
        }
    }


    Vector2Int calculateNextMove()
    {
        PlayerSettings settings = PlayerSettingsManager.settings;
        Constraints constraints = new Constraints(Convert.ToInt64(settings.maxComputationTimePerTurn), Convert.ToInt64(settings.miniCPUAgentTurnDuration));
        Task<IPlayerCommand> task = negamaxAgent.ComputeNextMoveAsync(worldState, constraints);
        Vec2Int delta = task.Result.Delta;
        Vec2Int origin = task.Result.Origin;
        Vec2Int newPosition = origin + delta;
        newPosition.Deconstruct(out int x, out int y);
        return new Vector2Int(x,y);
    }

}
