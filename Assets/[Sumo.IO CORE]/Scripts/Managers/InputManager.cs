using UnityEngine;

public class InputManager : Singleton<InputManager>
{

  public bool InputEnable { get; set; }

  void Start()
  {
    InputEnable = true;
  }

  void Update()
  {
    Inputs();
  }

  void Inputs()
  {
    if(Input.GetMouseButton(0) && InputEnable)
    {

      if((GameManager.Instance.IsState(GameState.ReadyToStartGame)))
      {
	EventManager.OnGameStateChanged.Invoke(GameState.OnTimer);
      }
      else if(GameManager.Instance.IsState(GameState.End))
      {
	EventManager.OnGameStateChanged.Invoke(GameState.ReadyToStartGame);
      }
      
     }
   }

}
