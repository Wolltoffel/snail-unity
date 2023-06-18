using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Represents a highscore table in the game.
/// </summary>
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

    /// <summary>
    /// Sets the heading text for the highscore table.
    /// </summary>
    /// <param name="map">The map data associated with the highscores.</param>
    public void SetHeading(MapData map)
    {
        heading.richText = true;
        heading.text = $"Highscores on <b>{map.name}</b>";
    }

    /// <summary>
    /// Adds a new highscore entry to the table.
    /// </summary>
    /// <param name="highScore">The highscore data to add.</param>
    /// <param name="newHighScoreFlag">Flag indicating if it is a new highscore.</param>
    public void AddHighScore(Highscore highScore, bool newHighScoreFlag)
    {
        // Get the height and initial Y position of the entry template
        float templateHeight = entryTemplate.GetComponent<RectTransform>().rect.height;
        float entryTemplateYPos = entryTemplate.GetComponent<RectTransform>().anchoredPosition.y;

        // Instantiate a new entry from the template
        GameObject entry = Instantiate(entryTemplate,entryContainer);
        entry.SetActive(true);
        RectTransform entryTransform = entry.GetComponent<RectTransform>();

        // Set the position of the entry based on the object above it
        entry.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -objectAboveYPos+entryTemplateYPos);
        objectAboveYPos += templateHeight;

        // Get the text elements in the entry template
        TextMeshProUGUI winnerScore  = entry.transform.Find("[WinnerScore]").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI winnerEntry = entry.transform.Find("[WinnerName] + [PlayerAgent]").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI loserEntry = entry.transform.Find("[LoserName] + [PlayerAgent]").GetComponent<TextMeshProUGUI>();

        // Set the text values for the winner score, winner entry, and loser entry
        winnerScore.text = highScore.winnerScore.ToString();
        winnerEntry.text = $"{highScore.winnerName} [{highScore.agentWinner}]";
        loserEntry.text = $"{highScore.loserName} [{highScore.agentLoser}]";

        // Set the color of the entry if it's a new high score
        if (newHighScoreFlag)
        {
            entry.GetComponent<Image>().color = Color.green;
        }

        // Add the entry to the list of spawned entries
        spawnedEntries.Add(entry);
    }

    /// <summary>
    /// Clears the highscore table and resets its state.
    /// </summary>
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
