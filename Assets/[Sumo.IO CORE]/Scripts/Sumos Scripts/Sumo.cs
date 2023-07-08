using UnityEngine;
using DG.Tweening;
using TMPro;

public abstract class Sumo : MonoBehaviour
{
    protected Rigidbody _rb;

    [Header("Sumo Canvas Atrributes")]
    [SerializeField] protected string _name;
    [SerializeField] protected TextMeshProUGUI _nameText;
    [SerializeField] protected TextMeshProUGUI _scoreText;
    protected int _score = 100;

    [SerializeField] protected GameObject _innerSumo;

    [Header("Force Attributes")]
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _RotateSpeed;
    [SerializeField] protected float _pushForce;

    [Header("Scale Attributes")]
    [SerializeField] protected Vector3 _currentScale;
    [SerializeField] protected Vector3 _normalScale;
    [SerializeField] protected float _scaleFactor;

    [Header("Movement Attributes")]
    protected Vector3 _moveDirection;
    protected Vector3 _moveRotation;



    void OnEnable()
    {
        EventManager.OnFeed += OnScaleUp;
        EventManager.OnFeed += OnScoreUp;
    }

    void OnDisable()
    {
        EventManager.OnFeed -= OnScaleUp;
        EventManager.OnFeed -= OnScoreUp;
    }


    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _currentScale = _innerSumo.transform.localScale;
        _normalScale = _currentScale;
    }

    protected virtual void Start()
    {
        SetOnStart();
    }

    void SetOnStart()
    {
        _nameText.text = _name;
        _currentScale = _normalScale;
    }

    public abstract void MoveForward();
    public abstract void PushOpposite(Vector3 dir);
    // public abstract void PushBackOnCrush();
    
    public void OnScaleUp(Sumo sumo)
    {
        Vector3 _tempScale = _scaleFactor * Vector3.one;
        _currentScale += _tempScale;

        sumo.transform.DOScale(_currentScale,0.3f).SetEase(Ease.OutBounce);
    }

    public virtual void OnScoreUp(Sumo sumo)
    {
        sumo._scoreText.text = "+" + _score.ToString();
        _scoreText.transform.gameObject.SetActive(true);


        sumo._scoreText.transform.DOLocalMoveY(1.4f,.6f).SetEase(Ease.OutSine).
        OnComplete(() => 
        {
            sumo._scoreText.transform.gameObject.SetActive(false);
            sumo._scoreText.transform.localPosition = new Vector3(0f,0.6f,0f);
        });

    }
    
}
