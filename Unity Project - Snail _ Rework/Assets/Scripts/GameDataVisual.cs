using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameDataVisual : MonoBehaviour
{
    [SerializeField]bool activatePretext;
    [SerializeField] string preText;

    TextMeshProUGUI textMeshProUGUI;


    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }


    public void SetVisual(string value)
    {
        if (activatePretext)
            textMeshProUGUI.text = $"{preText} {value}";
        else
            textMeshProUGUI.text = value ;

    }

    public void SetVisual(int value)
    {
        if (activatePretext)
            textMeshProUGUI.text = $"{preText} {value.ToString()}";
        else
            textMeshProUGUI.text = value.ToString();
    }

    public void SetVisual(float value)
    {
        if (activatePretext)
            textMeshProUGUI.text = $"{preText} {Mathf.RoundToInt(value).ToString()}";
        else
            textMeshProUGUI.text = Mathf.RoundToInt(value).ToString();
    }


}
