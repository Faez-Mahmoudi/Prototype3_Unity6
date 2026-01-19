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

    private float nextTimeToAddScore = 0;
    public bool paused;
    public int score;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI dollarText;
    [SerializeField] private TextMeshProUGUI bombText;
    [SerializeField] private Button continueButton;

    void Start()
    {
        MainManager.Instance.LoadScore();
        paused = false;
        gameOverPanel.gameObject.SetActive(false);

        if(MainManager.Instance.isContinueued)
            score += MainManager.Instance.savedScore;
        else
            score = 0;

        dollarText.text = MainManager.Instance.dollars + "$";
        bombText.text = MainManager.Instance.bombAmount + "B";
        //scoreText.text = PrintScore(score);
        bestScoreText.text = PrintScore(MainManager.Instance.bestScore);
    }

    // Update is called once per frame
    void Update()
    {
        if(MainManager.Instance.isGameActive)
        {
            if(Time.time >= nextTimeToAddScore && !paused)
            {
                ScoreUpdate();
                nextTimeToAddScore = Time.time + 0.03f;
            }
        }
        else    
            GameIsOverActions();            

        // ESC key pressed
        if (Input.GetKeyDown(KeyCode.Escape))
            ChangePause();
    }

    public void AddDollar(int value)
    {
        MainManager.Instance.dollars += value;
        dollarText.text = MainManager.Instance.dollars + "$";
    }

    public void AddBomb(int value)
    {
        MainManager.Instance.bombAmount += value;
        bombText.text = MainManager.Instance.bombAmount + "B";
    }

    public void ScoreUpdate()
    {
        score += 1;
        scoreText.SetText(PrintScore(score));
    }

    public string PrintScore(int score)
    {
        if(score / 10000000 >= 1)
            return score.ToString();
        else if(score / 1000000 >= 1 )
            return "0" + score;
        else if(score / 100000 >= 1 )
            return "00" + score;
        else if(score / 10000 >= 1 )
            return "000" + score;
        else if(score / 1000 >= 1 )
            return "0000" + score;
        else if(score / 100 >= 1 )
            return "00000" + score;
        else if(score / 10 >= 1 )
            return "000000" + score;
        else if(score >=1)
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

    private void GameIsOverActions()
    {
        gameOverPanel.gameObject.SetActive(true);
        if (score > MainManager.Instance.bestScore)
        {
            MainManager.Instance.bestScore = score;
            bestScoreText.SetText(PrintScore(MainManager.Instance.bestScore));
        }

        // Continue butten activated
        if (MainManager.Instance.dollars >= 10 && !MainManager.Instance.isContinueued)
            continueButton.interactable = true;
        else
            continueButton.interactable = false;
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
        AddDollar(-10);
        MainManager.Instance.savedScore = score;
        MainManager.Instance.isContinueued = true;
        MainManager.Instance.SaveScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        MainManager.Instance.isGameActive = true;
    }
}