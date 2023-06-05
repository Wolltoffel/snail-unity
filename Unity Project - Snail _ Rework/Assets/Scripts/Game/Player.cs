using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAgent
{
    Human, Ai
}

public class Player
{
    string name;
    int score;
    public Vector2Int position;
    PlayerVisual playerVisual;
    public int index;
    public PlayerAgent playerAgent;

    public Player(string name,PlayerAgent playerAgent, int score, Vector2Int position, PlayerVisual playerVisual, int index, GameObject mapParent)
    {
        this.name = name;
        this.playerAgent = playerAgent;
        this.score = score;
        this.position = position;
        this.playerVisual = playerVisual;
        this.index = index;

        playerVisual.SpawnPlayerObjects(new Vector3(position.x, -position.y, 0),mapParent, name);
    }

    public void SpawnSlimeVisuals(Tile tile,GameObject parent) {
        playerVisual.SpawnSlime(tile,parent);
    }

    public IEnumerator Move(Vector2Int target)
    {
        score++;
        position = target;
        yield return playerVisual.Move(new Vector3 (target.x,-target.y,0));
    }
}
