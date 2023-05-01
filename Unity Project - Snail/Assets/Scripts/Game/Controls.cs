using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls
{
    ActivePlayerGiver activePlayerGiver;

    public Controls()
    {
        activePlayerGiver = new ActivePlayerGiver();
    }

    public void checkKeyInputs()
    {
        ActionInfo actionInfo;
        Player activePlayer = activePlayerGiver.giveActivePlayer();

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            actionInfo = new ActionInfo(ActionType.skip, activePlayer);
            handleInputs(null, actionInfo);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            actionInfo = new ActionInfo(ActionType.surrender, activePlayer);
            handleInputs(null, actionInfo);
        }
    }

    public void handleInputs(Tile tile, ActionInfo actionInfo)
    {
        GameManager.tryExecuteTurn(tile, actionInfo);
    }
}