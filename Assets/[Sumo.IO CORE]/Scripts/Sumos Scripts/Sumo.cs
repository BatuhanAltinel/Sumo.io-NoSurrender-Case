using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;

public abstract class Sumo : MonoBehaviour
{
    protected Rigidbody _rb;
    [SerializeField] protected GameObject _innerSumo;

    [Header("Sumo Canvas Atrributes")]
    [SerializeField] protected string _name;
    [SerializeField] protected TextMeshProUGUI _nameText;
    [SerializeField] protected TextMeshProUGUI _feedScoreText;
    
    [Header("Score Attributes")]
    protected int _feedScore = 100;
    protected int _pushScore = 1200;
    public int _totalScore = 0;

    [Header("Force Attributes")]
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _RotateSpeed;
    [SerializeField] private float _pushForce;
    [SerializeField] private float _pushBackForce;

    [Header("Scale Attributes")]
    [SerializeField] protected Vector3 _currentScale;
    [SerializeField] protected Vector3 _normalScale;
    [SerializeField] protected float _scaleFactor;

    [Header("Movement Attributes")]
    public Vector3 _moveDirection;

    protected bool _isCrushed;


    void OnEnable()
    {
        EventManager.OnFeeding += OnScaleUp;
    }

    void OnDisable()
    {
        EventManager.OnFeeding -= OnScaleUp;
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

    public void SetSumoName(string name)
    {
        _name = name;
        _nameText.text = _name;
    }


    public void OnPushedFrom(Sumo sumo)
    {
        if(!_isCrushed)
        {
            _isCrushed = true;
            _rb.AddForce(sumo._moveDirection * sumo._pushForce , ForceMode.Impulse);
        
            ScaleShiftOnCrush();
            StartCoroutine(WaitAfterCrush());
        }
        
    }


    public void OnPushBack()
    {
        if(!_isCrushed)
        {
            _isCrushed = true;
            _rb.AddForce(-_moveDirection * _pushBackForce , ForceMode.Impulse);

            ScaleShiftOnCrush();
            StartCoroutine(WaitAfterCrush());
        }
    }

    
    private void OnScaleUp(Sumo sumo)
    {
        Vector3 _tempScale = _scaleFactor * Vector3.one;
        _currentScale += _tempScale;

        sumo.transform.DOScale(_currentScale,0.3f).SetEase(Ease.OutBounce);
    }

    public void IncreasePushPower()
    {
        _pushForce += 5;
        Debug.Log("push force incresed.");
    }

    private void ScaleShiftOnCrush()
    {
        Vector3 temp = _currentScale + new Vector3(0.25f,0,0.25f);
        transform.DOScale(temp,0.2f).SetEase(Ease.OutBounce).
        OnComplete(() => transform.DOScale(_currentScale,0.2f).SetEase(Ease.OutBounce));
    }
    

    public abstract void MoveForward();
    public abstract void OnScoreUpOnFeed();
    public abstract void OnScoreUpOnPush(Sumo sumo);

    IEnumerator WaitAfterCrush()
    {
        yield return new WaitForSeconds(0.25f);
        _isCrushed = false;
    }


}
