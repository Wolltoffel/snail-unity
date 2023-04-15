using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowRoundTime : MonoBehaviour
{
    private void Update()
    {
        updateRoundTime();
    }

    public void updateRoundTime ()
    {
        GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(RoundManager.turnDurationCounter).ToString();
    }
}
