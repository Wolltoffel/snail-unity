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
        left = null;
        right = null;
        up = null;
        down = null;
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
        if (right != null && adjacentTile.right!=null )
        {
            if (right == adjacentTile && adjacentTile.right.checkSlime(player) && adjacentTile.right.right != null)
                return adjacentTile.right.right;
        }
        if (left != null && adjacentTile.left!= null)
        {
            if (left == adjacentTile && adjacentTile.left.checkSlime(player) && adjacentTile.left.left != null)
                return adjacentTile.left.left;
        }
        if(up!=null && adjacentTile.up != null )
        {
            if (up == adjacentTile && adjacentTile.up.checkSlime(player) && adjacentTile.up.up != null)
                return adjacentTile.up.up;
        }
        if (down != null && adjacentTile.down != null )
        {
            if (down == adjacentTile && adjacentTile.down.checkSlime(player) && adjacentTile.down.down != null)
                return adjacentTile.down.down;
        }

       return null;
    }

}



