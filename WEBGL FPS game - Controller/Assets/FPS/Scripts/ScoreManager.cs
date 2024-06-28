using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private float timer;
    private int secondsElapsed;
    private int minutesElapsed;

    public TextMeshProUGUI scoreText;
    public int level;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return; // Ensure we don't execute the rest of the Awake code for this new instance
        }
    }

    void Start()
    {
        timer = 0f;
        secondsElapsed = 0;
        minutesElapsed = 0;
        UpdateScoreText();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1.0f)
        {
            secondsElapsed++;
            if (secondsElapsed == 60)
            {
                minutesElapsed++;
                secondsElapsed = 0;
            }

            UpdateScoreText();
            timer = 0.0f;
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Time: " + minutesElapsed.ToString("00") + ":" + secondsElapsed.ToString("00");
        }
    }

    public float GetScore()
    {
        return minutesElapsed * 60 + secondsElapsed;
    }

    public void SetLevel(int newLevel)
    {
        level = newLevel;
    }

    public int GetLevel()
    {
        return level;
    }

    public void SaveScores(List<HighScoreEntry> scoresToSave)
    {
        XMLManager.instance.SaveScores(scoresToSave, level);
    }

    public List<HighScoreEntry> LoadScores()
    {
        return XMLManager.instance.LoadScores(level);
    }
}
