using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;

public abstract class Sumo : MonoBehaviour
{
    protected Rigidbody _rb;
    public GameObject _innerSumo;

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
    public bool _fallingDown = false;
    public bool _selectedAsTarget = false;

    


    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _currentScale = transform.localScale;
        _normalScale = _currentScale;
    }

    public abstract void Movement();
    public abstract void OnScoreUpOnFeed();
    public abstract void OnScoreUpOnFallTo(Sumo sumo);


    public void ResetAllAttributesOnState(GameState state)
    {
        if(GameManager.Instance.IsState(GameState.ReadyToStartGame))
        {
            _nameText.text = _name;
            _currentScale = _normalScale;
            transform.localScale = _normalScale;
            _fallingDown = false;
            _selectedAsTarget = false;
            _totalScore = 0;
            _rb.velocity = Vector3.zero;
        }
        
    }

    public void SetSumoName(string name)
    {
        _name = name;
        _nameText.text = _name;
    }

    public string GetSumoName(Sumo sumo)
    {
        return sumo._name;
    }


    public void OnPushedFrom(Sumo sumo)
    {
        if(!_isCrushed)
        {
            _isCrushed = true;
            _rb.AddForce(sumo._moveDirection * sumo._pushForce , ForceMode.Impulse);
        
            StartCoroutine(WaitAfterCrush());
        }
    }

    public void OnPushedFrom(Sumo sumo,float pushForce)
    {
        if(!_isCrushed)
        {
            _isCrushed = true;
            _rb.AddForce(sumo._moveDirection * pushForce , ForceMode.Impulse);
        
            StartCoroutine(WaitAfterCrush());
        }
    }


    public void OnPushBack()
    {
        if(!_isCrushed)
        {
            _isCrushed = true;
            _rb.AddForce(-_moveDirection * _pushBackForce , ForceMode.Impulse);

            LeanBack();
            StartCoroutine(WaitAfterCrush());
        }
    }

    
    public void ScaleUp()
    {
        Vector3 _tempScale = _scaleFactor * Vector3.one;
        _currentScale += _tempScale;

        transform.DOScale(_currentScale,0.3f).SetEase(Ease.OutBounce);
    }

    public void IncreasePushPower()
    {
        _pushForce += 2;
    }

    public void ScaleShiftOnCrash()
    {
        Vector3 temp = _currentScale + new Vector3(0.25f,0,0.25f);
        transform.DOScale(temp,0.2f).SetEase(Ease.OutBounce).
        OnComplete(() => transform.DOScale(_currentScale,0.2f).SetEase(Ease.OutBounce));
    }

    public void SetSumoFallen()
    {
        _fallingDown = true;
    }

    void LeanBack()
    {
        _innerSumo.transform.DORotate(new Vector3(-12f,0,0),0.2f,RotateMode.LocalAxisAdd).
        OnComplete(()=>  _innerSumo.transform.DORotate(new Vector3(12f,0,0),0.2f,RotateMode.LocalAxisAdd));
    }

    public float GetPushForce()
    {
        return _pushForce;
    }


    IEnumerator WaitAfterCrush()
    {
        yield return new WaitForSeconds(0.3f);
        _rb.velocity = Vector3.zero;
        _isCrushed = false;
    }


}
