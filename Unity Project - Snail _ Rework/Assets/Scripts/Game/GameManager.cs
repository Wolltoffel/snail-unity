using System.Collections;
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
    [SerializeField] int roundTimer;
    [SerializeField] GameData gameData;

    [Header("InputSources")]
    [SerializeField] Button playButton;


    [Header("InternalVaraibles")]

    int activePlayerIndex;
    PlayerAction nextMove;
    bool gamehasEnded = true;


    void Awake()
    {
        mapBuilder = new MapBuilder(assetHolder,mapHolder,gameData);
        activePlayerIndex = Random.Range(0, 2);//Determine first player
        playButton.onClick.AddListener(PlayButtonClicked);
    }

   IEnumerator Start()
    {
        yield return new WaitUntil(()=> gamehasEnded == false);
        screenHandler.SetScreenActive(ScreenSelection.Game);
        mapBuilder.LoadAndBuildMap();
        cameraManager.SetUpCamera(gameData.GetSelectedMap().size);

        while (!gamehasEnded)
        {
            //cameraManager.SetUpCamera();
            mapBuilder.HighlightPassableTiles(activePlayerIndex, gameController);
            yield return PlayTurn();
            mapBuilder.DisableHighlights();
            SwitchActivePlayer();
        }

        yield return null;
    }

    IEnumerator PlayTurn()
    {
        yield return WaitForPlayerMove();
        switch (nextMove.move)
        {
            case ActionType.Surrender:
                yield return Surrender();
                break;
            case ActionType.Move:
                mapBuilder.DisableHighlights();
                yield return mapBuilder.MovePlayer(nextMove.position, activePlayerIndex);
                break;
            case ActionType.Skip:
                SwitchActivePlayer();
                break;  
        }

        nextMove = null; //ResetNextMoveForNextPlayer
        gameController.ResetAction();
    }

    /// <summary>
    /// Waits for the player to make a move or the round timer to expire.
    /// </summary>w
    IEnumerator WaitForPlayerMove() {

        float timer = 0;
        while (timer<=roundTimer) {
            timer += Time.deltaTime;
            //Debug.Log(timer);
            yield return null;
            nextMove = gameController.GetAction();
            if (nextMove!=null)
                yield break;
        }
        //In case there's no player action the round will be skipped for him
        nextMove = new PlayerAction(ActionType.Skip, Vector2Int.zero);
        yield return null;

    }
    
    IEnumerator Surrender()
    {
        yield return null;
    }

    public void SwitchActivePlayer()
    {
        if (activePlayerIndex == 0)
            activePlayerIndex = 1;
        else
            activePlayerIndex = 0;
    }

    void PlayButtonClicked()
    {
        gamehasEnded = false;
    }


}
