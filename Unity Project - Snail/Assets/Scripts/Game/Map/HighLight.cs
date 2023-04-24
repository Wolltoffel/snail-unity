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
        SoundManager.soundManager.PlaySound("ui-click-high-modern-click-06");
        transform.localScale = transform.localScale * 1.1f;
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
        ActionInfo actionInfo = new ActionInfo(ActionType.capture,player);
        GameManager.excuteTurn(player, tile,actionInfo);
    }
}
