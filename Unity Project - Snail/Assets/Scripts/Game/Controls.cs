using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls
{
    public Controls() { }
    public void checkInputs()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //Skips one Round
            ActionInfo actionInfo = new ActionInfo(ActionType.skip, RoundManager.activePlayer());
            RoundManager.switchTurnsEvent(actionInfo);
            RoundManager.skipRound();
        }
        if (Input.GetKeyDown(KeyCode.Escape)){

            Player winner = RoundManager.activePlayer();
            Player loser = RoundManager.inactivePlayer();
            GameManager.EndGame(winner,loser);
        }

    }

    public static void handleOnMouseInputs(Player player)
    {
        if (RoundManager.activePlayer() == player)
        {
            //Skips one Round
            ActionInfo actionInfo = new ActionInfo(ActionType.skip, RoundManager.activePlayer());
            RoundManager.switchTurnsEvent(actionInfo);
            RoundManager.skipRound();
        }
    }
}
