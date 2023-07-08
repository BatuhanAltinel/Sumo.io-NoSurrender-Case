using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerSumo : Sumo
{

    [SerializeField] private FloatingJoystick _joystick;


    void Update()
    {
        if(GameManager.Instance.IsState(GameState.InGame) && !_isCrushed)
            MoveForward();
    }

    public override void MoveForward()
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

            EventManager.OnScoreUpdate.Invoke(this._totalScore);
        });

       
    }

}
