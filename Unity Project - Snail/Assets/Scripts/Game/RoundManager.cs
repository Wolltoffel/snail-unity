using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoundManager : MonoBehaviour
{
    public static float turnDurationCounter;
    float maxTurnDuration;
    public static event EventHandler<ActionInfo> switchTurn;



    private void Awake()
    {
        turnDurationCounter = 0;
        maxTurnDuration = PlayerSettingsManager.settings.maxTurnDuration;
        PlayerConfig.DetermineTurnOrder();
        switchTurn += resetCounter;
    }

    private void Update()
    {
       countdownRoundSeconds();
    }

    public static void switchTurnsEvent(ActionInfo actionInfo)
    {
        switchTurn?.Invoke(null, actionInfo);
    }

    public static void resetCounter(object sender, EventArgs e)
    {
        turnDurationCounter = 0;
    }

    void countdownRoundSeconds() {
        turnDurationCounter += Time.deltaTime;
        if (turnDurationCounter > maxTurnDuration)
        {
            ActionInfo actionInfo = new ActionInfo(ActionInfo.Action.skip);
            switchTurn?.Invoke(null, actionInfo );
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

    public static int activePlayerIndex()
    {
        List<Player> player = PlayerConfig.player;

        for (int i= 0;i<player.Count;i++)
        {
            if (player[i].turn == true)
            {
                return i;
            }
        }
        return 0;
    }

    public static void skipRound()
    {
        int turnsWithoutCapture = RoundManager.activePlayer().turnsWithoutCapture;
        turnsWithoutCapture++;
        RoundManager.activePlayer().turnsWithoutCapture = turnsWithoutCapture;
        if (turnsWithoutCapture >= PlayerSettingsManager.settings.maxTurnsWithoutCapture)
        {
            PlayerConfig.unsubscribeSwitchTurns();
        }
    }


}
