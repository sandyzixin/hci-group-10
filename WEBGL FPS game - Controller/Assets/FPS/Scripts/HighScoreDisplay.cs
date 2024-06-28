using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour
{
    public Text nameText;
    public Text scoreText;

    public void DisplayHighScore(string name, float score)
    {
        nameText.text = name;
        int minutes = Mathf.FloorToInt(score / 60);
        int seconds = Mathf.FloorToInt(score % 60);
        scoreText.text = string.Format("{00:00}:{1:00}", minutes, seconds);
    }

    public void HideEntryDisplay()
    {
        nameText.text = "";
        scoreText.text = "";
    }
}
