using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Announcement : MonoBehaviour
{
    void Start()
    {
        RoundManager.switchTurn += makeAnnouncementAfterTurn;
    }
    void makeAnnouncementAfterTurn(object sender, ActionInfo actionInfo)
    {
        Player activePlayer = actionInfo.player;
        string activePlayerName = activePlayer.name;
        string text="";

        switch (actionInfo.action)
        {
            case ActionInfo.Action.slide:
                text = $"{activePlayerName} slides to {activePlayer.activeTile.position.x}/{activePlayer.activeTile.position.y}";
                break;
            case ActionInfo.Action.capture:
                text = $"{activePlayerName} captured {activePlayer.activeTile.position.x}/{activePlayer.activeTile.position.y}";
                break;
            case ActionInfo.Action.skip:
                text = $"{activePlayerName} missed his turn";
                break;
        }

        if (text != "")
        {
            PopUpManager popUpManager = new PopUpManager("PopUpTemplates/PopUp_Template_2");
            popUpManager.showPopUp(text, 0.3f);
        }

    }

    public void makeAnnouncementBeforeTurn()
    {
        string text = $"Round: {RoundManager.turnCounter} - {RoundManager.activePlayer()}";

        PopUpManager popUpManager = new PopUpManager("PopUpTemplates/PopUp_Template_2");
        popUpManager.showPopUp(text, 0.3f);
    }

}
