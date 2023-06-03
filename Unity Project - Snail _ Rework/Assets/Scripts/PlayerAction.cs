using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    Skip, Surrender, Move
}

public class PlayerAction

{
    public int index;
    public Vector2Int position;


    public ActionType move;

    public PlayerAction(ActionType move, Vector2Int position)
    {
        this.move = move;
        this.position = position;
    }
}
