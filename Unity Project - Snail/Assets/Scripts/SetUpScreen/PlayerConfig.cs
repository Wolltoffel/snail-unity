using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerConfig : MonoBehaviour
{

    public static List<Player> player;

    private void Awake()
    {
        player = new List<Player>();
        player.Add(new Player("Player 1"));
        player.Add(new Player("Player 2"));
        RoundManager.switchTurn += SwitchTurns;
    }

    public static void SetName(string name,int index)
    {
        player[index].name = name;
    }

    public static void SetAgent(Player.Agent agent,int index)
    {
        player[index].agent = agent;
    }

    public static void DetermineTurnOrder()
    {
        int random = UnityEngine.Random.Range(0, 1);
        player[0].turn = random == 0 ? true : false;
        player[1].turn = random == 1 ? true : false;
    }

    static void SwitchTurns(object sender, EventArgs e)
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
        RoundManager.switchTurn -= SwitchTurns;
    }


    

}
