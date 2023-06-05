using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation
{
    public string[] playernames;
    public PlayerAgent[] playerAgents;

    public PlayerInformation(string[] playernames, PlayerAgent[] activeAgents)
    {
        this.playernames = playernames;
        this.playerAgents = activeAgents;
    }

    public static PlayerInformation Default()
    {
        string[] playernames = new string[2] {"Player 1","Player 2"};
        PlayerAgent[] playerAgents = new PlayerAgent[2] { PlayerAgent.Human, PlayerAgent.Human };
        return new PlayerInformation(playernames, playerAgents);
    }
}


public class GameData : MonoBehaviour
{
    [Header("SetUpData")]
    MapData selectedMap;
    List<MapData> selectableMaps;
    public static PlayerSettings playersettings;
    PlayerInformation playerInfo;

    [Header("InGameData")]
    int roundTimer;
    int turnNumber;
    PlayerAction lastPeformedAction;

    [Header("Tools")]
    MapLoader mapLoader;
    PlayerSettingsManager playerSettingsManager;

    public void Awake()
    {
        mapLoader = new MapLoader();
        selectableMaps = mapLoader.loadMaps();
        playerSettingsManager = new PlayerSettingsManager();
        playersettings = playerSettingsManager.settings;
        playerInfo = PlayerInformation.Default();
    }

    public void SetPlayerName(string playername, int playerIndex)
    {
        playerInfo.playernames[playerIndex] = playername;
    }

    public string GetPlayerName(int playerIndex) {
        return playerInfo.playernames[playerIndex];
    }

    public PlayerInformation GetPlayerInformation()
    {
        return playerInfo;
    }

    public List<MapData> GetSelectableMaps(){
        return selectableMaps;
    }
    public MapData GetSelectedMap()
    {
        return selectedMap;
    }

    public void SetSelectedMap(MapData selectedMap)
    {
        this.selectedMap = selectedMap;
    }

    public string checkMapValidity(MapData map) {
        return mapLoader.checkMapValidity(map); 
    }

    public void SetRoundTimer(ref int roundTimer)
    {
        this.roundTimer = roundTimer;
    }

    public int GetRoundTimer()
    {
        return roundTimer;
    }
}
