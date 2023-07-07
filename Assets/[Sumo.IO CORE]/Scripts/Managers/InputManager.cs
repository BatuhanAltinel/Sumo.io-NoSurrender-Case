using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
	// [SerializeField]
	// private GameObject[] _uIElements;

	[SerializeField] PlayerSumo _playerSumo;

	private PlayerInput _playerInput;
	private InputAction _moveHorizontalAction;

	private bool _isInputDisabled;
	private bool _isTouchDown;
	
	public bool IsTouchDown() { return _isTouchDown; }

	public void SetTouchDown(bool state) { _isTouchDown = state; }

	private void OnEnable()
	{
		EventManager.OnInputEnabled += OnInputEnabled;
		EventManager.OnInputDisabled += OnInputDisabled;

		_moveHorizontalAction.performed += MoveHorizontalPerformed;
		_moveHorizontalAction.canceled += MoveHorizontalCanceled;
	}

	private void OnDisable()
	{
		EventManager.OnInputEnabled -= OnInputEnabled;
		EventManager.OnInputDisabled -= OnInputDisabled;

		_moveHorizontalAction.performed -= MoveHorizontalPerformed;
		_moveHorizontalAction.canceled -= MoveHorizontalCanceled;
	}

	protected override void Awake()
	{
		base.Awake();

		_playerInput = GetComponent<PlayerInput>();
		_moveHorizontalAction = _playerInput.actions["Move Horizontal"];
		_isInputDisabled = false;
		_isTouchDown = false;
	}

	private void OnInputEnabled()
	{
		_isInputDisabled = false;
	}

	private void OnInputDisabled()
	{
		_isInputDisabled = true;
	}

	private void MoveHorizontalPerformed(InputAction.CallbackContext context)
	{
		if(GameManager.Instance._gameState == GameState.InGame)
		{
			_isTouchDown = true;
			Vector2 temp = _moveHorizontalAction.ReadValue<Vector2>();
			Debug.Log("temp vector2 value :" + temp.normalized);
			_playerSumo.SetForwardDirection(temp.normalized);
		}
		
	}

	private void MoveHorizontalCanceled(InputAction.CallbackContext context)
	{
		_isTouchDown =false;
	}
	
	// private bool IsUIElementTouched()
	// {
	// 	PointerEventData eventDataCurrentPosition = new(EventSystem.current)
	// 	{
	// 		position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
	// 	};
	// 	List<RaycastResult> results = new();
	// 	EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

	// 	for (int i = 0; i < results.Count; i++)
	// 	{
	// 		foreach (GameObject go in _uIElements)
	// 		{
	// 			if (go == results[i].gameObject) return true;
	// 		}
	// 	}

	// 	return false;
	// }

}
