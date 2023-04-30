using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsUI : MonoBehaviour
{
    StatData stats;
    [SerializeField] TextMeshProUGUI playerName,scores, playedRounds,gameInformation;
    ResultInfo resultInfo;

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

        switch (resultInfo)
        {
            case ResultInfo.score:
                 gameInformation.text = $"{stats.winner} has won by score";
                break;
            case ResultInfo.surrender:
                gameInformation.text = $"{stats.winner} won by surrender";
                break;
            case ResultInfo.maxTurnsWithoutCaptureExceeded:
                gameInformation.text = $"{stats.winner} won becasuse {stats.winner} exceeded his Maximum Turns Wihout Capture";
                break;
        }

    }
}
