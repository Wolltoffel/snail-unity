using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfig : MonoBehaviour
{

    public static List<Player> player;

    private void Awake()
    {
        player = new List<Player>();
        player.Add(new Player("Player 1"));
        player.Add(new Player("Player 2"));
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
        int random = Random.Range(0, 1);
        player[0].turn = random == 0 ? true : false;
        player[1].turn = random == 1 ? true : false;
    }

}
