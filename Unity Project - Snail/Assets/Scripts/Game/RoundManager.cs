using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    float turnDurationCounter;
    float maxTurnDuration;

    private void Awake()
    {
        turnDurationCounter = 0;
        maxTurnDuration = PlayerSettingsManager.settings.maxTurnDuration;
        PlayerConfig.DetermineTurnOrder();
    }


    private void Update()
    {
        countdownRoundSeconds();
    }

    void switchTurns()
    {
        if (PlayerConfig.player[0].turn == false)
        {
            PlayerConfig.player[0].turn = true;
            PlayerConfig.player[1].turn = false;
        }
        else
        {
            PlayerConfig.player[0].turn = false;
            PlayerConfig.player[1].turn = true;
        }
    }

    void countdownRoundSeconds() {
        turnDurationCounter += Time.deltaTime;

        if (turnDurationCounter > maxTurnDuration)
        {
            turnDurationCounter = 0;
            switchTurns();
        }
    }

    public static Player activePlayer()
    {
        foreach(Player player in PlayerConfig.player)
        {
            if (player.turn == true)
            {
                return player;
            }
        }
        return null;
    }

    void stopCountRoundSeconds() { }
}
