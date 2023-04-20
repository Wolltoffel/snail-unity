using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSettingsButton : MonoBehaviour
{

    public void resetSettings()
    {
        PlayerSettingsManager.settings.loadDefaultValues();
    }
}
