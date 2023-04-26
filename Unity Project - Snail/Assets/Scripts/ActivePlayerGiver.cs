using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerGiver
{
    List<Player> players;

    public ActivePlayerGiver() {
        players = PlayerManager.player;
    }
    public Player giveActivePlayer()
    {
        foreach (Player player in players)
        {
            if (player.turn == true)
            {
                return player;
            }
        }
        return giveActivePlayer();
    }
}
