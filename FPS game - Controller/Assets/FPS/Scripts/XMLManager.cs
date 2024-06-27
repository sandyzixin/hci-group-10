using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class XMLManager : MonoBehaviour
{
    public static XMLManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameObject alive across scene loads
        }
        else
        {
            Destroy(gameObject); // If there is already another instance, destroy this one
        }

        if (!Directory.Exists(Application.persistentDataPath + "/HighScores/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/HighScores/");
        }
    }

    public void SaveScores(List<HighScoreEntry> scoresToSave, int level)
    {
        string fileName = "highscores" + level + ".xml";
        Leaderboard leaderboard = new Leaderboard { list = scoresToSave };

        XmlSerializer serializer = new XmlSerializer(typeof(Leaderboard));
        using (FileStream stream = new FileStream(Application.persistentDataPath + "/HighScores/" + fileName, FileMode.Create))
        {
            serializer.Serialize(stream, leaderboard);
        }
    }

    public List<HighScoreEntry> LoadScores(int level)
    {
        string fileName = "highscores" + level + ".xml";
        string filePath = Application.persistentDataPath + "/HighScores/" + fileName;

        if (File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Leaderboard));
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                Leaderboard leaderboard = serializer.Deserialize(stream) as Leaderboard;
                return leaderboard.list;
            }
        }
        else
        {
            return new List<HighScoreEntry>();
        }
    }
}

[System.Serializable]
public class Leaderboard
{
    public List<HighScoreEntry> list = new List<HighScoreEntry>();
}

[System.Serializable]
public class HighScoreEntry
{
    public string name;
    public float score;
}
