using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string name;
    public int score;
    public GameObject sprite;
    public Tile activeTile;
    public bool turn;
    public enum Agent{computer,human }
    public Agent agent;
    public int speed;

    bool moving;

    public Player(string name) 
    {
        this.name = name;
        agent = Agent.human;
    }

    public void move(Tile tile)
    {
        sprite.GetComponent<PlayerSprite>().startMove(tile);
        activeTile.playerSlot = null;
        activeTile.AddSlime();
        activeTile = tile;
        tile.playerSlot = this;
    }


}
