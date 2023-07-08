using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
	void Update()
	{
		Inputs();
	}

	void Inputs()
	{
		if(Input.GetMouseButton(0))
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
