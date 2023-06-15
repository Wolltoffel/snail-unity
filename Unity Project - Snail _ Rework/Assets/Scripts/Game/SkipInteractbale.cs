using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Represents an interactable surrender button in the game.
/// </summary>
public class SurrenderInteractable : MonoBehaviour, IPointerClickHandler
{
    GameController gameController;

    /// <summary>
    /// Adds a reference to the game controller.
    /// </summary>
    /// <param name="gameController">The game controller to add.</param>
    public void AddGameController(GameController gameController)
    {
        this.gameController = gameController;
    }

    // <summary>
    /// Handles the pointer click event.
    /// </summary>
    /// <param name="eventData">The pointer event data.</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerAction action = new PlayerAction(ActionType.Skip, Vector2Int.zero);
        gameController?.SetAction(action);
    }
}
