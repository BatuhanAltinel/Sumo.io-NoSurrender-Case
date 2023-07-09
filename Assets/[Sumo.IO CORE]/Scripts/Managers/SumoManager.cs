using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumoManager : Singleton<SumoManager>
{
  [SerializeField] List<AISumo> _sumoAIs = new();
  [SerializeField] List<Sumo> _allSumos = new();
  [SerializeField] GameObject _ground;
  int _sumoCount;
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
    SetAllAISumosTarget();

    _sumoCount = _allSumos.Count;
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

  public void SetAllAISumosTarget()
  {
    for (int i = 0; i < _sumoAIs.Count; i++)
    {
      FindTargetSumo(_sumoAIs[i]);
    }
  }

  private void FindTargetSumo(AISumo aiSumo)
  {
    foreach (Sumo targetSumo in _allSumos)
    {
      if(aiSumo != targetSumo  && !targetSumo._fallingDown && !targetSumo._selectedAsTarget)
      {
        aiSumo.SetTargetSumo(targetSumo);
        return;
      }
    }
  }

  public void CheckSumoCountForWin()
  {
    if(_sumoCount == 1)
    {
      EventManager.OnGameStateChanged(GameState.Win);
    }
  }

  public void MinusSumoCount()
  {
    _sumoCount--;
  }

  public void ResetSumoCount()
  {
    _sumoCount = _allSumos.Count;
  }

  public int GetCurrentSumoCount()
  {
    return _sumoCount;
  }

}
