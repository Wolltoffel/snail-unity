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
    public ActiveAgent agent;
    public int index;

    public Player(string name) 
    {
        this.name = name;
        RoundManager.switchTurn += switchTurn;
        score = 1;
    }

    public void setSlimeToStart()
    {
        Slime slime = new Slime(this, activeTile);
        activeTile.AddCustomSlime(slime,this);
    }

    public void move(Tile tile, ActionInfo actionInfo)
    {
        sprite.GetComponent<PlayerSprite>().startMove(tile,actionInfo.actionType);
        activeTile.playerSlot = null;
        activeTile = tile;
        if (actionInfo.actionType == ActionType.capture)
            turnsWithoutCapture=0;
        else
            turnsWithoutCapture++;
        if (turnsWithoutCapture >= PlayerSettingsManager.settings.maxTurnsWithoutCapture)
            GameManager.endGameplay(this, RoundManager.inactivePlayer(),ResultInfo.maxTurnsWithoutCaptureExceeded);

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

    public void captureField(Tile tile)
    {
        tile.AddSlime();
        increaseScore(); //Increase score if current field has no slime
    }


}
