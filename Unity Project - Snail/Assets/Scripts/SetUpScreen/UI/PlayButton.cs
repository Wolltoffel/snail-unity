using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{

    public void StartGame()
    {
        SetUpScreenData.setUpScreenData.insertPlayers();
        SceneManager.LoadScene(sceneName: "Game");
    }
    
}
