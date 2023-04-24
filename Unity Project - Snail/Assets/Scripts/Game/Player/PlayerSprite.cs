using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    bool activateMovement;
    bool endedMovement;
    Tile target;
    public float movementSpeed = 3f;
    [HideInInspector]public Player player;

    ActionType actionType;

    private void Update()
    {
        if (activateMovement)
            move();
        else if (endedMovement)
            endMove();
    }

    public void startMove(Tile target)
    {
        activateMovement = true;
        this.target = target;
    }

    public void endMove()
    {
        ActionInfo actionInfo = new ActionInfo(actionType, RoundManager.activePlayer());
        RoundManager.switchTurnsEvent(actionInfo);
        endedMovement = false;
    }

    void move()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = target.worldPosition;
        if (Vector3.Distance(currentPosition, targetPosition) > 0.0001f)
        {
            Vector3 direction = (targetPosition - currentPosition).normalized;
            transform.Translate(direction*movementSpeed*MapBuilder.tileSize*Time.deltaTime); 
        }
        else
        {
            activateMovement = false;
            endedMovement = true;
        }
    }

    private void OnMouseDown()
    {
        Controls.handleOnMouseInputs(player);
    }
}
