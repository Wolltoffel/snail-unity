using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLight : MonoBehaviour
{
    [HideInInspector]public Tile tile;
    Vector3 initialSize;
    AIHandler aiHandler;

    bool activeMovement;
    ActivePlayerGiver activePlayerGiver;

    bool slideMove;


    private void Awake()
    {
        initialSize = transform.localScale;
        aiHandler = new AIHandler();
        activePlayerGiver = new ActivePlayerGiver();
    }

    private void Update()
    {
        Player activePlayer = activePlayerGiver.giveActivePlayer();
        if (activePlayer.agent == Player.Agent.computer)
        {
            Vector2Int nextMove = aiHandler.giveNextMove(MapBuilder.arrayTiles, (uint)activePlayer.index);
            ActionInfo actionInfo = new ActionInfo(ActionType.capture, activePlayer);
            Controls controls = new Controls();
            controls.handleInputs(MapBuilder.arrayTiles[nextMove.x,nextMove.y], actionInfo);
        }

    }

    public void switchToSlideMode()
    {
        slideMove = enabled;
    }

    void turnOffSlideMode()
    {
        slideMove = false;
    }

    private void OnMouseEnter()
    {
        Player player = activePlayerGiver.giveActivePlayer();
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
        turnOffSlideMode();
        transform.localScale = initialSize;
    }

    private void OnMouseDown()
    {
        Player player = activePlayerGiver.giveActivePlayer();
        if (player.agent == Player.Agent.human)
        {
            ActionInfo actionInfo;
            if (slideMove)
                actionInfo = new ActionInfo(ActionType.slide, player);
            else
                actionInfo = new ActionInfo(ActionType.capture, player);
            Controls controls = new Controls();
            controls.handleInputs(tile,actionInfo);
        }
    }
}
