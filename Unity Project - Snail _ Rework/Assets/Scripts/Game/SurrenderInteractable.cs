using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SurrenderInteractable : MonoBehaviour, IPointerClickHandler
{
    GameController gameController;

    public void InsertGameData(GameController gameController)
    {
        this.gameController = gameController;
    }  

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerAction action = new PlayerAction(ActionType.Skip, Vector2Int.zero);
        gameController?.SetAction(action);
    }
}
