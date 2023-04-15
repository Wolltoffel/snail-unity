using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour
{
    [SerializeField]Vector2 upperLeftCorner;
    static public int tileSize = 1;
    static public Vector2 centerPosition;
    static public Vector2 size;
    static public MapData activeMap;
    Object ground;
    Object impassable;
    static List<Object> slimeSprite;
    static Object highlight;
    public static List<Tile> highlightedTiles;
    List <Object> playerSprites;
    public static List<Tile> tiles;

    private void Awake()
    {
        tiles = new List<Tile>();
        loadSprites();
        loadMap();
        manageTiles();
    }

    void loadSprites()
    {
        ground = Resources.Load("Sprites/" + "ground");
        impassable = Resources.Load("Sprites/" + "impassable");
        highlight = Resources.Load("Sprites/" + "empty_256x256");
        playerSprites = new List<Object>();
        playerSprites.Add(Resources.Load("Sprites/" + "snail_blue_256x256"));
        playerSprites.Add(Resources.Load("Sprites/" + "snail_orange_256x256"));
        slimeSprite = new List<Object>();
        slimeSprite.Add(Resources.Load("Sprites/" + "trail_blue_256x256"));
        slimeSprite.Add(Resources.Load("Sprites/" + "trail_orange_256x256"));
    }

    void loadMap()
    {
       MapData activeMap= MapManager.selectedMap;
       //Load provisional Map
       int[] provisionalContents = new int[18];
       provisionalContents[4] = 64;
       provisionalContents[9] = 129;
       provisionalContents[11] = 130;
       //MapData activeMap = new MapData(new Vector2Int(6, 3), provisionalContents, "Provisional");

       int[] contents = activeMap.contents;
       size = activeMap.size;

       centerPosition = upperLeftCorner + new Vector2 (size.x/2,-size.y/2);

        Vector2 nextPosition = upperLeftCorner+new Vector2(0.5f*tileSize,-0.5f*tileSize);
        int rowCounter=0;
        int rowNumber = 1;

        for (int i = 0; i<contents.Length; i++)
        {
            //Spawn Ground and Add Tiles
            Vector3 position = new Vector3(nextPosition.x, nextPosition.y, 0);

            Tile newTile = new Tile(position, new Vector2(i - (rowNumber - 1) * size.x+1, rowNumber));

            newTile.grassField = Instantiate(ground, position,Quaternion.Euler(Vector3.zero)) as GameObject;
            newTile.grassField.name = $"Grass ({newTile.position.x}/{newTile.position.y})";
            InsertHighlight(newTile, newTile.grassField);

            switch (contents[i])
            {
                case 64://Spawn Impassable Rocks
                    newTile.impassable = Instantiate(impassable, position, Quaternion.Euler(Vector3.zero)) as  GameObject;
                    newTile.impassable.name = $"Impassable ({newTile.position.x}/{newTile.position.y})";
                    newTile.impassable.transform.parent = newTile.grassField.transform;
                    break;
                case 129: //Spawn Player 1
                    SpawnPlayer(newTile,0,position);
                    break;
                case 130: //Spawn Player 2
                    SpawnPlayer(newTile, 1, position);
                    break;
                default:
                    break;
            }

            //Add Tiles to List
            tiles.Add(newTile);

            nextPosition.x++;
            rowCounter++;

            //Switch Rows
            if (rowCounter >= size.x) {
                nextPosition.y--;
                nextPosition.x = upperLeftCorner.x+0.5f*tileSize;
                rowCounter = 0;
                rowNumber++;
            }

        }

    }

    void SpawnPlayer(Tile newTile, int index, Vector3 position)
    {
         newTile.playerSlot = PlayerConfig.player[index] == null ? new Player("Player "+index) : PlayerConfig.player[index];
         newTile.playerSlot.sprite = Instantiate(playerSprites[index], position, Quaternion.Euler(Vector3.zero)) as GameObject;
         newTile.playerSlot.sprite.GetComponent<SpriteRenderer>().sortingOrder = 2;
         newTile.playerSlot.sprite.transform.parent = newTile.grassField.transform;
         newTile.playerSlot.sprite.name = $"{newTile.playerSlot.name} Sprite";
         newTile.playerSlot.activeTile = newTile;
    }

    void manageTiles()
    {
        foreach (Tile tile in tiles)
        {
            foreach (Tile item in tiles)
            {
                Vector2 right = tile.position + Vector2.right;
                Vector2 left = tile.position + Vector2.left;
                Vector2 down = tile.position + Vector2.down;
                Vector2 up = tile.position + Vector2.up;

                if (tile.right == null && item.position == right)
                {
                    tile.right = item;
                    item.left = tile;
                }
                else if (tile.left == null && item.position == left)
                {
                    tile.left = item;
                    item.right = tile;
                }
                else if (tile.down == null && item.position == down)
                {
                    tile.down = item;
                    item.up = tile;
                }
                else if (tile.up == null && item.position == up)
                {
                    tile.up = item;
                    item.down = tile;
                }
            }
        }

    }

    public static void InsertHighlight(Tile tile, GameObject parent)
    {
        tile.highLightSlot = Instantiate(highlight, tile.worldPosition, Quaternion.Euler(Vector3.zero))as GameObject;
        tile.highLightSlot.name = $"Highlight ({tile.position.x}/{tile.position.y})";
        tile.highLightSlot.SetActive(false);
        tile.highLightSlot.AddComponent<BoxCollider2D>();
        tile.highLightSlot.AddComponent<HighLight>();
        tile.highLightSlot.GetComponent<HighLight>().tile = tile;
        tile.highLightSlot.transform.parent = parent.transform;
    }

    public static void markPassableFields(Tile currentTile, Player player)
    {
        List<Tile> passableTiles = checkPassableTiles(currentTile, player);
        highlightedTiles = passableTiles;
        foreach (Tile tile in passableTiles)
        {
            tile.setHighlight(true);
        }
    }

    private static List<Tile> checkPassableTiles(Tile currentTile, Player player)
    {
        List<Tile> proxomityTiles = new List<Tile>();
        List<Tile> freeTiles = new List<Tile>();

        if(currentTile.left!=null)
            proxomityTiles.Add(currentTile.left);
        if (currentTile.right != null)
            proxomityTiles.Add(currentTile.right);
        if (currentTile.up != null)
            proxomityTiles.Add(currentTile.up);
        if (currentTile.down != null)
            proxomityTiles.Add(currentTile.down);

        foreach (Tile tile in proxomityTiles)
        {
            if (tile != null)
            {
                if (tile.checkPassable(player))
                    freeTiles.Add(tile);
            }
        }
        return freeTiles;
    }

    public static void AddSlime(Tile tile, Player player)
    {
        Slime slime = new Slime(player,tile);
        slime.instance = Instantiate(slimeSprite[RoundManager.activePlayerIndex()], tile.worldPosition, Quaternion.Euler(Vector3.zero)) as GameObject;
        tile.slime = slime;
    }
}
