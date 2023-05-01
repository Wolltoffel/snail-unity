using UnityEngine;

public class Tile
{
    public Vector2Int position;
    public Vector3 worldPosition;
    public Player playerSlot;

    public GameObject highLightSlot, grassFieldSlot, impassableSlot;
    public Slime slime;
    static AssetLoader spriteLoader;

    private ActivePlayerGiver activePlayerGiver;

    public Tile(Vector3 worldPosition, Vector2Int position)
    {
        this.position = position;
        this.worldPosition = worldPosition;
        spriteLoader = new AssetLoader();
        activePlayerGiver = new ActivePlayerGiver();
    }

    public void insertContent(int index)
    {
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
        playerSlot = SetUpScreenData.setUpScreenData.players[index];
        playerSlot.sprite = GameObject.Instantiate(spriteLoader.player[index], worldPosition, Quaternion.Euler(Vector3.zero)) as GameObject;
        playerSlot.sprite.GetComponent<SpriteRenderer>().sortingOrder = 2;
        playerSlot.sprite.transform.parent = grassFieldSlot.transform;
        playerSlot.sprite.name = $"{playerSlot.name} Sprite";
        playerSlot.sprite.AddComponent<PlayerSprite>();
        playerSlot.sprite.GetComponent<PlayerSprite>().player = playerSlot;
        playerSlot.sprite.AddComponent<BoxCollider2D>();
        playerSlot.activeTile = this;
        playerSlot.setSlimeToStart();
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
        Player activePlayer = activePlayerGiver.giveActivePlayer();
        Slime slime = new Slime(activePlayer, this);
        GameObject slimeInstance;
        slimeInstance = GameObject.Instantiate(spriteLoader.slime[activePlayer.index], worldPosition, Quaternion.Euler(Vector3.zero)) as GameObject;
        slimeInstance.name = $"Slime{position.x} {position.y}";
        slimeInstance.transform.parent = grassFieldSlot.transform;
        slime.instance = slimeInstance;
        this.slime = slime;
    }

    public void AddCustomSlime(Slime slime, Player player)
    {
        slime.instance = GameObject.Instantiate(spriteLoader.slime[player.index], worldPosition, Quaternion.Euler(Vector3.zero)) as GameObject;
        this.slime = slime;
    }

    public bool checkSlime(Player player)
    {
        if (slime != null)
        {
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

    public void emptyTile()
    {
        Object.Destroy(highLightSlot);
        Object.Destroy(grassFieldSlot);
        Object.Destroy(impassableSlot);
    }




}