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
    static ActivePlayerGiver activePlayerGiver;

    private void Awake()
    {
        turnDurationCounter = 0;
        turnCounter = 1;
        maxTurnDuration = PlayerSettingsManager.settings.maxTurnDuration;
        switchTurn += resetCounter;
        GameManager.endGame += resetManager;
        activePlayerGiver = new ActivePlayerGiver();
        Player player = activePlayerGiver.giveActivePlayer();
    }

    private void Update()
    {
        countdownRoundSeconds();
    }

    public static void switchTurnsEvent(ActionInfo actionInfo)
    {
        switchTurn?.Invoke(null, actionInfo);
    }

    private void roundManager_switchTurn(object sender, ActionInfo e)
    {
        throw new NotImplementedException();
    }

    public static void resetCounter(object sender, ActionInfo e)
    {
        turnCounter++;
        turnDurationCounter = 0;
    }

    void countdownRoundSeconds() {
        turnDurationCounter += Time.deltaTime;
        if (turnDurationCounter > maxTurnDuration)
        {
            ActionInfo actionInfo = new ActionInfo(ActionType.skip,activePlayerGiver.giveActivePlayer());
            switchTurn?.Invoke(null, actionInfo);
        }
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

    public static void skipRound()
    {
        Player activePlayer = activePlayerGiver.giveActivePlayer();
        int turnsWithoutCapture = activePlayer.turnsWithoutCapture;
        turnsWithoutCapture++;
        activePlayer.turnsWithoutCapture = turnsWithoutCapture;
        if (turnsWithoutCapture >= PlayerSettingsManager.settings.maxTurnsWithoutCapture)
        {
            PlayerManager.unsubscribeSwitchTurns();
        }
    }

    private void resetManager(object sender, EventArgs e)
    {
        turnCounter = 0;
        turnDurationCounter = 0;
        switchTurn -= resetCounter;
        GameManager.endGame -= resetManager;
    }


}
