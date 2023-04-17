using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public Vector2 position;
    public Vector3 worldPosition;
    public Player playerSlot;
    public Tile left, right, up, down;
    public GameObject highLightSlot, grassFieldSlot, impassableSlot;
    public Slime slime;
    static AssetLoader spriteLoader;
    public Tile(Vector3 worldPosition,Vector2 position) {
        this.position = position;
        this.worldPosition = worldPosition;
        spriteLoader = new AssetLoader();
    }

    public void insertContent(int index) {
        spawnGrass();
        insertHighlight();
        switch (index)
        {
            case 64://Spawn Impassable Rocks
                spawnImpassable();
                break;
            case 129: //Spawn Player 1
                spawnPlayer(0);
                break;
            case 130: //Spawn Player 2
                spawnPlayer(1);
                break;
            default:
                break;
        }
    }

    void spawnGrass()
    {
        grassFieldSlot = GameObject.Instantiate(spriteLoader.ground, worldPosition, Quaternion.Euler(Vector3.zero)) as GameObject;
        grassFieldSlot.name = $"Grass ({position.x}/{position.y})";
    }

    void spawnPlayer(int index)
    {
        playerSlot = PlayerConfig.player[index] == null ? new Player("Player " + index) : PlayerConfig.player[index];
        playerSlot.sprite = GameObject.Instantiate(spriteLoader.player[index], worldPosition, Quaternion.Euler(Vector3.zero)) as GameObject;
        playerSlot.sprite.GetComponent<SpriteRenderer>().sortingOrder = 2;
        playerSlot.sprite.transform.parent = grassFieldSlot.transform;
        playerSlot.sprite.name = $"{playerSlot.name} Sprite";
        playerSlot.sprite.AddComponent<PlayerSprite>();
        playerSlot.activeTile = this;
    }

    void spawnImpassable()
    {
        impassableSlot = GameObject.Instantiate(spriteLoader.impassable, worldPosition, Quaternion.Euler(Vector3.zero)) as GameObject;
        impassableSlot.name = $"Impassable ({position.x}/{position.y})";
        impassableSlot.transform.parent = grassFieldSlot.transform;
    }

    void insertHighlight()
    {
        highLightSlot = GameObject.Instantiate(spriteLoader.highlight, worldPosition, Quaternion.Euler(Vector3.zero)) as GameObject;
        highLightSlot.name = $"Highlight ({position.x}/{position.y})";
        highLightSlot.SetActive(false);
        highLightSlot.AddComponent<BoxCollider2D>();
        highLightSlot.AddComponent<HighLight>();
        highLightSlot.GetComponent<HighLight>().tile = this;
        highLightSlot.transform.parent = grassFieldSlot.transform;
    }

    public bool checkPassable(Player player)
    {
        if (impassableSlot == null && slime == null && playerSlot == null)
        {
            return true;
        }
        else if (slime != null && slime.owner == player)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void setHighlight(bool b) 
    {
        highLightSlot.SetActive(b);
    }

    public void AddSlime()
    {
        Slime slime = new Slime(RoundManager.activePlayer(), this);
        slime.instance = GameObject.Instantiate(spriteLoader.slime[RoundManager.activePlayerIndex()], worldPosition, Quaternion.Euler(Vector3.zero)) as GameObject;
        this.slime = slime;
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
        Tile[] currentTiles = new Tile[] {left,right,up,down};
        Tile[] adjacentTiles = new Tile[] {adjacentTile.left, adjacentTile.right, adjacentTile.up, adjacentTile.down};

        for (int i= 0; i < currentTiles.Length; i++)
        {
            if(currentTiles[i] == adjacentTile && adjacentTiles[i] != null)
            {
                if (adjacentTiles[i].checkSlime(player))
                    return adjacentTiles[i];
            }
        }
        return null;
    }

    public void emptyTile()
    {
      Object.Destroy(highLightSlot);
      Object.Destroy(grassFieldSlot);
      Object.Destroy(impassableSlot);
     }

   

}



