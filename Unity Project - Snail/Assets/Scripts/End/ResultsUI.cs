using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsUI : MonoBehaviour
{
    StatData stats;
    [SerializeField] TextMeshProUGUI playerName,scores, playedRounds,gameInformation,highScoreListDisplay;
    [SerializeField] HighscoreListManager highscoreListManager;
    ResultInfo resultInfo;

    private void Start()
    {
        loadData();
        setTexts();
    }

    void loadData() {
        stats = StatManager.statManager.giveCurrentStats();
        resultInfo = StatManager.resultInfo;
    }

    void setTexts()
    {
        playerName.text = stats.winner;
        scores.text = $"{stats.winnerScore} : {stats.loserScore}";
        playedRounds.text = $"{stats.rounds} played Rounds";

        setResultInfo();
        setHighScoreText();
    }

    void setResultInfo()
    {
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

    void setHighScoreText()
    {
        HighscoreData highScoreData = new HighscoreData(stats.mapName, stats.winner, stats.loser, stats.winnerScore, stats.loserScore, stats.agentWinner, stats.agentLoser);
        HighscoreData[] highScoreDataList  = highscoreListManager.giveCurrentHighscoreList(highScoreData);
        string text="";
        string highscoreDataText="";

        foreach (HighscoreData item in highScoreDataList)
        {
            if (item != null)
            {
                highscoreDataText = $"Map: {item.mapName} Winner: {item.winnerName} Score:{item.winnerScore}";
                text += highscoreDataText + "\n";
            }
        }

        highScoreListDisplay.text = text;
    }
}
