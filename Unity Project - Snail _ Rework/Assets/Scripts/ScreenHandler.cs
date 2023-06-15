using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the available screen selections.
/// </summary>
public enum ScreenSelection
{
    SetUp,Game,Results
}

/// <summary>
/// Handles the activation and deactivation of different screens.
/// </summary>
public class ScreenHandler : MonoBehaviour
{
    [SerializeField]GameObject setUpScreen;
    [SerializeField]GameObject gameScene;
    [SerializeField]GameObject resultScreen;


    public void Awake()
    {
        SetScreenActive(ScreenSelection.SetUp);
    }

    /// <summary>
    /// Sets the specified screen active and deactivates the others.
    /// </summary>
    /// <param name="sceneSelection">The screen to set as active.</param>
    public void SetScreenActive(ScreenSelection sceneSelection)
    {
        for (int i = 0;i<3;i++)
        {
            if (i== (int)sceneSelection)
                ActivateScreen(i);
            else
                DeactivateScreen(i);
        }
    }

    /// <summary>
    /// Activates the specified screen.
    /// </summary>
    /// <param name="sceneIndex">The index of the screen to activate.</param>

    void ActivateScreen(int sceneIndex)
    {
       switch (sceneIndex)
        {
            case 0:
                setUpScreen.SetActive(true); 
                break;
            case 1:
                gameScene.SetActive(true);
                break;
            case 2:
                resultScreen.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// Deactivates the specified screen.
    /// <param name="sceneIndex">The index of the screen to deactivate.</param>
    void DeactivateScreen(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case 0:
                setUpScreen.SetActive(false);
                break;
            case 1:
                gameScene.SetActive(false);
                break;
            case 2:
                resultScreen.SetActive(false);
                break;
        }
    }
}
