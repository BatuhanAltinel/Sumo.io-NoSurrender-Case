using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;


public class UIManager : MonoBehaviour
{
    [Header("Game Panel Elements")]
    [SerializeField] GameObject _gamePanel;
    float _gameTimer = 75;
    [SerializeField] TextMeshProUGUI _gameTimeText;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _sumoCountText;
    [SerializeField] GameObject _playButton;
    [SerializeField] GameObject _pauseButton;
    [SerializeField] GameObject _pausedText;


    [Header("End Panel Elements")]
    [SerializeField] GameObject _endPanel;
    [SerializeField] TextMeshProUGUI _rankText;
    [SerializeField] TextMeshProUGUI _pushedByText;
    [SerializeField] TextMeshProUGUI[] _scoreListTexts;

    [Header("Menu Panel")]
    [SerializeField] GameObject _menuPanel;
    [SerializeField] TextMeshProUGUI _startTimerText;
    [SerializeField] TextMeshProUGUI _GoText;
    int _startTimerCount = 3;


    void OnEnable()
    {
        EventManager.OnGameStateChanged += UpdateUI;    
        EventManager.OnScoreUpdate += UpdateScoreText;   
        EventManager.OnSumoFallDown += UpdateSumoCountText; 
    }

    void OnDisable()
    {
        EventManager.OnGameStateChanged -= UpdateUI;
        EventManager.OnScoreUpdate -= UpdateScoreText;
        EventManager.OnSumoFallDown -= UpdateSumoCountText;
    }


    void Start()
    {
        ResetTotalScoreText();
        UpdateSumoCountText();
    }

    void Update()
    {
        if(GameManager.Instance.IsState(GameState.InGame))
            DecreaseGameTimer();
    }


    void UpdateUI(GameState state)
    {
        switch (state)
        {
            case GameState.ReadyToStartGame:
                ResetTotalScoreText();
                ResetGameTimer();
                SumoManager.Instance.ResetSumoCount();
                UpdateSumoCountText();
                _menuPanel.gameObject.SetActive(CloseAllPanelExceptThis());
            break;

            case GameState.OnTimer:
                StartTimerSequence();                
            break;

            case GameState.InGame:
                _gamePanel.gameObject.SetActive(CloseAllPanelExceptThis());
            break;

            case GameState.Win:
                SetTextOnWin();
            break;

             case GameState.Lose:
                SetTextOnLose();
            break;

            case GameState.End: 
                _endPanel.gameObject.SetActive(CloseAllPanelExceptThis());
                UpdateScoreListText();
            break;
        }
    }


    void StartTimerSequence()
    {
        _startTimerText.gameObject.SetActive(true);
        _startTimerText.text = _startTimerCount.ToString();

        
        if(_startTimerCount > 0)
            _startTimerText.transform.DOScale(new Vector3(10,10,10),1f).SetEase(Ease.OutSine).OnComplete(() => 
            {
                _startTimerText.transform.localScale = Vector3.one;
                MinusStartTimer();
            });
        else
        {
            _startTimerText.gameObject.SetActive(false);
            _startTimerText.transform.localScale = Vector3.one;
            _startTimerCount = 3;

            ShowGoText();
        }
    }


    void MinusStartTimer()
    {
        _startTimerCount--;
        StartTimerSequence();
    }

    void ShowGoText()
    {
        _GoText.gameObject.SetActive(true);

        _GoText.transform.DOScale(new Vector3(10,10,10),1f).SetEase(Ease.OutSine).OnComplete(() => 
        {
            _GoText.gameObject.SetActive(false);
            _GoText.transform.localScale = Vector3.one;

            EventManager.OnGameStateChanged(GameState.InGame);
        });
    }


    void DecreaseGameTimer()
    {
        if(_gameTimer <= 0)
            StartCoroutine(WaitForEndState());
            // EventManager.OnGameStateChanged(GameState.End);
        
        _gameTimer -= Time.deltaTime;

        int minute = (int) _gameTimer / 60;
        int second = 0;

        if(minute > 0)
            second = (int) _gameTimer - 60;
        else
            second = (int) _gameTimer;

        _gameTimeText.text = second.ToString($"{minute}:00");
    }


    void ResetGameTimer()
    {
        _gameTimer = 75;
    }


    void ResetTotalScoreText()
    {
        _scoreText.text = "0";
    }


    bool CloseAllPanelExceptThis()
    {
        _menuPanel.gameObject.SetActive(false);
        _gamePanel.gameObject.SetActive(false);
        _endPanel.gameObject.SetActive(false);

        return true;
    }


    void UpdateScoreText(int totalScore)
    {
        _scoreText.text = totalScore.ToString();
    }


    void UpdateSumoCountText()
    {
        _sumoCountText.text = SumoManager.Instance.GetCurrentSumoCount().ToString();
    }


    void SetTextOnWin()
    {
        _rankText.text = "You are #" + SumoManager.Instance.GetCurrentSumoCount().ToString();
        _pushedByText.text = "Congrats !!!";

        StartCoroutine(WaitForEndState());
    }


    void SetTextOnLose()
    {
        _rankText.text = "You are #" + SumoManager.Instance.GetCurrentSumoCount().ToString();
        _pushedByText.text = "Pushed by AI";
        StartCoroutine(WaitForEndState());
    }


    void UpdateScoreListText()
    {
        List<Sumo> newSumoList = SumoManager.Instance.GetSumosOrderedList();

        for (int i = 0; i < _scoreListTexts.Length; i++)
        {
            _scoreListTexts[i].text = (i+1) + " - " + newSumoList[i].GetSumoName(newSumoList[i]) + " - " + newSumoList[i]._totalScore;
        }
    }

    public void ResumeTheGame()
    {
        _playButton.SetActive(false);
        _pauseButton.SetActive(true);
        _pausedText.SetActive(false);

        Time.timeScale = 1;
    }


    public void PauseTheGame()
    {
        _playButton.SetActive(true);
        _pauseButton.SetActive(false);
        _pausedText.SetActive(true);

        Time.timeScale = 0;
    }

    IEnumerator WaitForEndState()
    {
        InputManager.Instance.InputEnable = false;

        yield return new WaitForSeconds(1f);
        EventManager.OnGameStateChanged(GameState.End);

        yield return new WaitForSeconds(2f);
        InputManager.Instance.InputEnable = true;
    }

}
