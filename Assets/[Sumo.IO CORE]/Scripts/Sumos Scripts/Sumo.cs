using UnityEngine;
using DG.Tweening;

public abstract class Sumo : MonoBehaviour
{
    protected Rigidbody _rb;

    [SerializeField] protected GameObject _innerSumo;

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



    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _currentScale = _innerSumo.transform.localScale;
        _normalScale = _currentScale;

        SetOnStart();
    }

    void Start()
    {
        
    }

    void SetOnStart()
    {
        _currentScale = _normalScale;
    }

    protected virtual void MoveForward(){}
    protected virtual void PushOpposite(Vector3 dir){}
    
    void OnScaleUp()
    {
        Vector3 _tempScale = _scaleFactor * Vector3.one;
        _currentScale += _tempScale;

        transform.DOScale(_currentScale,0.3f).SetEase(Ease.OutBounce);
    }
    
}
