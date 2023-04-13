using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    static float turnDurationCounter;
    float maxTurnDuration;

    private void Awake()
    {
        turnDurationCounter = 0;
        maxTurnDuration = PlayerSettingsManager.settings.maxTurnDuration;
        PlayerConfig.DetermineTurnOrder();
    }


    private void Update()
    {
       // countdownRoundSeconds();
    }

    public static void switchTurns()
    {
        turnDurationCounter = 0;

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
        Debug.Log("Switched Turns");

    }

    void countdownRoundSeconds() {
        turnDurationCounter += Time.deltaTime;
        if (turnDurationCounter > maxTurnDuration)
        {
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

    void stopCountRoundSeconds() { }
}
