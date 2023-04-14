using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public Vector2 position;
    public Vector3 worldPosition;
    public Player playerSlot;
    public Tile left, right, up, down;
    public GameObject highLightSlot;
    public GameObject grassField;
    public GameObject impassable;
    public Slime slime;

    public Tile(Vector3 worldPosition,Vector2 position) {
        this.position = position;
        this.worldPosition = worldPosition;
    }

    public bool checkPassable(Player player)
    {
        if (impassable==null
            &&slime == null
           && playerSlot == null)
        { return true; }
        else if (slime != null)
        {
            if (slime.owner == player)
            {
                return true;
            }
            else { return false; }
        }
        else
        { return false; }
    }

    public void setHighlight(bool b) 
    {
        highLightSlot.SetActive(b);
    }

    public bool checkSlime(Player player) {
        if (slime != null) {
            if (slime.owner == player)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    public Tile giveNextSlideTile(Tile adjacentTile, Player player)
    {
        if (right != null)
        {
             if (right == adjacentTile && adjacentTile.right.checkSlime(player))
            return adjacentTile.right.right;
        }
        if (left != null)
        {
            if (left == adjacentTile && adjacentTile.left.checkSlime(player))
                return adjacentTile.left.left;
        }
        if(up!=null){
            if (up == adjacentTile && adjacentTile.up.checkSlime(player))
                return up.up;
        }
        if (down != null)
        {
            if (down == adjacentTile && adjacentTile.down.checkSlime(player))
                return adjacentTile.down.down;
        }

       return null;
    }

}



