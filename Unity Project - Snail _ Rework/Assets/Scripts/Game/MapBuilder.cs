using System.Collections;
using UnityEngine;
using Snails;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEditor;

/// <summary>
/// Class responsible for building and managing the game map.
/// </summary>
public class MapBuilder
{
    [Header("Tools")]
    AssetHolder assetHolder;
    SaveSystem saver;
    MapData activeMap;

    [Header("References")]
    Player[] players;
    GameObject mapHolder;
    GameData gameData;

    [Header("Values")]
    string savePath = Application.streamingAssetsPath + "/Maps/default.json";
    Tile[,] tiles;

    [Header("Visuals")]
    List<Highlight> spawnedHighlights;
    List<GameObject> spawnedMapAssets;

    public MapBuilder(AssetHolder assetHolder, GameObject mapHolder, GameData gameData)
    {
        players = new Player[2];
        this.assetHolder = assetHolder;
        saver = new SaveSystem();
        this.mapHolder = mapHolder;
        spawnedHighlights = new List<Highlight>();
        spawnedMapAssets = new List<GameObject>();
        this.gameData = gameData;
    }

    public Player[] GetPlayer()
    {
        return players;
    }


    /// <summary>
    /// Loads the map data from a file and constructs the game map.
    /// </summary>
    public void LoadAndBuildMap()
    {
       PlayerInformation playerInfo = gameData.GetPlayerInformation();
       activeMap = gameData.GetSelectedMap();
       
       tiles = new Tile[activeMap.size.x, activeMap.size.y];

        int counter = 0;

        for (int i = 0; i < activeMap.size.y; i++)
        {
            for (int j = 0; j < activeMap.size.x; j++)
            {
                Vector3 worldPosition = new Vector3(j, -i, 0);
                Vector2Int position = new Vector2Int(j, i);

                PlayerNumber playerNumber = PlayerNumber.Empty;
                TileState tileState = TileState.Default;

                //Spawn Grass
                GameObject grass = GameObject.Instantiate(assetHolder.grassField, worldPosition, Quaternion.Euler(Vector3.zero));
                grass.transform.SetParent(mapHolder.transform);
                grass.name = $"Grass {j}/{i}";
                spawnedMapAssets.Add(grass);

                switch (activeMap.contents[counter])
                {
                    case 64: //Spawn Impassables
                        GameObject impassable = GameObject.Instantiate(assetHolder.impassable, worldPosition, Quaternion.Euler(Vector3.zero));
                        impassable.transform.SetParent(grass.transform);
                        impassable.name = $"Impassable {j}/{i}";
                        spawnedMapAssets.Add(impassable);
                        tileState = TileState.Impassable;
                        break;
                    case 129:
                        SpawnPlayer(position, 0, playerInfo);
                        tileState = TileState.Snail;
                        playerNumber = PlayerNumber.PlayerOne;
                        break;
                    case 130:
                        SpawnPlayer(position, 1, playerInfo);
                        tileState = TileState.Snail;
                        playerNumber = PlayerNumber.PlayerTwo;
                        break;
                }

                tiles[j, i] = new Tile(position, worldPosition, playerNumber, tileState);
                counter++;
            }
        }
    }

    void SpawnPlayer(Vector2Int position, int index, PlayerInformation playerInfo)
    {
        PlayerVisual playerVisual = new PlayerVisual(assetHolder.player[index], assetHolder.slime[index]);
        players[index] = new Player(playerInfo.playernames[index], playerInfo.playerAgents[index],1, position, playerVisual, index, mapHolder);
    }

