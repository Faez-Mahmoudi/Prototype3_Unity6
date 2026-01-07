using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
# if UNITY_EDITOR
using UnityEditor;
# endif

public class UIHandler : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    //[SerializeField] private GameObject powerupPrefab;
    public bool paused;
    private int score = 0;
    private int dollars = 0;
    //private int step = 0;
    //private int nextPowerupScore = 50;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI dollarText;

    // Update is called once per frame
    void Update()
    {
        if (!MainManager.Instance.isGameActive)
            gameOverPanel.gameObject.SetActive(true);
        else
            gameOverPanel.gameObject.SetActive(false);

        if (Input.GetKeyDown(KeyCode.Escape))
            ChangePause();

        //scoreText.SetText("Your score: " + score);
    }

    public void AddDollar(int value)
    {
        dollars += value;
        dollarText.text = dollars + "$";
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.SetText("Score = " + score);

        if (score > MainManager.Instance.bestScore)
        {
            MainManager.Instance.bestScore = score;
            bestScoreText.SetText("Best Score: " + MainManager.Instance.bestScore);
        }

        /*
        while (score >= nextPowerupScore)
        {
            Vector3 pos = new Vector3(Random.Range(-10, 10), 1, Random.Range(0, 10));
            Instantiate(powerupPrefab, pos, powerupPrefab.transform.rotation);
            nextPowerupScore += (100 + step * 25);
            //Debug.Log("value:" + nextPowerupScore);
            step++;            
        }

        while (score >= nextLiveScore)
        {
            Vector3 pos = new Vector3(Random.Range(-10, 10), 1, Random.Range(0, 10));
            Instantiate(livePearPrefab, pos, livePearPrefab.transform.rotation);
            nextLiveScore += (100 + step * 30); 
            //Debug.Log("live:" + nextLiveScore);
        }
        */
    }

    public void ChangePause()
    {
        if (MainManager.Instance.isGameActive)
        {
            if(!paused)
            {
                paused = true;
                pausePanel.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                paused = false;
                pausePanel.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        MainManager.Instance.isGameActive = true;
    }

    public void Exit()
    {
        MainManager.Instance.SaveScore();

        # if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        # else
        Application.Quit();
        # endif
    }

    private void Continue()
    {

    }
}
