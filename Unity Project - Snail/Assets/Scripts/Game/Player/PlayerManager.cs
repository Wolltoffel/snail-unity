using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{

    public static List<Player> player;

    private void Awake()
    {
        player = SetUpScreenData.setUpScreenData.givePlayers();

        RoundManager.switchTurn += switchTurns;
        GameManager.endGame += resetPlayers;
        DetermineTurnOrder();
    }


    public static void DetermineTurnOrder()
    {
        int random = UnityEngine.Random.Range(0, 2);
        player[0].turn = random == 0 ? true : false;
        player[1].turn = random == 1 ? true : false;
    }

    static void switchTurns(object sender, EventArgs e)
    {
        if (player[0].turn == false)
        {
            player[0].turn = true;
            player[1].turn = false;
        }
        else
        {
            player[0].turn = false;
            player[1].turn = true;
        }

    }

    public static void unsubscribeSwitchTurns()
    {
        RoundManager.switchTurn -= switchTurns;
    }

    void resetPlayers(object sender, EventArgs e) {
        foreach (Player player in player)
        {
            player.resetPlayer();
        }
        RoundManager.switchTurn -= switchTurns;
    }


    

}
