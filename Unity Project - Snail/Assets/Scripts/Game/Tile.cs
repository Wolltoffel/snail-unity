using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public Vector2 position;
    public Vector3 worldPosition;
    public bool generallyPassable;
    public Player slimeOwner;
    public Player playerSlot;

    public Tile(Vector2 position, Vector3 worldPosition, bool generallyPassable)
    {
        this.position = position;
        this.worldPosition = worldPosition;
        this.generallyPassable = generallyPassable;
        slimeOwner = null;
    }

    public void AddSlime(Player player) {
        slimeOwner = player;
    }
}



