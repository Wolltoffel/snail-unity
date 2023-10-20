using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Enumeration of possible action types.
/// </summary>
public enum ActionType
{
    Skip, Surrender, Move
}

/// <summary>
/// Represents a player action.
/// </summary>
public class PlayerAction

{
    /// <summary>
    /// The position associated with the action.
    /// </summary>
    public Vector2Int position;
    /// <summary>
    /// The type of the action.
    /// </summary>
    public ActionType actionType;

    /// <summary>
    /// Initializes a new instance of the PlayerAction class with the specified action type and position.
    /// </summary>
    /// <param name="actionType">The type of the action.</param>
    /// <param name="position">The position associated with the action.</param>
    public PlayerAction(ActionType move, Vector2Int position)
    {
        this.actionType = move;
        this.position = position;
    }

}
