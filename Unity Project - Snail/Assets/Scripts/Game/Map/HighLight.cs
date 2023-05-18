using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighLight : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [HideInInspector]public Tile tile;
    Vector3 initialSize;
    AIHandler aiHandler;
    bool calculatingMove;

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
        if (activePlayer.agent == ActiveAgent.computer)
        {
            if (calculatingMove == false)
            {
                StartCoroutine(aiHandler.calculateNextMove());
                calculatingMove = true;
            }
            
            Vector2Int nextMove = aiHandler.giveNextMove(MapBuilder.arrayTiles, (uint)activePlayer.index);
            
            if (calculatingMove && aiHandler.giveNextMoveTask().IsCompleted) {
                if (nextMove != activePlayer.activeTile.position)
                {
                    ActionInfo actionInfo = new ActionInfo(ActionType.capture, activePlayer);
                    Controls controls = new Controls();
                    controls.handleInputs(MapBuilder.arrayTiles[nextMove.x, nextMove.y], actionInfo);
                    Debug.Log(nextMove);
                    calculatingMove = false;
                }
            }
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

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Player player = activePlayerGiver.giveActivePlayer();
        if (player.agent == ActiveAgent.human && Controls.giveControlsActive())
        {
            SoundManager.soundManager.PlaySound("ui-click-high-modern-click-06");
            transform.localScale = transform.localScale * 1.1f;
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        transform.localScale = initialSize;
    }

    private void OnDisable()
    {
        turnOffSlideMode();
        transform.localScale = initialSize;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Player player = activePlayerGiver.giveActivePlayer();
        if (player.agent ==ActiveAgent.human)
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
