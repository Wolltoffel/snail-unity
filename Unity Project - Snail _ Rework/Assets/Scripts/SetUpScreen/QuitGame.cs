using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Handles quitting the game.
/// </summary>
public class QuitGame : MonoBehaviour
{
    /// <summary>
    /// Quits the application.
    /// </summary>
    public void QuitApplication()
    {
        Application.Quit();
    }

    /// <summary>
    /// Checks for the "Escape" key press and quits the application if detected.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
