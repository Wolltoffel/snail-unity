using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowTurnNumber : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        updateTurnNumber(null, null);
        RoundManager.switchTurn += updateTurnNumber;
    }

    public void updateTurnNumber(object o, ActionInfo actionInfo)
    {
        textMeshProUGUI.text = $"Turn {RoundManager.turnCounter}";
    }
}
