using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumoManager : MonoBehaviour
{
    [SerializeField] List<AISumo> _sumoAIs = new();
    [SerializeField] GameObject _ground;
    float _areaRadius;

    void OnEnable()
    {
        EventManager.OnGameStateChanged += SetAIsPositions;
    }

    void OnDisable()
    {
        EventManager.OnGameStateChanged -= SetAIsPositions;
    }
    void Start()
    {
        _areaRadius = _ground.transform.localScale.x / 2;

        SetAIsNames();
    }

    void SetAIsPositions(GameState state)
    {
        if(GameManager.Instance.IsState(GameState.OnTimer))
        {
          for (int i = 0; i < _sumoAIs.Count; i++)
          {
            _sumoAIs[i].transform.position = new Vector3(GetRandomPositionPoint(),1,GetRandomPositionPoint());
          }
        }
        
    }

    void SetAIsNames()
    {
        for (int i = 0; i < _sumoAIs.Count; i++)
          {
            _sumoAIs[i].SetSumoName("Sumo_" + (i+1));       
          }
    }

    float GetRandomPositionPoint()
    {
        float radius = 0;
        radius = Random.Range(-_areaRadius/2,_areaRadius/2);

        return radius;
    }


}
