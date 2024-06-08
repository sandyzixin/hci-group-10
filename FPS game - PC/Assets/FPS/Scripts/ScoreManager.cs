using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
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

    // Public method to get the current score
    public float GetScore()
    {
        return score;
    }

    public float GetLevel()
    {
        return level;
    }
}
