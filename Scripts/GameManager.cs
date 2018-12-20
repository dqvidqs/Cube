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
        
        if(Instance == null) {
            Instance = this;
            //DontDestroyOnLoad (gameObject);
        }else{
            //DestroyImmediate(gameObject);
            Instance = this;
        }
        /*Instance = this;
            GameObject go = GameObject.Find("GameStartPage");
            if(go == null){
                Debug.Log("go1 cant found");
            }
            else{
                StarPage = go;
                StarPage.SetActive(true);
            }
            go = GameObject.Find("GameOverPage");
            if(go == null){
                Debug.Log("go2 cant found");
            }
            else{
                GameOverPage = go;
                GameOverPage.SetActive(false);
            }
            go = GameObject.Find("CountDownPage");
            if(go == null){
                Debug.Log("go3 cant found");
            }
            else{
                CountDownPage = go;
                CountDownPage.SetActive(false);
            }*/
        
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
        PlayerPrefs.SetInt("Score", score);
        if(score > savedScore)
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


