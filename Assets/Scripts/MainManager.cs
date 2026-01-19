using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance{get; private set;}
    public int bestScore;
    public int savedScore;
    public int dollars;
    public int bombAmount;
    public bool isGameActive;
    public bool isContinueued;

    // Save our data
    [System.Serializable]
    class SaveData
    {
        public int b_Score;
        public int m_dollars;
        public int b_amount;
    }

    void Awake()
    {
       if (Instance != null)
       {
            Destroy(gameObject);
            return;
       }

       Instance = this;
       DontDestroyOnLoad(gameObject); 
       isGameActive = true;
       isContinueued = false;
       LoadScore();
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.b_Score = bestScore;
        data.m_dollars = dollars;
        data.b_amount = bombAmount;

        string json  = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestScore = data.b_Score;
            dollars = data.m_dollars;
            bombAmount = data.b_amount;
        }
        else
        {
            bestScore = 0;
            dollars = 0;
            bombAmount = 0;
        }
    }
}
