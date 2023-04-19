using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime
{
    public Player owner;
    public Tile tile;
    public GameObject instance;

    public Slime(Player owner, Tile tile)
    {
        this.owner = owner;
        this.tile = tile;
    }
}
