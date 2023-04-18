using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    bool activateMovement;
    Tile target;
    public float movementSpeed = 3f;

    private void Update()
    {
        if (activateMovement)
        {
            move();
        }
    }

    public void startMove(Tile target)
    {
        activateMovement = true;
        this.target = target;
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
        }
    }
}
