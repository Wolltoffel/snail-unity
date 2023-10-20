using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <summary>
/// Represents a player's name input field.
/// This class manages the functionality of an input field where players can enter their names.
/// It retrieves and sets the player's name in the GameData based on the input field value.
/// </summary>
public class PlayerNamerField : MonoBehaviour
{
    TMP_InputField inputField;
    [SerializeField] int playerIndex;
    [SerializeField] GameData gameData;

    void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    private void Start()
    {
        // Set the initial value of the input field to the player's name
        inputField.text = gameData.GetPlayerName(playerIndex);

        // Add a listener to detect changes in the input field
        inputField.onValueChanged.AddListener(SetName);
    }

    /// <summary>
    /// Sets the player's name in the GameData based on the input field value.
    /// </summary>
    /// <param name="name">The new name entered in the input field.</param>
    void SetName(string name)
    {
        gameData.SetPlayerName(name, playerIndex);
    }

}
