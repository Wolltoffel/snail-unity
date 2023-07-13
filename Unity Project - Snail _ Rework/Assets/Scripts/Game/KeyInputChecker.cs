using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a class that checks for key inputs and triggers corresponding actions in the game.
/// </summary>
public class KeyInputChecker
{
    GameController gameController;

    /// <summary>
    /// Initializes a new instance of the KeyInputChecker class.
    /// </summary>
    /// <param name="gameController">The game controller to send actions to.</param>
    public KeyInputChecker (GameController gameController)
    {
        this.gameController = gameController;
    }

    /// <summary>
    /// Coroutine that checks for key inputs and triggers corresponding actions.
    /// </summary>
    public IEnumerator CheckKeyInputs() {

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                PlayerAction playerAction = new PlayerAction(ActionType.Skip, Vector2Int.zero);
                gameController.SetAction(playerAction);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PlayerAction playerAction = new PlayerAction(ActionType.Surrender, Vector2Int.zero);
                gameController.SetAction(playerAction);
            }

            yield return null;
        } 
    }
}
