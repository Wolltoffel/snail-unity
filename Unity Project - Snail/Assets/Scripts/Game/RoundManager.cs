using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoundManager : MonoBehaviour
{
    public static float turnDurationCounter;
    float maxTurnDuration;
    public static event EventHandler<ActionInfo> switchTurn;
    public static int turnCounter;

    private void Awake()
    {
        turnDurationCounter = 0;
        maxTurnDuration = PlayerSettingsManager.settings.maxTurnDuration;
        switchTurn += resetCounter;
        GameManager.endGame += ResetManager;
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
        turnCounter++;
        turnDurationCounter = 0;
    }

    void countdownRoundSeconds() {
        turnDurationCounter += Time.deltaTime;
        if (turnDurationCounter > maxTurnDuration)
        {
            ActionInfo actionInfo = new ActionInfo(ActionInfo.Action.skip,activePlayer());
            switchTurn?.Invoke(null, actionInfo );
        }
    }

    public static Player activePlayer()
    {
        foreach(Player player in PlayerManager.player)
        {
            if (player.turn == true)
            {
                return player;
            }
        }
        return activePlayer();
    }

    public static Player inactivePlayer()
    {
        foreach (Player player in PlayerManager.player)
        {
            if (player.turn == false)
            {
                return player;
            }
        }
        return inactivePlayer();
    }

    public static int activePlayerIndex()
    {
        List<Player> player = PlayerManager.player;

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
            PlayerManager.unsubscribeSwitchTurns();
        }
    }

    private void ResetManager(object sender, EventArgs e)
    {
        turnCounter = 0;
        turnDurationCounter = 0;
        switchTurn -= resetCounter;
        GameManager.endGame -= ResetManager;
    }


}
