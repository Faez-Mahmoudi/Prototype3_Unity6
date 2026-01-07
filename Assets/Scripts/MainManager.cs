using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance{get; private set;}
    public int bestScore;
    public int dollars;
    public bool isGameActive;

    // Save bestScore
    [System.Serializable]
    class SaveData
    {
        public int b_Score;
        public int m_dollars;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
       LoadScore();
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.b_Score = bestScore;
        data.m_dollars = dollars;

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
        }
        else
        {
            bestScore = 0;
            dollars = 0;
        }
    }
}
