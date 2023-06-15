using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum ActionType
{
    Skip, Surrender, Move
}

public class PlayerAction

{
    public Vector2Int position;
    public ActionType actionType;

    public PlayerAction(ActionType move, Vector2Int position)
    {
        this.actionType = move;
        this.position = position;
    }

}
