using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenSelection
{
    SetUp,Game,Results
}

public class ScreenHandler : MonoBehaviour
{
    [SerializeField]GameObject setUpScreen;
    [SerializeField]GameObject gameScene;
    [SerializeField]GameObject resultScreen;


    public void Awake()
    {
        SetScreenActive(ScreenSelection.SetUp);
    }

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
