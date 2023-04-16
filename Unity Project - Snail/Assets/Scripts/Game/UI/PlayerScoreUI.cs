using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScoreUI : MonoBehaviour
{
    [SerializeField] int playerIndex;
    Player player;
    TextMeshProUGUI tmpProText;

    void Start() {
        player = PlayerConfig.player[playerIndex];
        tmpProText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        string text = $"Score: {player.score}";
        tmpProText.text = text;
    }
}
