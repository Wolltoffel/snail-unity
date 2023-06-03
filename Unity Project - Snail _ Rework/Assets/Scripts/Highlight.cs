using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Highlight : MonoBehaviour, IPointerDownHandler
{
    Vector2Int position;
    GameManager manager;

    public void InsertData(Vector2Int position,GameManager manager)
    {
        this.position = position;
        this.manager = manager;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        PlayerAction agentMove = new PlayerAction(ActionType.Move, position);
        manager.MakeMove(agentMove);
    }
}
