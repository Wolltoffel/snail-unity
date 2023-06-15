using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAgentDropdown : MonoBehaviour
{
    [SerializeField] int playerIndex ;
    TMP_Dropdown dropdown;
    [SerializeField]GameData gameData;

    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        PlayerInformation playerInfo = gameData.GetPlayerInformation();

        List<string> options = new List<string>(Enum.GetNames(typeof(PlayerAgent)));
        dropdown.options.Clear();
        dropdown.AddOptions(options);
        dropdown.value = (int)playerInfo.playerAgents[playerIndex];
        
        dropdown.onValueChanged.AddListener(delegate { SetValue(dropdown); }); //Adjust dropdown on change

    }

    public void SetValue(TMP_Dropdown dropdown)
    {
        gameData.SetPlayerAgent((PlayerAgent)dropdown.value,playerIndex);
    }
}
