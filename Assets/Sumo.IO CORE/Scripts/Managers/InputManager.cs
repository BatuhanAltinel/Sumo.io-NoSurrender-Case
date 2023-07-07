using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
	[SerializeField]
	private GameObject[] _uIElements;

	private PlayerInput _playerInput;
	private InputAction _moveLeftAction;
	private InputAction _moveRightAction;

	private bool _isInputDisabled;
	private bool _isTouchDown;
	
	public bool IsTouchDown() { return _isTouchDown; }

	public void SetTouchDown(bool state) { _isTouchDown = state; }

	private void OnEnable()
	{
		EventManager.OnInputEnabled += OnInputEnabled;
		EventManager.OnInputDisabled += OnInputDisabled;
	}

	private void OnDisable()
	{
		EventManager.OnInputEnabled -= OnInputEnabled;
		EventManager.OnInputDisabled -= OnInputDisabled;
	}

	protected override void Awake()
	{
		base.Awake();

		_playerInput = GetComponent<PlayerInput>();
		_moveLeftAction = _playerInput.actions["Move Left"];
		_moveRightAction = _playerInput.actions["Move Right"];
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

	private bool IsUIElementTouched()
	{
		PointerEventData eventDataCurrentPosition = new(EventSystem.current)
		{
			position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
		};
		List<RaycastResult> results = new();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

		for (int i = 0; i < results.Count; i++)
		{
			foreach (GameObject go in _uIElements)
			{
				if (go == results[i].gameObject) return true;
			}
		}

		return false;
	}

}
