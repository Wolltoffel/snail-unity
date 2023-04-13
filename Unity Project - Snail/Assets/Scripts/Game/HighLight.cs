using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLight : MonoBehaviour
{
    [HideInInspector]public Tile tile;
    Vector3 initialSize;

    bool activeMovement;


    private void Awake()
    {
        initialSize = transform.localScale;
    }

    private void OnMouseEnter()
    {
        transform.localScale = transform.localScale * 1.1f;
    }

    private void OnMouseExit()
    {
        transform.localScale = initialSize;
    }

    private void OnMouseDown()
    {
        GameManager.movePlayer(RoundManager.activePlayer(), tile);
    }
}
