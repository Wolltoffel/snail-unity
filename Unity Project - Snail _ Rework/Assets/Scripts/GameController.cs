using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    Player[] players;
    List<Highlight> highlights;
    
    Button surrenderButton;
    Button skipRoundButton;

    PlayerAction action;

    public void InsertData(Player[] players, List<Highlight> highlights, Button surrenderButton, Button skipRoundButton)
    {
        this.players = players;
        this.highlights = highlights;
        this.surrenderButton = surrenderButton;
        this.skipRoundButton = skipRoundButton;
    }

    public void SetAction(PlayerAction inputAction)
    {
        this.action = inputAction;
    }

    public PlayerAction GetAction()
    {
        return action;
    }

    public void ResetAction()
    {
        action = null;
    }
 


}
