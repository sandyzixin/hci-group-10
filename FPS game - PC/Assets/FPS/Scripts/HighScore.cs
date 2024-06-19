using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    // UI elements for the win scene
    public Text scoreText;
    public InputField nameInputField;
    public Button submitButton;

    // UI elements for the high score scene
    public HighScoreDisplay[] highScoreDisplayArray;

    public ScoreManager scoreManager; // Reference to ScoreManager
    List<HighScoreEntry> scores = new List<HighScoreEntry>();


    void Start()
    {
        scoreManager = ScoreManager.instance;

        // Check if we're in the win scene or high score scene
        if (scoreText != null && nameInputField != null && submitButton != null)
        {
            // We're in the win scene
            InitializeWinScene();
        }
        else if (highScoreDisplayArray != null)
        {
            // We're in the high score scene
            InitializeHighScoreScene();
        }
    }

    void InitializeWinScene()
    {
        // Get the player's score from the ScoreManager
        float playerScore = scoreManager.GetScore();

        // Display the player's score
        scoreText.text = playerScore.ToString("F2");

        // Add button listener for submitting the score
        submitButton.onClick.AddListener(OnSubmitScore);
    }

    void OnSubmitScore()
    {
        string playerName = nameInputField.text;
        scores = scoreManager.LoadScores();

        // Add the new score
        scores.Add(new HighScoreEntry { name = playerName, score = scoreManager.GetScore() });
        scores.Sort((x, y) => y.score.CompareTo(x.score));

        // Save the updated scores
        scores = scores.GetRange(0, Mathf.Min(scores.Count, 10)); // Keep only top 10 scores

        scoreManager.SaveScores(scores);

        // Load the High Score Scene
        SceneManager.LoadScene("HighScoreScene" + scoreManager.GetLevel());
    }

    void InitializeHighScoreScene()
    {
        // Load the scores from XML
        scores = scoreManager.LoadScores();

        // Sort the scores in descending order
        scores.Sort((x, y) => y.score.CompareTo(x.score));

        // Update the high score display
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
    }
}
