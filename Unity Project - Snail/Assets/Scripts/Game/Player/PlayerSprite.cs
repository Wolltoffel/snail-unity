using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSprite : MonoBehaviour
{
    bool activateMovement;
    bool endedMovement;
    Tile target;
    public float movementSpeed = 3f;
    [HideInInspector] public Player player;
    ActionType actionType;
    ActivePlayerGiver activePlayerGiver;

    private void Start()
    {
        activePlayerGiver = new ActivePlayerGiver();
    }

    private void Update()
    {
        if (activateMovement)
            move();
        else if (endedMovement)
            endMove(actionType);
    }

    public void startMove(Tile target, ActionType actionType)
    {
        activateMovement = true;
        this.target = target;
        this.actionType = actionType;
    }

    public void endMove(ActionType actionType)
    {
        ActionInfo actionInfo = new ActionInfo(actionType, activePlayerGiver.giveActivePlayer());
        RoundManager.switchTurnsEvent(actionInfo);
        endedMovement = false;
        player.hideSlime();
    }

    void move()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = target.worldPosition;
        if (Vector3.Distance(currentPosition, targetPosition) > 0.0001f)
        {
            Vector3 direction = (targetPosition - currentPosition).normalized;
            transform.Translate(direction * movementSpeed * MapBuilder.tileSize * Time.deltaTime);
        }
        else
        {
            activateMovement = false;
            endedMovement = true;
        }
    }

    private void OnMouseDown()
    {
        Controls controls = new Controls();
        ActionInfo actionInfo = new ActionInfo(ActionType.skip, player);
        controls.handleInputs(null, actionInfo);
    }
}