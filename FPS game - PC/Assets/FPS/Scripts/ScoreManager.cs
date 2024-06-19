using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private float score;
    public TextMeshProUGUI scoreText;
    public int level;
    private float timer;
    private int secondsElapsed;
    private int minutesElapsed;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        secondsElapsed = 0;
        minutesElapsed = 0;
        score = 0f;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameObject alive across scene loads
        }
        else
        {
            Destroy(gameObject); // If there is already another instance, destroy this one
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (secondsElapsed == 59)
        {
            minutesElapsed++;
            secondsElapsed = -1;
        }

        

        if (timer >= 1.0f)
        {
            score = minutesElapsed + (secondsElapsed * 0.1f);
            // Increment the elapsed seconds counter
            secondsElapsed++;

            if (secondsElapsed < 10)
            {
                //scoreText.text = "Score: " + score + " | Time: " + minutesElapsed + ":0" + secondsElapsed;
                scoreText.text = "Time: " + minutesElapsed + ":0" + secondsElapsed;
            }
            else
            {
                //scoreText.text = "Score: " + score + " | Time: " + minutesElapsed + ":" + secondsElapsed;
                scoreText.text = "Time: " + minutesElapsed + ":" + secondsElapsed;
            }

            // Reset the timer
            timer = 0.0f;
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            if (secondsElapsed < 10)
            {
                scoreText.text = "Time: " + minutesElapsed + ":0" + secondsElapsed;
            }
            else
            {
                scoreText.text = "Time: " + minutesElapsed + ":" + secondsElapsed;
            }
        }
    }

    // Public method to get the current score
    public float GetScore()
    {
        return score;
    }

    // Public method to set the current level
    public void SetLevel(int newLevel)
    {
        level = newLevel;
    }

    public int GetLevel()
    {
        return level;
    }

    // Method to save scores to XML
    public void SaveScores(List<HighScoreEntry> scoresToSave)
    {
        XMLManager.instance.SaveScores(scoresToSave, level);
    }

    // Method to load scores from XML
    public List<HighScoreEntry> LoadScores()
    {
        return XMLManager.instance.LoadScores(level);
    }
}
