using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour
{
    public Text nameText;
    public Text scoreText;
    List<HighScoreEntry> scores = new List<HighScoreEntry>();


    public void DisplayHighScore(string name, float score) { 
    
        nameText.text = name;
        scoreText.text = string.Format("{0:0000}", score);
    }

    public void HideEntryDisplay()
    {
        nameText.text = "";
        scoreText.text = "";
    }
   
    void AddNewScore(string entryName, float entryScore)
    {
        scores.Add(new HighScoreEntry { name = entryName, score = entryScore });
    }
}

[System.Serializable]
public class HighScoreEntry
{
    public string name;
    public float score;
}