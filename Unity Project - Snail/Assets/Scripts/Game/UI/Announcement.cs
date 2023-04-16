using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Announcement : MonoBehaviour
{
    TextMeshProUGUI textMesh;

    void Start()
    {
        RoundManager.switchTurn += makeAnnouncement;
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        
    }

    void makeAnnouncement(object sender, ActionInfo actionInfo)
    {
        Player activePlayer = actionInfo.player;
        string activePlayerName = activePlayer.name;
        string text="";

        switch (actionInfo.action)
        {
            case ActionInfo.Action.slide:
                text = $"{activePlayerName}: Slide to {activePlayer.activeTile.position.x} {activePlayer.activeTile.position.y}";
                break;
            case ActionInfo.Action.capture:
                text = $"{activePlayerName}: Capture {activePlayer.activeTile.position.x} {activePlayer.activeTile.position.y}";
                break;
            case ActionInfo.Action.skip:
                text = $"{activePlayerName}: Miss";
                break;
        }

        textMesh.text = text;
    }

}
