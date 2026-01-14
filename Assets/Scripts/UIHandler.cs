using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
# if UNITY_EDITOR
using UnityEditor;
# endif

public class UIHandler : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject gameOverPanel;
    public bool paused;
    public int score;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI dollarText;
    [SerializeField] private Button continueButton;

    void Start()
    {
        MainManager.Instance.LoadScore();
        paused = false;

        if(MainManager.Instance.isContinueued)
            score += MainManager.Instance.savedScore;
        else
            score = 0;

        dollarText.text = MainManager.Instance.dollars + "$";
        scoreText.SetText(PrintScore(score));
        bestScoreText.text = PrintScore(MainManager.Instance.bestScore);
    }

    // Update is called once per frame
    void Update()
    {
        // GameOver panel activate/deactiavate
        if (!MainManager.Instance.isGameActive)
            gameOverPanel.gameObject.SetActive(true);
        else
            gameOverPanel.gameObject.SetActive(false);

        // ESC key press
        if (Input.GetKeyDown(KeyCode.Escape))
            ChangePause();

        // Continue butten activated
        if (MainManager.Instance.dollars >= 3 && !MainManager.Instance.isContinueued)
            continueButton.interactable = true;
        else
            continueButton.interactable = false;

        // Score update
        if(MainManager.Instance.isGameActive && !paused)
            ScoreUpdate();
    }

    public void AddDollar(int value)
    {
        MainManager.Instance.dollars += value;
        dollarText.text = MainManager.Instance.dollars + "$";
    }

    public void ScoreUpdate()
    {
        score += 1;//(int) (0.1f * Math.Round(Time.deltaTime));
        scoreText.SetText(PrintScore(score));

        if (score > MainManager.Instance.bestScore)
        {
            MainManager.Instance.bestScore = score;
            bestScoreText.SetText(PrintScore(MainManager.Instance.bestScore));
        }
    }

    public string PrintScore(int score)
    {
        if(score / 10000000 >= 1)
            return "0" + score;
        else if(score / 1000000 >= 1 )
            return "00" + score;
        else if(score / 100000 >= 1 )
            return "000" + score;
        else if(score / 10000 >= 1 )
            return "0000" + score;
        else if(score / 1000 >= 1 )
            return "00000" + score;
        else if(score / 100 >= 1 )
            return "000000" + score;
        else if(score / 10 >= 1 )
            return "0000000" + score;
        else 
            return "";
    }

    public void ChangePause()
    {
        if (MainManager.Instance.isGameActive)
        {
            if(!paused)
            {
                paused = true;
                Time.timeScale = 0;
            }
            else
            {
                paused = false;
                Time.timeScale = 1;
            }
        }
    }

    public void Restart()
    {
        if(MainManager.Instance.isContinueued)
            MainManager.Instance.isContinueued = !MainManager.Instance.isContinueued;

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

    public void Continue()
    {
        //MainManager.Instance.dollars -=1;
        MainManager.Instance.savedScore = score;
        MainManager.Instance.isContinueued = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        MainManager.Instance.isGameActive = true;
    }
}