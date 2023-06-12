using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WrongMapPopUp : MonoBehaviour
{

    [SerializeField] GameObject popUpWindow;
    [SerializeField]Button closeButton;
    [SerializeField] TextMeshProUGUI errorText;
    Vector3 initialScale,smallScale;
    Coroutine scaleUp;

    public void Awake()
    {
        popUpWindow.SetActive(false);
        initialScale = popUpWindow.transform.localScale;
        smallScale = initialScale * 0.001f;
        popUpWindow.transform.localScale = smallScale;

        closeButton.onClick.AddListener(()=>HidePopUp());
    }

    public void showPopUp(string errorMessage)
    {
        errorText.text = errorMessage;
        scaleUp = StartCoroutine(Scale(popUpWindow.transform.localScale, initialScale));
        popUpWindow.SetActive(true);
    }

    public void HidePopUp()
    {
        StartCoroutine(hidePopUpProcess()); 
    }
    
    public IEnumerator hidePopUpProcess()
    {
        if (scaleUp != null)
            StopCoroutine(scaleUp);

        yield return Scale(popUpWindow.transform.localScale, smallScale);
        popUpWindow.SetActive(false);
    }

    IEnumerator Scale(Vector3 currentScale, Vector3 targetScale)
    {
        Vector3 startScale = currentScale; ;
        float startTime = Time.time;

        while (Vector3.Distance(currentScale, targetScale) > 0.01f)
        {
            float t = (Time.time - startTime) / 2f;
            currentScale = Vector3.Lerp(currentScale, targetScale, t);
            popUpWindow.transform.localScale = currentScale;
            yield return null;
        }
    }
}
