using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllHighScoresDisplay : MonoBehaviour
{
    public HighScoreDisplay[] highScoreDisplaysLevel1; // UI elements for level 1
    public HighScoreDisplay[] highScoreDisplaysLevel2; // UI elements for level 2
    public HighScoreDisplay[] highScoreDisplaysLevel3; // UI elements for level 3

    void Start()
    {
        InitializeAllHighScores();
    }

    void InitializeAllHighScores()
    {
        LoadAndDisplayHighScores(highScoreDisplaysLevel1, 1);
        LoadAndDisplayHighScores(highScoreDisplaysLevel2, 2);
        LoadAndDisplayHighScores(highScoreDisplaysLevel3, 3);
    }

    void LoadAndDisplayHighScores(HighScoreDisplay[] highScoreDisplays, int level)
    {
        List<HighScoreEntry> scores = XMLManager.instance.LoadScores(level);
        scores.Sort((x, y) => x.score.CompareTo(y.score)); // Sort ascending by score

        for (int i = 0; i < highScoreDisplays.Length; i++)
        {
            if (i < scores.Count)
            {
                highScoreDisplays[i].DisplayHighScore(scores[i].name, scores[i].score);
            }
            else
            {
                highScoreDisplays[i].HideEntryDisplay();
            }
        }
    }
}
