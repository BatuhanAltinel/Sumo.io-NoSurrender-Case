using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AISumo : Sumo
{
    [SerializeField] Sumo _targetSumo;

    protected override void Awake()
    {
        base.Awake();
    }
    
    public override void MoveForward()
    {

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
}
