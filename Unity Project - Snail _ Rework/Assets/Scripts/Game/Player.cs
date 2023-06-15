using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAgent
{
    Human, Ai
}

public class Player
{
    public string name;
    public int score;
    public int index;
    public PlayerAgent playerAgent;
    public Vector2Int position;
    PlayerVisual playerVisual;
    public int turnsWithoutCapture = 0;

    public Player(string name,PlayerAgent playerAgent, int score, Vector2Int position, PlayerVisual playerVisual, int index, GameObject mapParent, GameController gameController)
    {
        this.name = name;
        this.playerAgent = playerAgent;
        this.score = score;
        this.position = position;
        this.playerVisual = playerVisual;
        this.index = index;

        playerVisual.SpawnPlayerObjects(new Vector3(position.x, -position.y, 0),mapParent, name,gameController);
    }

    public void SpawnSlimeVisuals(Tile tile,GameObject parent) {
        playerVisual.SpawnSlime(tile,parent);
    }

    public void SetSkipButtonActive(bool active)
    {
        playerVisual.SetSkipButtonActive(active);
    }

    public void IncreaseTurnsWithoutCapture() {
        turnsWithoutCapture++;
    }

    public void IncreaseScore()
    {
        score++;
    }

    public IEnumerator Move(Vector2Int target)
    {
        position = target;
        yield return playerVisual.Move(new Vector3 (target.x,-target.y,0));
    }

    public void Reset()
    {
        playerVisual.Reset();
    }

}
