using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLight : MonoBehaviour
{
    [HideInInspector]public Tile tile;
    Vector3 initialSize;
    AIHandler aiHandler;

    bool activeMovement;


    private void Awake()
    {
        initialSize = transform.localScale;
        aiHandler = new AIHandler();
    }

    private void Update()
    {
        Player activePlayer = RoundManager.activePlayer();
        if (activePlayer.agent == Player.Agent.computer)
        {
            Vector2Int nextMove = aiHandler.giveNextMove(MapBuilder.arrayTiles, (uint)RoundManager.activePlayer().index);
            ActionInfo actionInfo = new ActionInfo(ActionType.capture, activePlayer);
            GameManager.excuteTurn(activePlayer, MapBuilder.arrayTiles[nextMove.x,nextMove.y], actionInfo);
        }

    }

    private void OnMouseEnter()
    {
        Player player = RoundManager.activePlayer();
        if (player.agent == Player.Agent.human)
        {
            SoundManager.soundManager.PlaySound("ui-click-high-modern-click-06");
            transform.localScale = transform.localScale * 1.1f;
        }
    }

    private void OnMouseExit()
    {
        transform.localScale = initialSize;
    }

    private void OnDisable()
    {
        transform.localScale = initialSize;
    }

    private void OnMouseDown()
    {
        Player player = RoundManager.activePlayer();
        if (player.agent == Player.Agent.human)
        {
            ActionInfo actionInfo = new ActionInfo(ActionType.capture, player);
            GameManager.excuteTurn(player, tile, actionInfo);
        }
    }
}
