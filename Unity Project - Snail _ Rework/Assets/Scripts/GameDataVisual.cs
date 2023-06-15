using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages the visual representation of game data.
/// </summary>
public class GameDataVisual : MonoBehaviour
{
    [SerializeField]bool activatePretext;
    [SerializeField] string preText;

    TextMeshProUGUI textMeshProUGUI;
    Color defaultColor;


    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        defaultColor = textMeshProUGUI.color;
    }

    /// <summary>
    /// Sets the visual representation of the game data using a string value.
    /// </summary>
    /// <param name="value">The string value to display.</param>
    public void SetVisual(string value)
    {
        if (activatePretext)
            textMeshProUGUI.text = $"{preText} {value}";
        else
            textMeshProUGUI.text = value ;

    }

    /// <summary>
    /// Sets the visual representation of the game data using an integer value.
    /// </summary>
    /// <param name="value">The integer value to display.</param>
    public void SetVisual(int value)
    {
        if (activatePretext)
            textMeshProUGUI.text = $"{preText} {value.ToString()}";
        else
            textMeshProUGUI.text = value.ToString();
    }

    /// <summary>
    /// Sets the visual representation of the game data using a float value.
    /// </summary>
    /// <param name="value">The float value to display.</param>
    public void SetVisual(float value)
    {
        if (activatePretext)
            textMeshProUGUI.text = $"{preText} {Mathf.RoundToInt(value).ToString()}";
        else
            textMeshProUGUI.text = Mathf.RoundToInt(value).ToString();
    }

    /// <summary>
    /// Sets the color of the text.
    /// </summary>
    /// <param name="color">The color to set.</param>
    public void SetColor(Color color)
    {
        textMeshProUGUI.color = color;
    }

    /// <summary>
    /// Restores the default color of the text.
    /// </summary>
    public void SetDefaultColor()
    {
       SetColor(defaultColor);
    }


}
