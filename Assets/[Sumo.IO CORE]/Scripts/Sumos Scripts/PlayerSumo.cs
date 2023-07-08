using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSumo : Sumo
{

    [SerializeField] private FloatingJoystick _joystick;


    void Update()
    {
        if(GameManager.Instance.IsState(GameState.InGame))
            MoveForward();
    }

    public void SetForwardDirection(Vector2 dir)
    {
        _moveDirection = new Vector3(dir.x,_moveDirection.y,dir.y);
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

    public override void PushOpposite(Vector3 dir)
    {
        
    }

    public override void OnScoreUp(Sumo sumo)
    {
        base.OnScoreUp(sumo);

        // EventManager.OnScoreUpdate.Invoke(_score);
    }

}
