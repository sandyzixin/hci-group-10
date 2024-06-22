using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text scoreText;
    public InputField nameInputField;
    public Button submitButton;
    public HighScoreDisplay[] highScoreDisplayArray;

    public ScoreManager scoreManager;
    List<HighScoreEntry> scores = new List<HighScoreEntry>();

    void Start()
    {
        scoreManager = ScoreManager.instance;

        if (scoreText != null && nameInputField != null && submitButton != null)
        {
            InitializeWinScene();
        }
        else if (highScoreDisplayArray != null)
        {
            InitializeHighScoreScene();
        }
    }

    void InitializeWinScene()
    {
        float playerScore = scoreManager.GetScore();
        int minutes = Mathf.FloorToInt(playerScore / 60);
        int seconds = Mathf.FloorToInt(playerScore % 60);
        scoreText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        submitButton.onClick.AddListener(OnSubmitScore);
    }

    void OnSubmitScore()
    {
        string playerName = nameInputField.text;
        scores = scoreManager.LoadScores();
        scores.Add(new HighScoreEntry { name = playerName, score = scoreManager.GetScore() });
        scores.Sort((x, y) => x.score.CompareTo(y.score)); // Sort ascending by time

        scores = scores.GetRange(0, Mathf.Min(scores.Count, 5)); // Keep only top 5 scores
        scoreManager.SaveScores(scores);

        int currentLevel = scoreManager.GetLevel();
        SceneManager.LoadScene("HighScoreScene" + currentLevel);
    }

    void InitializeHighScoreScene()
    {
        scores = scoreManager.LoadScores();
        scores.Sort((x, y) => x.score.CompareTo(y.score)); // Sort ascending by time

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

    public void LoadNextLevel()
    {
        int currentLevel = scoreManager.GetLevel();
        if (currentLevel < 3) // Assuming you have 3 levels
        {
            SceneManager.LoadScene("Level" + (currentLevel + 1));
        }
        else
        {
            SceneManager.LoadScene("IntroMenu");
        }

        Destroy(scoreManager.gameObject); // Destroy ScoreManager after displaying high scores
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("IntroMenu");
    }
}