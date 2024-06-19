using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SocialPlatforms;

public class XMLManager : MonoBehaviour
{
    public static XMLManager instance;
    //public Leaderboard leaderboard1;
    //public Leaderboard leaderboard2;
    //public Leaderboard leaderboard3; 
    //public ScoreManager scoreManager; //ik heb een getLevel functie in ScoreManager gezet,
                                      //je zou misschien iets kunnen doen van als level 1 voer deze code uit met leaderbord als level == 2 met leaderboard2 etc
    
    void Awake()
    {
        instance = this;

        if (!Directory.Exists(Application.persistentDataPath + "/HighScores/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/HighScores/");
        }
    }

    public void SaveScores(List<HighScoreEntry> scoresToSave, int level)
    {

        string fileName = "highscores" + level + ".xml";

        Leaderboard leaderboard = new Leaderboard();
        leaderboard.list = scoresToSave;

        XmlSerializer serializer = new XmlSerializer(typeof(Leaderboard));
        FileStream stream = new FileStream(Application.persistentDataPath + "/HighScores/" + fileName, FileMode.Create);
        serializer.Serialize(stream, leaderboard);
        stream.Close();
    }

    void SaveLeaderboard(Leaderboard leaderboard, string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Leaderboard));
        FileStream stream = new FileStream(Application.persistentDataPath + "/HighScores/" + fileName, FileMode.Create);
        serializer.Serialize(stream, leaderboard);
        stream.Close();
    }

    public List<HighScoreEntry> LoadScores(int level)
    {
        string fileName = "highscores" + level + ".xml";
        string filePath = Application.persistentDataPath + "/HighScores/" + fileName;

        if (File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Leaderboard));
            FileStream stream = new FileStream(filePath, FileMode.Open);
            Leaderboard leaderboard = serializer.Deserialize(stream) as Leaderboard;
            stream.Close();
            return leaderboard.list;
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

