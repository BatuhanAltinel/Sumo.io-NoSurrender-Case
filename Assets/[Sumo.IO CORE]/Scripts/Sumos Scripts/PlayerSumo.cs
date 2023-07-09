using UnityEngine;
using DG.Tweening;
public class PlayerSumo : Sumo
{

    [SerializeField] private FloatingJoystick _joystick;
    private Vector3 _startPosition;


    void OnEnable()
    {
        EventManager.OnGameStateChanged += ResetPoisitionOnState;
        EventManager.OnGameStateChanged += ResetAllAttributesOnState;
    }
    void OnDisable()
    {
        EventManager.OnGameStateChanged -= ResetPoisitionOnState;
        EventManager.OnGameStateChanged -= ResetAllAttributesOnState;
    }

    protected override void Awake()
    {
        base.Awake();
        _startPosition = transform.position;
    }

    void Update()
    {
        if(GameManager.Instance.IsState(GameState.InGame) && !_isCrushed)
            Movement();
    }

    public override void Movement()
    {
        _moveDirection = Vector3.zero;
        _moveDirection.x = _joystick.Horizontal * _moveSpeed * Time.deltaTime;
        _moveDirection.z = _joystick.Vertical * _moveSpeed * Time.deltaTime;

        if(_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            Vector3 direction = Vector3.RotateTowards(_innerSumo.transform.forward, _moveDirection, _RotateSpeed * Time.deltaTime, 0.0f);
            _innerSumo.transform.rotation = Quaternion.LookRotation(direction);

        }

        _rb.MovePosition(_rb.position + _moveDirection);
    }

    public override void OnScoreUpOnFallTo(Sumo sumo)
    {
        
    }

    public override void OnScoreUpOnFeed()
    {
        _totalScore += _feedScore;

        _feedScoreText.text = "+" + _feedScore.ToString();
        _feedScoreText.transform.gameObject.SetActive(true);


        _feedScoreText.transform.DOLocalMoveY(1.4f,.6f).SetEase(Ease.OutSine).
        OnComplete(() => 
        {
            _feedScoreText.transform.gameObject.SetActive(false);
            _feedScoreText.transform.localPosition = new Vector3(0f,0.6f,0f);

            EventManager.OnScoreUpdate.Invoke(_totalScore);
        });

    }

    void ResetPoisitionOnState(GameState state)
    {
        if(GameManager.Instance.IsState(GameState.ReadyToStartGame))
            transform.position = _startPosition;
    }



}
