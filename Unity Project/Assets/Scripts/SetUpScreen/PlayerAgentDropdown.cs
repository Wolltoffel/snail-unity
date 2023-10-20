using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Dropdown handler for selecting player agents.
/// </summary>
public class PlayerAgentDropdown : MonoBehaviour
{
    [SerializeField] int playerIndex ;
    TMP_Dropdown dropdown;
    [SerializeField]GameData gameData;

    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        PlayerInformation playerInfo = gameData.GetPlayerInformation();

        // Get the list of player agent options
        List<string> options = new List<string>(Enum.GetNames(typeof(PlayerAgent)));

        // Clear the existing options and add new options to the dropdown
        dropdown.options.Clear();
        dropdown.AddOptions(options);

        // Set the initial value of the dropdown based on the player agent in the PlayerInformation
        dropdown.value = (int)playerInfo.playerAgents[playerIndex];

        // Add a listener to detect value changes in the dropdown
        dropdown.onValueChanged.AddListener(delegate { SetValue(dropdown); }); //Adjust dropdown on change

    }

    /// <summary>
    /// Sets the value of the player agent dropdown.
    /// </summary>
    /// <param name="dropdown">The TMP_Dropdown component.</param>
    public void SetValue(TMP_Dropdown dropdown)
    {
        gameData.SetPlayerAgent((PlayerAgent)dropdown.value,playerIndex);
    }
}
