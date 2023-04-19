using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player
{
    public string name;
    public int score;
    public GameObject sprite;
    public Tile activeTile;
    public bool turn;
    public int turnsWithoutCapture;
    public enum Agent{computer,human }
    public Agent agent;

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
        turnsWithoutCapture = 0;
        tile.playerSlot = this;
    }

    public void increaseScore()
    {
        score++;
    }

    public void resetPlayer()
    {
        score = 0;
        UnityEngine.Object.Destroy(sprite);
        activeTile = null;
        turn = false;
        turnsWithoutCapture = 0;
    }


}