    public void  HighlightPassableTiles(int playerIndex, GameController gameController)
    {
        List<Tile> passableTiles = GetPassableTiles(players[playerIndex]);

        for (int i = 0; i < passableTiles.Count; i++)
        {
            GameObject highlightIntance = GameObject.Instantiate(assetHolder.highlight, passableTiles[i].worldPosition, Quaternion.Euler (Vector3.zero));
            highlightIntance.name = $"Highlight {passableTiles[i].position.x}/{passableTiles[i].position.y}";
            highlightIntance.transform.SetParent(mapHolder.transform);

            if (players[playerIndex].playerAgent == PlayerAgent.Human)
            {
                highlightIntance.AddComponent<BoxCollider2D>();
                spawnedHighlights.Add(highlightIntance.AddComponent<Highlight>());
                spawnedHighlights[i].InsertData(passableTiles[i].position, gameController, players[playerIndex]);
            }
        }

    }

    public List<Tile> GetPassableTiles(Player player)
    {
        List<Tile> neighbours = GetNeighbours(player.position);
        List<Tile> passables = new List<Tile>();
        for (int i= 0; i < neighbours.Count; i++)
        {
            if (neighbours[i].tileState == TileState.Default)
                passables.Add(neighbours[i]);
            else if (neighbours[i].checkSlime(player.index))
            {
                passables.Add(GetNextSlideTile(neighbours[i], player));
            }
        }
        return passables;
    }

    List <Tile> GetNeighbours(Vector2Int position) {

        List<Tile> neighbours = new List<Tile>();

        //Right Neighbour
        if (position.x + 1 <= activeMap.size.x-1)
            neighbours.Add(tiles[position.x + 1, position.y]);
        
        //Left Neighbour
        if (position.x - 1 >= 0)
            neighbours.Add(tiles[position.x - 1, position.y]);

        //Upper Neighbour
        if (position.y - 1 >=0)
            neighbours.Add(tiles[position.x, position.y-1]);

        //Lower Neighbour
        if (position.y + 1 <= activeMap.size.y-1)
            neighbours.Add(tiles[position.x, position.y + 1]);

        return neighbours;
    }

    /// <summary>
    /// Calculates the next tile to slide to from the given target tile for the specified player.
    /// </summary>
    /// <param name="target">The target tile to slide from.</param>
    /// <param name="player">The player for whom the slide is being calculated.</param>
    /// <returns>The next tile to slide to.</returns>
    Tile GetNextSlideTile(Tile target, Player player) {
       
        Vector2Int playerPosition = player.position;

        //Calculate direction where the slide goes
        Vector2Int direction = target.position-playerPosition;

        while (true)
        {
            Vector2Int newPosition = target.position + direction;
            if (checkValidity(newPosition))
            {
                Tile newTile = tiles[newPosition.x, newPosition.y];
                if (newTile.checkSlime(player.index))
                {
                    target = newTile;
                }
                else
                    break;
            }
            else
                break;
        }

        return target;
    }

    bool checkValidity(Vector2Int inputCoordinates)
    {
        if (inputCoordinates.x < activeMap.size.x
            && inputCoordinates.x>=0
            && inputCoordinates.y<activeMap.size.y
            && inputCoordinates.y>=0)
            return true;
        else
            return false;
    }

    public void DisableHighlights()
    {
        for (int i= 0; i<spawnedHighlights.Count; i++)
        {
            GameObject.Destroy(spawnedHighlights[i].gameObject);
        }
        spawnedHighlights.Clear();
    }

    /// <summary>
    /// Moves the player to the specified target position.
    /// </summary>
    /// <param name="targetPosition">The target position to move the player to.</param>
    /// <param name="playerIndex">The index of the player to move.</param>
    public IEnumerator MovePlayer(Vector2Int targetPosition, int playerIndex)
    {
        //Update Tile
        Tile targetTile = tiles[targetPosition.x, targetPosition.y];
        targetTile.SetPlayerNumber(playerIndex);
        targetTile.tileState = TileState.Snail;

        //Adjust previous Tile
        Player player = players[playerIndex];
        Vector2Int playerPositon = player.position;
        Tile playerPositonTile = tiles[playerPositon.x, playerPositon.y];
        playerPositonTile.tileState = TileState.Slime;
        player.SpawnSlimeVisuals(playerPositonTile, mapHolder);

        //Animated Player and Update Values
        yield return player.Move(targetPosition);
    }
  }

