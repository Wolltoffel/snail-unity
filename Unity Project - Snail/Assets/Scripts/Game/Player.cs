using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string name;
    public int score;
    public List<Vector2> possibleFields;
    public List<Vector2> slimeFields;
    public GameObject sprite;
    public Tile activeTile;
    public bool turn;
    public enum Agent{computer,human }
    public Agent agent; 

    public Player(string name) 
    {
        this.name = name;
        agent = Agent.human;
    }

}
