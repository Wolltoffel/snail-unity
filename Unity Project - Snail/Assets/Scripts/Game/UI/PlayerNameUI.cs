using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerNameUI : MonoBehaviour
{
    [SerializeField] int playerIndex;
    [SerializeField] Color highlightColor;
    Color defaultColor;
    Player player;
    TextMeshProUGUI tmpProText;

    void Start() 
    {
        player = PlayerManager.player[playerIndex];
        tmpProText = GetComponent<TextMeshProUGUI>();
        tmpProText.text = player.name;
        defaultColor = tmpProText.color;
        RoundManager.switchTurn += changeColor;
        changeColor(null, EventArgs.Empty);
    }

    void changeColor(object sender, EventArgs e)
    {
        if (player.turn)
            tmpProText.color = highlightColor;
        else
            tmpProText.color = defaultColor;
    }
}
