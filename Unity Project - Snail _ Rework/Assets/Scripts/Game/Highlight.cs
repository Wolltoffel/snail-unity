using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Highlight : MonoBehaviour, IPointerDownHandler
{
    Vector2Int position;
    public GameController gameController;
    public Player player;

    public void InsertData(Vector2Int position,GameController gameController, Player player)
    {
        this.position = position;
        this.gameController = gameController;
        this.player = player;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        PlayerAction action = new PlayerAction(ActionType.Move, position);
        gameController.SetAction(action);
    }
}
