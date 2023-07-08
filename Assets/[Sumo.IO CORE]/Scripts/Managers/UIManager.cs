using UnityEngine;
using UnityEngine.UI;
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

    [Header("End Panel Elements")]
    [SerializeField] GameObject _endPanel;
    [SerializeField] TextMeshProUGUI _rankText;
    [SerializeField] TextMeshProUGUI _pushedByText;

    [Header("Menu Panel")]
    [SerializeField] GameObject _menuPanel;
    [SerializeField] TextMeshProUGUI _startTimerText;
    [SerializeField] TextMeshProUGUI _GoText;
    int _startTimerCount = 3;


    void OnEnable()
    {
        EventManager.OnGameStateChanged += UpdateUI;    
    }

    void OnDisable()
    {
        EventManager.OnGameStateChanged -= UpdateUI;
    }


    void Start()
    {
        
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
                ReseGameTimer();
                _menuPanel.gameObject.SetActive(CloseAllPanelExceptThis());
                // StartTimerSequence();

            break;

            case GameState.OnTimer:
                StartTimerSequence();                
            break;

            case GameState.InGame:
                _gamePanel.gameObject.SetActive(CloseAllPanelExceptThis());
                
            break;

            case GameState.End: 
                _endPanel.gameObject.SetActive(CloseAllPanelExceptThis());
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
            EventManager.OnGameStateChanged(GameState.End);
        
        _gameTimer -= Time.deltaTime;

        int minute = (int) _gameTimer / 60;
        int second = 0;

        if(minute > 0)
            second = (int) _gameTimer - 60;
        else
            second = (int) _gameTimer;

        _gameTimeText.text = second.ToString($"{minute}:00");
    }

    void ReseGameTimer()
    {
        _gameTimer = 75;
    }

    bool CloseAllPanelExceptThis()
    {
        _menuPanel.gameObject.SetActive(false);
        _gamePanel.gameObject.SetActive(false);
        _endPanel.gameObject.SetActive(false);

        return true;
    }

}
