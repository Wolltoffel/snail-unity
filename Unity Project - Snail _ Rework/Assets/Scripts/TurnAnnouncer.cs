using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnAnnouncer : MonoBehaviour
{

    public GameObject popUpWindow;    
    public TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        popUpWindow.SetActive(false);
    }

    public IEnumerator Announce(string announcement,float durationInSeconds)
    {
        textMeshProUGUI.text = announcement;
        popUpWindow.SetActive(true);
        yield return new WaitForSeconds(durationInSeconds);
        popUpWindow.SetActive(false);
    }
}
