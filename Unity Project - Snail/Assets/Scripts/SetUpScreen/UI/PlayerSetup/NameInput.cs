using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameInput : MonoBehaviour
{
    [SerializeField] int characterLimit;
    TMP_InputField tMP_InputField;
    [SerializeField]TextMeshProUGUI placeHolder;
    [SerializeField]int playerIndex;
    private void Start()
    {
        tMP_InputField = GetComponent<TMP_InputField>();
        setDefaultValueForPlaceHolder();
    }

    void setDefaultValueForPlaceHolder() {
        Player player = SetUpScreenData.setUpScreenData.players[playerIndex];
        placeHolder.text = player.name;
    }
    void setCharacterLimit()
    {
        tMP_InputField.characterLimit = characterLimit;
    }
    public void setName()
    {
        string playername = tMP_InputField.text;
        SetUpScreenData.setUpScreenData.setName(playername, playerIndex);
    }

}
