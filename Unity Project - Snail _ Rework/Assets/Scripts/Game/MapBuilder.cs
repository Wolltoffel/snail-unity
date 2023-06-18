using System.Collections;
using UnityEngine;
using Snails;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEditor;
using JetBrains.Annotations;
using System;
using UnityEngine.UIElements;

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


    /// <summary>
    /// Initializes a new instance of the <see cref="MapBuilder"/> class.
    /// </summary>
    /// <param name="assetHolder">The asset holder.</param>
    /// <param name="mapHolder">The map holder.</param>
    /// <param name="gameData">The game data.</param>
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

    /// <summary>
    /// Gets the player at the specified index.
    /// </summary>
    public Player GetPlayer(int playerIndex)
    {
        return players[playerIndex];
    }

    /// <summary>
    /// Gets the reference to the player's score at the specified index.
    /// </summary>
    public ref int GetPlayerScore(int playerIndex)
    {
        return ref players[playerIndex].score;
    }


    /// <summary>
    /// Loads the map data from a file and constructs the game map.
    /// </summary>
    public void LoadAndBuildMap(GameController gameController)
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
                    case 129: //Spawn Player One
                        SpawnPlayer(position, 0, playerInfo, gameController);
                        tileState = TileState.Snail;
                        playerNumber = PlayerNumber.PlayerOne;
                        break;
                    case 130: //Spawn Player Two
                        SpawnPlayer(position, 1, playerInfo , gameController);
                        tileState = TileState.Snail;
                        playerNumber = PlayerNumber.PlayerTwo;
                        break;
                }

                tiles[j, i] = new Tile(position, worldPosition, playerNumber, tileState);
                counter++;
            }
        }
    }

    void SpawnPlayer(Vector2Int position, int index, PlayerInformation playerInfo, GameController gameController)
    {
        PlayerVisual playerVisual = new PlayerVisual(assetHolder.player[index], assetHolder.slime[index]);
        players[index] = new Player(playerInfo.playernames[index], playerInfo.playerAgents[index],1, position, playerVisual, index, mapHolder,gameController);
    }

    /// <summary>
    /// Highlights the passable tiles for the specified player.
    /// </summary>
    /// <param name="playerIndex">The index of the player.</param>
    /// <param name="gameController">The game controller.</param>
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
            }
            spawnedHighlights.Add(highlightIntance.AddComponent<Highlight>());
            spawnedHighlights[i].InsertData(passableTiles[i].position, gameController, players[playerIndex]);
            spawnedHighlights[i].InsertData(passableTiles[i].position, gameController, players[playerIndex]);
        }

    }

    /// <summary>
    /// Gets the list of passable tiles for the specified player.
    /// </summary>
    public List<Tile> GetPassableTiles(Player player)
    {
        List<Tile> neighbours = GetNeighbours(player.position);
        List<Tile> passables = new List<Tile>();
        for (int i= 0; i < neighbours.Count; i++)
        {
            if (neighbours[i].tileState == TileState.Default)
                passables.Add(neighbours[i]);
            else if (neighbours[i].CheckSlime(player.index))
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

            // Check if the new position is within the field boundaries
            if (CheckFieldBoundaries(newPosition))
            {
                Tile newTile = tiles[newPosition.x, newPosition.y];

                // If the new tile has slime, update the target tile and continue sliding
                if (newTile.CheckSlime(player.index))   
                    target = newTile;
                else
                    break;
            }
            // Break the loop if the new position is outside the field boundaries
            else
                break;
        }

        return target;
    }


    /// <summary>
    /// Checks if the input coordinates are within the field boundaries.
    /// </summary>
    bool CheckFieldBoundaries(Vector2Int inputCoordinates)
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
        Player player = players[playerIndex];

        //Check if target has Slime
        if (tiles[targetPosition.x, targetPosition.y].CheckSlime(playerIndex))
            players[playerIndex].IncreaseTurnsWithoutCapture();
        else
        {
            player.IncreaseScore();
        }
           
        //Update Tile
        Tile targetTile = tiles[targetPosition.x, targetPosition.y];
        targetTile.SetPlayerNumber(playerIndex);
        targetTile.tileState = TileState.Snail;

        //Adjust previous Tile
        Vector2Int playerPositon = player.position;
        Tile playerPositonTile = tiles[playerPositon.x, playerPositon.y];
        if (!playerPositonTile.CheckSlime(playerIndex) && playerPositonTile.spawnedSlime == false)
        {
            player.SpawnSlimeVisuals(playerPositonTile, mapHolder);
            playerPositonTile.spawnedSlime = true;
        }

        playerPositonTile.tileState = TileState.Slime;

        //Animated Player and Update Values
        yield return player.Move(targetPosition);
    }

    public bool CheckSlime(Vector2Int targetPosition, int playerIndex)
    {
        return tiles[targetPosition.x, targetPosition.y].CheckSlime(playerIndex);
    }

    public bool CheckIsMapFull()
    {
        for (int i = 0; i < activeMap.size.y; i++)
        {
            for (int j = 0; j < activeMap.size.x; j++)
            {
                if (tiles[j, i].tileState == TileState.Default)
                    return false;
            }
        }
        return true;
    }

    public bool CheckTurnsWithoutCapture(int playerIndex)
    {
        if (players[playerIndex].turnsWithoutCapture>=GameData.playersettings.maxTurnsWithoutCapture) 
            return true;   
        
        return false;
    }

    public void SkipRound(int playerIndex)
    {
        players[playerIndex].IncreaseTurnsWithoutCapture();
    }

    public void SetSkipButtonActive(int activePlayerIndex, bool active)
    {
        players[activePlayerIndex].SetSkipButtonActive(active);
    }

    public int GetWinnerByScore()
    {
        if (players[0].score > players[1].score)
            return 0;
        if (players[0].score < players[1].score)
            return 1;
        else
            return 100;
    }

    public void ResetMap()
    {
        for (int i = 0;i < spawnedMapAssets.Count; i++)
        {
            GameObject.Destroy(spawnedMapAssets[i]);
        }
        spawnedMapAssets.Clear();

        for (int i = 0; i < players.Length; i++)
        {
            players[i].Reset();
        }
    }

    /// <summary>
    /// Runs the AI agent for the specified active player.
    /// </summary>
    public IEnumerator RunAIAgent(int activePlayerIndex,int turnNumber, GameController gameController)
    {
        AIAgent aiAgent = new AIAgent(activePlayerIndex);

        // Run the AI agent's logic
        yield return aiAgent.Run(activeMap.size,turnNumber, tiles);
        AIAction aiAction = aiAgent.GetAIAction();

        // Convert the AI action to a player action based on the active player's position
        PlayerAction playerAction = aiAction.ConvertToPlayerAction(players[activePlayerIndex].position);

        // Check if the player action is valid and set it as the game action
        if (CheckPlayerActionValidity(playerAction))
            gameController.SetAction(playerAction);
        else
        {
            // If the player action is not valid, set a default action of skipping the turn
            gameController.SetAction(new PlayerAction(ActionType.Skip, Vector2Int.zero));
        }
       
    }

    /// <summary>
    /// Checks the validity of a player action.
    /// </summary>
    bool CheckPlayerActionValidity (PlayerAction playerAction)
    {
        // Check if the position matches with one of the spawned highlights.
        // This validation works because the highlights represent the only possible positions for the snail to move to.
        for (int i = 0; i<spawnedHighlights.Count;i++)
        {
            if (spawnedHighlights[i].GetPosition() == playerAction.position)
                return true;
        }

        return false;
    }



}

