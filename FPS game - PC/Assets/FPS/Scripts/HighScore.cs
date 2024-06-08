using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public HighScoreDisplay[] highScoreDisplayArray;
    public InputField nameInputField;
    public Button submitButton;
    //public ScoreManager scoreManager; // Reference to ScoreManager
    List<HighScoreEntry> scores = new List<HighScoreEntry>();

    void Start()
    {
        // Load the scores from XML
        scores = XMLManager.instance.LoadScores();

        // Add button listener for submitting the score
        submitButton.onClick.AddListener(OnSubmitScore);

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        scores.Sort((HighScoreEntry x, HighScoreEntry y) => y.score.CompareTo(x.score));

        for (int i = 0; i < highScoreDisplayArray.Length; i++)
        {
            if (i < scores.Count)
            {
                highScoreDisplayArray[i].DisplayHighScore(scores[i].name, scores[i].score);
            }
            else
            {
                highScoreDisplayArray[i].HideEntryDisplay();
            }
        }

        // Save the updated scores to XML
        XMLManager.instance.SaveScores(scores);
    }

    void AddNewScore(string entryName, float entryScore)
    {
        scores.Add(new HighScoreEntry { name = entryName, score = entryScore });
        UpdateDisplay();
    }

    void OnSubmitScore()
    {
        string playerName = nameInputField.text;
        //float playerScore = scoreManager.GetScore(); // Get the score from ScoreManager
        float playerScore = Random.Range(0f, 10000f);

        AddNewScore(playerName, playerScore);
    }
}
