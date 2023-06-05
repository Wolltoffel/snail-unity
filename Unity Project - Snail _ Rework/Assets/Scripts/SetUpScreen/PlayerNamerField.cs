using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        inputField.text = gameData.GetPlayerName(playerIndex);
        inputField.onValueChanged.AddListener(SetName);
    }

    void SetName(string name)
    {
        gameData.SetPlayerName(name, playerIndex);
        Debug.Log(name);
    }

}
