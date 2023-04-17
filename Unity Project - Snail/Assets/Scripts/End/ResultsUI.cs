using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsUI : MonoBehaviour
{
    StatData stats;
    [SerializeField] TextMeshProUGUI playerName,scores, playedRounds;

    private void Start()
    {
        loadData();
        setText();
    }

    void loadData() {
         stats = StatManager.stats;
    }

    void setText()
    {
        playerName.text = stats.winner;
        scores.text = $"{stats.winnerScore} : {stats.loserScore}";
        playedRounds.text = $"{stats.rounds} played Rounds";
    }
}
