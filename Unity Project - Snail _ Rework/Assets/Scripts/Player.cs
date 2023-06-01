using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int score;
    public Vector2Int position;
    PlayerVisual playerVisual;
    public int index;

    public Player(int score, Vector2Int position, PlayerVisual playerVisual, int index, GameObject mapParent)
    {
        this.score = score;
        this.position = position;
        this.playerVisual = playerVisual;
        this.index = index;

        playerVisual.SpawnPlayerObjects(new Vector3(position.x, -position.y, 0),mapParent);
    }

    public void SpawnSlime(Vector3 worldPosition, GameObject grassParent) {
        playerVisual.SpawnSlime(worldPosition, grassParent);
    }

    public IEnumerator Move(Vector2 target)
    {
        score++;
        yield return playerVisual.Move(new Vector3 (target.x,-target.y,0));
    }
}
