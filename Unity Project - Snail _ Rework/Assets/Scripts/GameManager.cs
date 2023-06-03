using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

/// <summary>
/// Manages the game flow and player turns.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] AssetHolder assetHolder;
    [SerializeField] int roundTimer;
    [SerializeField] GameObject mapHolder;
    MapBuilder mapBuilder;
    int activePlayerIndex;
    PlayerAction nextMove;
    bool gamehasEnded;

    void Awake()
    {
        mapBuilder = new MapBuilder(assetHolder,mapHolder, this);

        //Determine first player
        activePlayerIndex = Random.Range(0, 2);
        mapBuilder.LoadMapFromFile();

    }

   IEnumerator Start()
    {
        while (!gamehasEnded)
        {
            mapBuilder.HighlightPassableTiles(activePlayerIndex);
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

        nextMove = null;
    }

    /// <summary>
    /// Waits for the player to make a move or the round timer to expire.
    /// </summary>
    IEnumerator WaitForPlayerMove() {

        float timer = 0;
        while (timer<=roundTimer) {
            timer += Time.deltaTime;
            //Debug.Log(timer);
            yield return null;
            if (nextMove != null)
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

    public void MakeMove(PlayerAction newMove)
    {
        nextMove = newMove;
    }

    public void SwitchActivePlayer()
    {
        if (activePlayerIndex == 0)
            activePlayerIndex = 1;
        else
            activePlayerIndex = 0;
    }


}
