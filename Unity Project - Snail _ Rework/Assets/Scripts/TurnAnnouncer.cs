using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnAnnouncer : MonoBehaviour
{
    /// <summary>
    /// The GameObject representing the popup window.
    /// </summary>
    public GameObject popUpWindow;
    
    /// <summary>
    /// The TextMeshProUGUI component to display the announcement text.
    /// </summary>
    public TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        popUpWindow.SetActive(false);
    }

    /// <summary>
    /// Displays an announcement in the popup window for a specified duration.
    /// </summary>
    /// <param name="announcement">The announcement text to display.</param>
    /// <param name="durationInSeconds">The duration of the announcement in seconds.</param>
    /// <returns>An IEnumerator for coroutine execution.</returns>
    public IEnumerator Announce(string announcement,float durationInSeconds)
    {
        textMeshProUGUI.text = announcement;
        popUpWindow.SetActive(true);
        yield return new WaitForSeconds(durationInSeconds);
        popUpWindow.SetActive(false);
    }
}
