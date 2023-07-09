using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AISumo : Sumo
{
    public Sumo _targetSumo;




    protected override void Awake()
    {
        base.Awake();
    }
    
    void Update()
    {
        Movement();
    }


    public override void Movement()
    {
        if(GameManager.Instance.IsState(GameState.InGame) && !_isCrushed)
        {
           RotateToTheTarget();
        //    _rb.MovePosition(_rb.position + _moveDirection);
            transform.Translate(-_moveDirection * _moveSpeed * Time.deltaTime);
        }
    }


    public override void OnScoreUpOnPush(Sumo sumo)
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
        });

    }

    public void SetTargetSumo(Sumo sumo)
    {
        _targetSumo = sumo;
        _targetSumo._selectedAsTarget = true;
    }
    private void RotateToTheTarget()
    {
        Vector3 direction = Vector3.RotateTowards(_innerSumo.transform.forward, _targetSumo._moveDirection, _RotateSpeed * Time.deltaTime, 0.0f);
        _innerSumo.transform.rotation = Quaternion.LookRotation(_targetSumo.transform.position);
        _moveDirection = direction;
    }
}
