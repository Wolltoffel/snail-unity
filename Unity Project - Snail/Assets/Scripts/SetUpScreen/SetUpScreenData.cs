using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpScreenData : MonoBehaviour
{
    public static SetUpScreenData setUpScreenData;
    private static List<Player> players;

    private void Awake()
    {
        if (setUpScreenData != null && setUpScreenData != this)
            Destroy(this);
        else
            setUpScreenData = this;

        insertPlayers();
    }

    public void insertPlayers()
    {
        if (players == null)
        {
            players = new List<Player>();
            players.Add(new Player("Player 1"));
            players.Add(new Player("Player 2"));
        }

        foreach (Player item in players)
        {
            item.index = players.IndexOf(item);
        }
    }

    public void setName(string name, int index)
    {
        players[index].name = name;
    }

    public void setAgent(ActiveAgent agent, int index)
    {
        players[index].agent = agent;
    }

    public List<Player> givePlayers()
    {
        return players;
    }
}
