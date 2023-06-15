using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Represents player information including names and agents.
/// </summary>
public class PlayerInformation
{
    public string[] playernames;
    public PlayerAgent[] playerAgents;


    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInformation"/> class with the specified player names and agents.
    /// </summary>
    /// <param name="playernames">An array of player names.</param>
    /// <param name="activeAgents">An array of player agents.</param>
    public PlayerInformation(string[] playernames, PlayerAgent[] activeAgents)
    {
        this.playernames = playernames;
        this.playerAgents = activeAgents;
    }

    /// <summary>
    /// Gets the default player information.
    /// </summary>
    /// <returns>The default <see cref="PlayerInformation"/> object.</returns>
    public static PlayerInformation Default()
    {
        string[] playernames = new string[2] {"Player 1","Player 2"};
        PlayerAgent[] playerAgents = new PlayerAgent[2] { PlayerAgent.Human, PlayerAgent.Human };
        return new PlayerInformation(playernames, playerAgents);
    }
}

/// <summary>
/// Manages game data including setup, in-game data, and UI elements.
/// </summary>
public class GameData : MonoBehaviour
{
    [Header("SetUpData")]
    MapData selectedMap;
    List<MapData> selectableMaps;
    public static PlayerSettings playersettings;
    PlayerInformation playerInfo;

    [Header("InGameData")]
    Highscore highScore;

    [Header("Tools")]
    MapLoader mapLoader;
    PlayerSettingsManager playerSettingsManager;
    HighscoreManager highscoreManager;

    [Header ("InGameHUD")]
    [Space(5)]
    [SerializeField]GameDataVisual[] playerNameVisual = new GameDataVisual[2];
    [SerializeField] GameDataVisual[] playerScoreVisual = new GameDataVisual[2];
    [SerializeField] GameDataVisual roundTimerVisual;
    [SerializeField] GameDataVisual turnCounterVisual;
    [SerializeField] TurnAnnouncer turnAnnouncer;

    [Header("ResultScreen")]
    [SerializeField] GameDataVisual resultsVisual;
    [SerializeField] HighscoreTable highscoreTable;

    [Header ("Buttons")]
    [SerializeField]Button resetSettingsButton;

    public void Awake()
    {
        mapLoader = new MapLoader();
        selectableMaps = mapLoader.LoadMaps();
        playerSettingsManager = new PlayerSettingsManager();
        playersettings = playerSettingsManager.settings;
        playerInfo = PlayerInformation.Default();
        resetSettingsButton.onClick.AddListener(ResetPlayerSettings);
    }


    public void SetPlayerName(string playername, int playerIndex)
    {
        playerInfo.playernames[playerIndex] = playername;
    }

    public string GetPlayerName(int playerIndex) {
        return playerInfo.playernames[playerIndex];
    }

    public void SetPlayerAgent(PlayerAgent agent, int playerIndex)
    {
        playerInfo.playerAgents[playerIndex] = agent;
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
        highscoreManager = new HighscoreManager(selectedMap.name);
    }

    /// <summary>
    /// Checks the validity of the map.
    /// </summary>
    /// <param name="map">The map to check.</param>
    /// <returns>A string indicating the validity of the map.</returns>
    public string checkMapValidity(MapData map) {
        return mapLoader.CheckMapValidity(map); 
    }

    /// <summary>
    /// Saves the winner and loser inside a highscore file.
    /// </summary>
    /// <param name="winner">The winning player.</param>
    /// <param name="loser">The losing player.</param>
    public void SetWinner(Player winner, Player loser)
    {
        highScore = new Highscore(selectedMap.name, 
            winner.name, loser.name, 
            winner.score, loser.score, 
            winner.playerAgent,loser.playerAgent);
    }

    #region GameScreen HUD
    public void SetPlayerNamesHUD()
    {
        playerNameVisual[0].SetVisual(playerInfo.playernames[0]);
        playerNameVisual[1].SetVisual(playerInfo.playernames[1]);
    }

    public void SetPlayerScoresHUD(int[] playerScores)
    {
        playerScoreVisual[0].SetVisual(playerScores[0]);
        playerScoreVisual[1].SetVisual(playerScores[1]);
    }

    public void SetTurnCounterHUD(int turnCounter)
    {
        turnCounterVisual.SetVisual(turnCounter);
    }

    public void SetRoundTimerVisualHUD(float roundCounter)
    {
        roundTimerVisual.SetVisual(roundCounter);
    }

    public IEnumerator AnnouncePlayerActionHUD(string playerName,PlayerAction playerAction,bool isSlideMove)
    {
        string announcement = playerName+": ";

        switch (playerAction.actionType)
        {
            case ActionType.Move:
                announcement += isSlideMove == true ? "Slide to " : isSlideMove == false ? "Capture " : "";
                announcement += playerAction.position;
                break;
            case ActionType.Skip:
                announcement+= "Miss";
                break;
            case ActionType.Surrender:
                yield break;
        }

        yield return turnAnnouncer.Announce(announcement, 1);
    }

    /// <summary>
    /// Announces the next player's turn in the HUD.
    /// </summary>
    /// <param name="turnNumber">The current turn number.</param>
    /// <param name="playerName">The name of the next player.</param>
    /// <returns>An <see cref="IEnumerator"/> for coroutine.</returns>
    public IEnumerator AnnounceNextPlayerHUD(int turnNumber, string playerName)
    {
        string announcement = playerName + $" Round {turnNumber} - {playerName}";
        yield return turnAnnouncer.Announce(announcement, 1);
    }

    public void ColorActivePlayerHUD(int activePlayerIndex, int otherPlayerIndex)
    {
        playerNameVisual[activePlayerIndex].SetColor(Color.red);
        playerNameVisual[otherPlayerIndex].SetDefaultColor();   
    }


    #endregion

    #region ResultScreen HUD
    public void SetGameResultsHUD(int lastTurnNumber)
    {
        string results = $"{highScore.winnerName} \n " +
            $"{highScore.winnerScore} : {highScore.loserScore} \n " +
            $"{lastTurnNumber} Turns";

        resultsVisual.SetVisual(results);
    }

    /// <summary>
    /// Shows the high scores in the result screen HUD.
    /// </summary>
    public void ShowHighScoresHUD() {

        // Attempt to add the current high score to the high score data
        highscoreManager.AttemptToAddToHighscoreData(highScore);
        
        // Retrieve the updated high score data
        HighscoreData highscoreData = highscoreManager.GetHighscoreData();

        // Set the heading of the high score table to the selected map
        highscoreTable.SetHeading(selectedMap);


        for (int i = 0;i<highscoreData.highscores.Length; i++)
        {
            // Check if the high score entry has a non-zero winner score
            if (highscoreData.highscores[i].winnerScore > 0)
            {
                // The current high score entry matches the new high score, so mark it as a new high score
                if (ReferenceEquals (highscoreData.highscores[i],highScore))
                    highscoreTable.AddHighScore(highscoreData.highscores[i], true);
                else
                {
                    // The current high score entry is not the new high score
                    highscoreTable.AddHighScore(highscoreData.highscores[i], false);
                }
            }
        }
    }

    #endregion


    public void ResetPlayerSettings()
    {
        playersettings.LoadDefaultValues();
    }
}
