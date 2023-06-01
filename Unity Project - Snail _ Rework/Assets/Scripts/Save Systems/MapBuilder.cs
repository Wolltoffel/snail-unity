using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snails;

public class MapBuilder
{
    [Header ("Tools")]
    AssetHolder assetHolder;
    SaveSystem saver;
    MapData activeMap;

    [Header ("References")]
    Player[] player;
    GameObject mapParent;
    List<Highlight> spawnedHighlights;

    [Header ("Values")]
    string savePath = Application.streamingAssetsPath + "/Maps/default.json";
    Tile[,] tiles;
    
    public MapBuilder(AssetHolder assetHolder)
    {
        player = new Player[2];
        this.assetHolder = assetHolder;
        saver = new SaveSystem();
    }

   public void LoadMapFromFile()
    {
       activeMap =  saver.LoadData<MapData>(savePath);
       tiles = new Tile[activeMap.size.x, activeMap.size.y];

       int counter = 0;

        for (int i= 0; i < activeMap.size.y;i++)
        {
            for (int j = 0; j < activeMap.size.x; j++)
            {
                Vector3 worldPosition = new Vector3(j, -i, 0);
                Vector2Int position = new Vector2Int(j, i);

                PlayerNumber playerNumber = PlayerNumber.Empty;
                TileState tileState = TileState.Default;

                //Spawn Grass
                GameObject grass = GameObject.Instantiate(assetHolder.grassField, worldPosition, Quaternion.Euler(Vector3.zero));
                grass.transform.SetParent(mapParent.transform);

                //SpawnHighlights
                GameObject highlight = GameObject.Instantiate(assetHolder.highlight, worldPosition, Quaternion.Euler(Vector3.zero));
                highlight.transform.SetParent(mapParent.transform);
                highlight.AddComponent<Highlight>();

                switch (activeMap.contents[counter])
                {
                    case 64: //Spawn Impassables
                        GameObject impassable = GameObject.Instantiate(assetHolder.impassable, worldPosition, Quaternion.Euler(Vector3.zero));
                        impassable.transform.SetParent(grass.transform);
                        tileState = TileState.Impassable;
                        break;
                    case 129:
                        SpawnPlayer(position,0);
                        tileState = TileState.Snail;
                        playerNumber = PlayerNumber.PlayerOne;
                        break;
                    case 130:
                        SpawnPlayer(position, 1);
                        tileState = TileState.Snail;
                        playerNumber = PlayerNumber.PlayerTwo;
                        break;
                }

                tiles[j, i] = new Tile(position, PlayerNumber.Empty, TileState.Default);
                counter++;
            }
        }
    }

    void SpawnPlayer(Vector2Int position, int index)
    {
        PlayerVisual playerVisual = new PlayerVisual(assetHolder.player[index], assetHolder.player[index]);
        player[index] = new Player(1, position, playerVisual, index,mapParent);
    }

    public void markPassableFields(Player player, int playerIndex)
    {
        List<Tile> neighbours = giveNeighbours(player.position);
        List<Tile> highlight = new List<Tile>();
        for (int i= 0; i < neighbours.Count; i++)
        {
            if (neighbours[i].tileState == TileState.Default)
                highlight.Add(neighbours[i]);
            else if (neighbours[i].tileState == TileState.Slime)
            {
                if (playerIndex == 0 && neighbours[i].playerNumber==PlayerNumber.PlayerOne) 
                    highlight.Add(neighbours[i]);
                else if
                {

                }
            }

        }
    }

    List <Tile> giveNeighbours(Vector2Int position) {

        List<Tile> neighbours = new List<Tile>();

        //Right Neighbour
        if (position.x + 1 <= activeMap.size.x-1)
            neighbours.Add(tiles[position.x + 1, position.y]);
        
        //Left Neighbour
        if (position.x - 1 >= 0)
            neighbours.Add(tiles[position.x - 1, position.y]);

        //Upper Neighbour
        if (position.y - 1 >0)
            neighbours.Add(tiles[position.x, position.y-1]);

        //Lower Neighbour
        if (position.y + 1 >= activeMap.size.y-1)
            neighbours.Add(tiles[position.x, position.y + 1]);

        return neighbours;
    }

    public IEnumerator MovePlayer(Vector2Int target, int playerIndex)
    {
        yield return player[playerIndex].Move(target);
    }

    public void SpawnSlime(int playerIndex,Vector3 worldPosition, GameObject grassParent)
    {
        player[playerIndex].SpawnSlime(worldPosition, grassParent);
    }
  }
