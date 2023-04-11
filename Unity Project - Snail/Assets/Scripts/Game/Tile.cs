using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public Vector2 position;
    public Vector3 worldPosition;
    public Player slimeOwner;
    public Player playerSlot;
    public Tile left, right, up, down;
    public GameObject highLightSlot;
    public GameObject grassField;
    public GameObject impassable;
    public GameObject slime;

    public Tile(Vector3 worldPosition,Vector2 position) {
        this.position = position;
        this.worldPosition = worldPosition;
    }

    public void AddSlime(Player player) {
        slimeOwner = player;
    }

    public bool checkTile(Player player)
    {
        if (impassable==null
            &&slimeOwner == null
            && playerSlot != player)
        { return true; }
        else
        { return false; }
    }

    public void setHighlight(bool b) 
    {
        highLightSlot.SetActive(b);
    }

}



