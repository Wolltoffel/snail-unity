using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Represents an interactable surrender button in the game.
/// </summary>
public class SurrenderInteractable : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler, IPointerExitHandler
{
    GameController gameController;
    Coroutine shrinkProcess,growProcess;
    Vector3 initialScale;

    void Awake()
    {
        initialScale = transform.localScale;
    }

    /// <summary>
    /// Adds a reference to the game controller.
    /// </summary>
    /// <param name="gameController">The game controller to add.</param>
    public void AddGameController(GameController gameController)
    {
        this.gameController = gameController;
    }

    // <summary>
    /// Handles the pointer click event.
    /// </summary>
    /// <param name="eventData">The pointer event data.</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerAction action = new PlayerAction(ActionType.Skip, Vector2Int.zero);
        gameController?.SetAction(action);

        if (growProcess != null)
            StopCoroutine(growProcess);

        shrinkProcess = StartCoroutine(Scale(transform.localScale, initialScale));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (shrinkProcess != null)
            StopCoroutine(shrinkProcess);

        SoundManager.instance.PlaySound("ui-click-high-modern-click-06");

        growProcess = StartCoroutine(Scale(transform.localScale, initialScale * 1.1f));
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
        Vector3 startScale = currentScale; ;
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
