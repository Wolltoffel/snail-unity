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
        activePlayerGiver = new ActivePlayerGiver();
        GameManager.endGame += unsubscribeFromEvents;
    }
    void makeAnnouncementAfterTurn(object sender, ActionInfo actionInfo)
    {
        Player activePlayer = actionInfo.player;
        string activePlayerName = activePlayer.name;
        string textAfterTurn="";
        string textBeforeNextTurn;

        switch (actionInfo.actionType)
        {
            case ActionType.slide:
                textAfterTurn = $"{activePlayerName} slides to {activePlayer.activeTile.position.x}/{activePlayer.activeTile.position.y}";
                break;
            case ActionType.capture:
                textAfterTurn = $"{activePlayerName} captured {activePlayer.activeTile.position.x}/{activePlayer.activeTile.position.y}";
                break;
            case ActionType.skip:
                textAfterTurn = $"{activePlayerName} missed his turn";
                break;
        }

        textBeforeNextTurn = $"Round: {RoundManager.turnCounter} - {activePlayerGiver.giveActivePlayer().name}";

        if (textAfterTurn != "")
        {
            PopUpManager popUpManager= new PopUpManager("PopUpTemplates/PopUp_Template_2");
            StartCoroutine (popUpManager.showPopUp(textAfterTurn, 0.3f, popUpManager.showPopUp(textBeforeNextTurn,0.5f)));
        }

    }

    void unsubscribeFromEvents(object sender,StatData stats)
    {
        RoundManager.switchTurn -= makeAnnouncementAfterTurn;
        GameManager.endGame -= unsubscribeFromEvents;
    }

}
