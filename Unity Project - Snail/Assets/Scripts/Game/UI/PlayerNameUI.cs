using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNameUI : MonoBehaviour
{
    [SerializeField] int playerIndex;
    Player player;
    TextMeshProUGUI tmpProText;

    void Start() {
        player = PlayerManager.player[playerIndex];
        tmpProText = GetComponent<TextMeshProUGUI>();
        tmpProText.text = player.name;
    }
}
