using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using Unity.VisualScripting;

public class XMLManager : MonoBehaviour
{
    public static XMLManager instance;
    public Leaderboard leaderboard;
    public Leaderboard leaderboard2;
    public Leaderboard leaderboard3; //hiermee zou je misschien van de drie levels een nieuwe scorebord kunnen maken 
    public ScoreManager scoreManager; //ik heb een getLevel functie in ScoreManager gezet,
                                      //je zou misschien iets kunnen doen van als level 1 voer deze code uit met leaderbord als level == 2 met leaderboard2 etc
    
    void Awake()
    {
        instance = this;

        if (!Directory.Exists(Application.persistentDataPath + "/HighScores/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/HighScores/");
        }
    }

    public void SaveScores(List<HighScoreEntry> scoresToSave)
    {
        leaderboard.list = scoresToSave;
        XmlSerializer serializer = new XmlSerializer(typeof(Leaderboard));
        FileStream stream = new FileStream(Application.persistentDataPath + "/HighScores/highscores.xml", FileMode.Create);
        serializer.Serialize(stream, leaderboard);
        stream.Close();
    }

    public List<HighScoreEntry> LoadScores()
    {
        if (File.Exists(Application.persistentDataPath + "/HighScores/highscores.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Leaderboard));
            FileStream stream = new FileStream(Application.persistentDataPath + "/HighScores/highscores.xml", FileMode.Open);
            leaderboard = serializer.Deserialize(stream) as Leaderboard;
            stream.Close();
        }

        return leaderboard.list;
    }
}

[System.Serializable]
public class Leaderboard
{
    public List<HighScoreEntry> list = new List<HighScoreEntry>();
}
