using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Highlight : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    Vector2Int position;
    Vector3 initialScale;
    public GameController gameController;
    public Player player;
    Coroutine growProcess, shrinkProcess;

    public void InsertData(Vector2Int position,GameController gameController, Player player)
    {
        this.position = position;
        this.gameController = gameController;
        this.player = player;
        initialScale = transform.localScale;
    }

    public Vector2Int GetPosition()
    {
        return position;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        PlayerAction action = new PlayerAction(ActionType.Move, position);
        gameController.SetAction(action);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (shrinkProcess != null)
            StopCoroutine(shrinkProcess);

        SoundManager.instance.PlaySound("ui-click-high-modern-click-06");

        growProcess = StartCoroutine(Scale(transform.localScale,initialScale*1.1f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        if (growProcess != null)
            StopCoroutine(growProcess);

        shrinkProcess = StartCoroutine(Scale(transform.localScale, initialScale));
    }

    IEnumerator Scale(Vector3 currentScale, Vector3 targetScale)
    {
        // Smoothly scales the highlight from currentScale to targetScale
        Vector3 startScale = currentScale;;
        float startTime = Time.time;

        while (Vector3.Distance(currentScale, targetScale) > 0.01f)
        {
            float t = (Time.time - startTime) / 0.4f;
            currentScale = Vector3.Lerp(currentScale, targetScale, t);
            transform.localScale = currentScale;
            yield return null;
        }
    }
}
