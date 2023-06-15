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


    /// <summary>
    /// Inserts the necessary data for the GameController to function properly.
    /// </summary>
    /// <param name="players">An array of Player objects representing the game players.</param>
    /// <param name="highlights">A List of Highlight objects representing the highlights in the game.</param>
    /// <param name="surrenderButton">The Button used for surrendering.</param>
    /// <param name="skipRoundButton">The Button used for skipping rounds.</param>
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
