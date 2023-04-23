using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoundButton : MonoBehaviour
{

    public void Aussetzen()
    {
        ActionInfo actionInfo = new ActionInfo(ActionType.skip, RoundManager.activePlayer());
        RoundManager.switchTurnsEvent(actionInfo);
        RoundManager.skipRound();
    }
}
