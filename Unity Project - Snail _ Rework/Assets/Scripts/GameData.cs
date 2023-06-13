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
        highscoreManager = new HighscoreManager(selectedMap.name);
    }

    public string checkMapValidity(MapData map) {
        return mapLoader.checkMapValidity(map); 
    }

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

    public IEnumerator AnnounceNextPlayerHUD(int turnNumber, string playerName)
    {
        string announcement = playerName + $" Round {turnNumber} - {playerName}";
        yield return turnAnnouncer.Announce(announcement, 1);
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

    public void ShowHighScoresHUD() {

        bool newHighscore = highscoreManager.AttemptToAddToHighscoreData(highScore);

        HighscoreData highscoreData = highscoreManager.GetHighscoreData();
        highscoreTable.SetHeading(selectedMap);


        for (int i = 0;i<highscoreData.highscores.Length; i++)
        {

            if (highscoreData.highscores[i].winnerScore > 0)
            {
                highscoreTable.AddHighScore(highscoreData.highscores[i],false);
            }
        }

    }

    #endregion


}
