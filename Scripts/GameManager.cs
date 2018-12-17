using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public delegate void GameDelegate();

    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;

    public static GameManager Instance;

    public GameObject StarPage;
    public GameObject GameOverPage;
    public GameObject CountDownPage;
    public Text ScoreText;

    enum PageState
    {
        None,
        Start,
        Over,
        Count
    }

    int score = 0;
    bool gameover = false;
    public bool GameOver { get { return gameover; } }
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void OnEnable()
    {
        CountDownText.OnCountFinish += OnCountFinish;
        TapControler.OnPlayerDied += OnPlayerDied;
        TapControler.OnPlayerScored += OnPlayerScored;
    }
    void OnDisible()
    {
        CountDownText.OnCountFinish -= OnCountFinish;
        TapControler.OnPlayerDied -= OnPlayerDied;
        TapControler.OnPlayerScored -= OnPlayerScored;
    }
    void OnPlayerDied()
    {
        gameover = true;
        int savedScore = PlayerPrefs.GetInt("HighScore");
        if(score> savedScore)
        {
            PlayerPrefs.SetInt("HighScore", score);

        }
        SetPageState(PageState.Over);

    }
    void OnPlayerScored()
    {
        score++;
        ScoreText.text = score.ToString();
    }
    void OnCountFinish()
    {
        SetPageState(PageState.None);
        OnGameStarted();
        score = 0;
        gameover = false;
    }
    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                StarPage.SetActive(false);
                GameOverPage.SetActive(false);
                CountDownPage.SetActive(false);
                break;
            case PageState.Start:
                StarPage.SetActive(true);
                GameOverPage.SetActive(false);
                CountDownPage.SetActive(false);
                break;
            case PageState.Over:
                StarPage.SetActive(false);
                GameOverPage.SetActive(true);
                CountDownPage.SetActive(false);
                break;
            case PageState.Count:
                StarPage.SetActive(false);
                GameOverPage.SetActive(false);
                CountDownPage.SetActive(true);
                break;
        }
    }
    public void ConfirmGameOver()
    {
        SetPageState(PageState.Start);
        OnGameOverConfirmed();
        ScoreText.text = "0";
    }
    public void StartGame()
    {
        SetPageState(PageState.Count);
    }
}


