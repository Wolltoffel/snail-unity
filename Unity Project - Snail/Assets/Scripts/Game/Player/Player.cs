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
    public int index;

    bool moving;

    public Player(string name) 
    {
        this.name = name;
        RoundManager.switchTurn += switchTurn;
    }

    public void setSlimeToStart()
    {
        Slime slime = new Slime(this, activeTile);
        activeTile.AddCustomSlime(slime,this);
    }

    public void move(Tile tile, ActionInfo actionInfo)
    {
        sprite.GetComponent<PlayerSprite>().startMove(tile);
        activeTile.playerSlot = null;
        activeTile = tile;
        if (actionInfo.actionType == ActionType.capture)
            turnsWithoutCapture=0;
        else
            turnsWithoutCapture++;
        if (turnsWithoutCapture >= PlayerSettingsManager.settings.maxTurnsWithoutCapture)
            GameManager.EndGame(this, RoundManager.inactivePlayer());

        tile.playerSlot = this;
    }

    public void increaseScore()
    {
        score++;
    }

    void switchTurn(object sender, ActionInfo e)
    {
        if (e.actionType == ActionType.skip)
            turnsWithoutCapture++;
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
