using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snails.Agent;
using Snails;
using System.Threading.Tasks;

public class AIHandler : MonoBehaviour
{
    TileData[] tileData;
    Player player;
    WorldState worldState;
    NegamaxAgent negamaxAgent;

    void remapTiles(List<Tile> tiles) {
        for (int i= 0; i < tiles.Count; i++)
        {
            ContentType contents = ContentType.Empty;
            if (tiles[i].checkSlime(player))
                contents = ContentType.Trail;
            if (tiles[i].playerSlot != null)
                contents = ContentType.Snail;
            if (!tiles[i].checkPassable(player))
                contents = ContentType.Impassable;
            
            tileData[i] = new TileData(contents, 0);
        }
    }

    void remapMap(TileData[] tileData)
    {
        Vector2 mapSize  = MapBuilder.size;
        Vec2Int mapSizeToInt = new Vec2Int((int)mapSize.x, (int)mapSize.y);
        worldState = new WorldState(mapSizeToInt, 129, RoundManager.turnCounter);
    }

    void setPlayers()
    {
        negamaxAgent = new NegamaxAgent();
        Constraints constraints = new Constraints(10, 10);
        Task<IPlayerCommand> task = negamaxAgent.ComputeNextMoveAsync(worldState, constraints);
        Vec2Int delta = task.Result.Delta;
        Vec2Int origin = task.Result.Origin;
    }

}
