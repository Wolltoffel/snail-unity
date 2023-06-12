using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the game flow and player turns.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header ("Tools")]
    [SerializeField] GameController gameController;
    [SerializeField] CameraManager cameraManager;
    MapBuilder mapBuilder;
    [SerializeField] ScreenHandler screenHandler;

    [Header ("Data")]
    [SerializeField] AssetHolder assetHolder;
    [SerializeField] GameObject mapHolder;
    [SerializeField] int maxRoundTime;
    [SerializeField] GameData gameData;

    [Header("InputSources")]
    [SerializeField] Button playButton;
    [SerializeField] Button replayButton;
    [SerializeField] Button continueButton;


    [Header("InternalVaraibles")]
    int activePlayerIndex;
    PlayerAction performedAction;
    bool gamehasEnded = true;
    int turnCounter=1;
    int winnerIndex = 100;

    /// <summary>
    /// Initializes the map builder and assigns the active player randomly.
    /// </summary>
    void Awake()
    {
        mapBuilder = new MapBuilder(assetHolder,mapHolder,gameData);
        activePlayerIndex = Random.Range(0, 2);//Determine first player
        
        //Add Button Listeners
        playButton.onClick.AddListener(PlayButtonClicked);
        continueButton.onClick.AddListener(ContinueGame);
        replayButton.onClick.AddListener(ReplayGame);
    }
    /// <summary>
    /// Starts the game flow coroutine.
    /// </summary>
    IEnumerator Start()
    {
        yield return new WaitUntil(()=> gamehasEnded == false);
        screenHandler.SetScreenActive(ScreenSelection.Game);
        mapBuilder.LoadAndBuildMap(gameController);
        cameraManager.SetUpCamera(gameData.GetSelectedMap().size);
        
        gameData.SetPlayerNamesHUD();
        gameData.SetTurnCounterHUD(turnCounter);
        gameData.SetPlayerScoresHUD(new int[2] { mapBuilder.GetPlayerScore(0), mapBuilder.GetPlayerScore(1) });

        while (!gamehasEnded)
        {
            //cameraManager.SetUpCamera();
            mapBuilder.HighlightPassableTiles(activePlayerIndex, gameController);
            yield return PlayTurn();
            mapBuilder.DisableHighlights();
            SwitchActivePlayer();
            turnCounter++;
            gameData.SetTurnCounterHUD(turnCounter);
            yield return gameData.AnnounceNextPlayerHUD(turnCounter, mapBuilder.GetPlayer(activePlayerIndex).name);
        }

        screenHandler.SetScreenActive(ScreenSelection.Results);

        if (winnerIndex== 0|| winnerIndex== 1) //Check if there is a winner
        {
            int loserIndex =
            winnerIndex == 0 ? 1 :
            winnerIndex == 1 ? 0
            : 100;

            gameData.SetWinner(mapBuilder.GetPlayer(winnerIndex), mapBuilder.GetPlayer(loserIndex));
            gameData.SetGameResultsHUD(turnCounter-1);
        }

        yield return null;
    }
    /// <summary>
    /// Plays a turn of the game.
    /// </summary>
    IEnumerator PlayTurn()
    {
        yield return WaitForPlayerMove();

        bool isSlideMove = false;
        isSlideMove = mapBuilder.CheckSlime(performedAction.position, activePlayerIndex);
        StartCoroutine(gameData.AnnouncePlayerActionHUD(mapBuilder.GetPlayer(activePlayerIndex).name,performedAction, isSlideMove));

        switch (performedAction.actionType)
        {
            case ActionType.Surrender:
                yield return null;
                break;
            case ActionType.Move:
                mapBuilder.DisableHighlights();
                yield return mapBuilder.MovePlayer(performedAction.position, activePlayerIndex);
                break;
            case ActionType.Skip:
                mapBuilder.SkipRound(activePlayerIndex);
                break;
        }

        gamehasEnded = CheckForEndedGame(out winnerIndex);
        
        performedAction = null; //ResetNextMoveForNextPlayer
        gameController.ResetAction();
    }

    /// <summary>
    /// Waits for the player to make a move or the round timer to expire.
    /// </summary>
    IEnumerator WaitForPlayerMove()
    {
        float timer = 0;

        while (timer <= maxRoundTime)
        {
            timer += Time.deltaTime;
            gameData.SetRoundTimerVisualHUD(maxRoundTime - timer);
            yield return null;
            performedAction = gameController.GetAction();
            if (performedAction != null)
            {
                gameData.SetPlayerScoresHUD(new int[2] { mapBuilder.GetPlayerScore(0), mapBuilder.GetPlayerScore(1) });
                yield break;
            }

           if (mapBuilder.CheckTurnsWithoutCapture(activePlayerIndex))
           {
                performedAction = new PlayerAction(ActionType.Skip, Vector2Int.zero);
                yield break;
           }
        }

            //In case there's no player action the round will be skipped for him
            performedAction = new PlayerAction(ActionType.Skip, Vector2Int.zero);
            yield return null;
    }

    /// <summary>
    /// Switches the active player and updates the HUD accordingly.
    /// </summary>
    public void SwitchActivePlayer()
    {
        if (activePlayerIndex == 0)
        {
            activePlayerIndex = 1;
            mapBuilder.SetSkipButtonActive(0, false);
            mapBuilder.SetSkipButtonActive(1, true);
        }

        else
        {
            activePlayerIndex = 0;
            mapBuilder.SetSkipButtonActive(0, true);
            mapBuilder.SetSkipButtonActive(1, false);
        }
    }
    
    /// <summary>
    /// Event handler for the play button click event. Starts the game.
    /// </summary>
    void PlayButtonClicked()
    {
        gamehasEnded = false;
    }

    /// <summary>
    /// Checks if the game has ended by evaluating different end conditions.
    /// </summary>
    /// <returns>True if the game has ended, false otherwise.</returns>
    bool CheckForEndedGame(out int winnerIndex) {

        winnerIndex = 100;
        if (performedAction.actionType == ActionType.Surrender)
        {
            winnerIndex = GetOtherPlayerIndex(activePlayerIndex);
            return true;
        }
        else if (mapBuilder.CheckIsMapFull()|| 
            mapBuilder.CheckTurnsWithoutCapture(0)&& mapBuilder.CheckTurnsWithoutCapture(1))
        {
            winnerIndex = mapBuilder.GetWinnerByScore();
            return true;
        }
           
        return false;
    }

    int GetOtherPlayerIndex(int thisPlayer)
    {
        if (thisPlayer == 0)
            return 1;
        if (thisPlayer == 1)
            return 0;
        else
            return 100;
    }

    void ResetGameManager()
    {
        activePlayerIndex = Random.Range(0, 2);//Determine first player
        performedAction = null;
        turnCounter = 1;
        winnerIndex = 100;
    }

    void ContinueGame()
    {
        mapBuilder.ResetMap();
        screenHandler.SetScreenActive(ScreenSelection.SetUp);
        ResetGameManager();
        StartCoroutine(Start());

    }

    void ReplayGame()
    {
        mapBuilder.ResetMap();
        ResetGameManager();
        StartCoroutine(Start());
        PlayButtonClicked();
    }



}
