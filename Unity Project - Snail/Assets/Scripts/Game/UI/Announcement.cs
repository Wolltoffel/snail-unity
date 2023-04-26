using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Announcement : MonoBehaviour
{
    ActivePlayerGiver activePlayerGiver;

    void Start()
    {
        RoundManager.switchTurn += makeAnnouncementAfterTurn;
    }
    void makeAnnouncementAfterTurn(object sender, ActionInfo actionInfo)
    {
        Player activePlayer = actionInfo.player;
        string activePlayerName = activePlayer.name;
        string text="";

        switch (actionInfo.actionType)
        {
            case ActionType.slide:
                text = $"{activePlayerName} slides to {activePlayer.activeTile.position.x}/{activePlayer.activeTile.position.y}";
                break;
            case ActionType.capture:
                text = $"{activePlayerName} captured {activePlayer.activeTile.position.x}/{activePlayer.activeTile.position.y}";
                break;
            case ActionType.skip:
                text = $"{activePlayerName} missed his turn";
                break;
        }

        if (text != "")
        {
            PopUpManager popUpManager = new PopUpManager("PopUpTemplates/PopUp_Template_2");
            popUpManager.showPopUp(text, 0.3f);
        }

    }

    public PopUpManager makeAnnouncementBeforeTurn(float time)
    {
        string text = $"Round: {RoundManager.turnCounter} - {activePlayerGiver.giveActivePlayer()}";

        PopUpManager popUpManager = new PopUpManager("PopUpTemplates/PopUp_Template_2");
        popUpManager.showPopUp(text, time);
        return popUpManager;
    }

    void initiateSwitchTurns()
    {
        float time = 0.3f;
        PopUpManager popUpManager = makeAnnouncementBeforeTurn(time);
    }

}
