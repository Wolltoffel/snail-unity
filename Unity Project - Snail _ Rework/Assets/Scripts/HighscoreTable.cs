using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI heading;
    [SerializeField]GameObject entryTemplate;
    [SerializeField] Transform entryContainer;

    List<GameObject> spawnedEntries = new List<GameObject>();

    float objectAboveYPos;

    private void Awake()
    {
        entryTemplate.SetActive(false);
    }

    public void SetHeading(MapData map)
    {
        heading.richText = true;
        heading.text = $"Highscores on <b>{map.name}</b>";
    }

    public void AddHighScore(Highscore highScore, bool newHighScore)
    {
        float templateHeight = entryTemplate.GetComponent<RectTransform>().rect.height;
        float entryTemplateYPos = entryTemplate.GetComponent<RectTransform>().anchoredPosition.y;

        GameObject entry = Instantiate(entryTemplate,entryContainer);
        entry.SetActive(true);
        RectTransform entryTransform = entry.GetComponent<RectTransform>();
        entry.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -objectAboveYPos+entryTemplateYPos);
        objectAboveYPos += templateHeight;


        TextMeshProUGUI winnerScore  = entry.transform.Find("[WinnerScore]").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI winnerEntry = entry.transform.Find("[WinnerName] + [PlayerAgent]").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI loserEntry = entry.transform.Find("[LoserName] + [PlayerAgent]").GetComponent<TextMeshProUGUI>();

        winnerScore.text = highScore.winnerScore.ToString();
        winnerEntry.text = $"{highScore.winnerName} [{highScore.agentWinner}]";
        loserEntry.text = $"{highScore.looserName} [{highScore.agentLoser}]";

        if (newHighScore)
        {
            entry.GetComponent<Image>().color = Color.green;
        }

        spawnedEntries.Add(entry);
    }

    public void OnDisable()
    {
        for (int i = 0; i < spawnedEntries.Count; i++)
        {
            Destroy(spawnedEntries[i]);
        }
        spawnedEntries.Clear();
        objectAboveYPos = 0;
    }
}
