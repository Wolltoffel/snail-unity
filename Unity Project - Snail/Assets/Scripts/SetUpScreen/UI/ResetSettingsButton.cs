using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSettingsButton : MonoBehaviour
{
   [SerializeField] PlayerSettingsManager playerSettingsManager;

    public void resetSettings()
    {
        PlayerSettings playerSettings = PlayerSettingsManager.settings;
        playerSettings.loadDefaultValues();
        playerSettingsManager.saveSettings(playerSettings);
    }
}
