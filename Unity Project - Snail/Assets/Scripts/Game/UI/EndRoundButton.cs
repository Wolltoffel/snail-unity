using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoundButton : MonoBehaviour
{

    public void Aussetzen()
    {
        RoundManager.switchTurns();
    }
}
