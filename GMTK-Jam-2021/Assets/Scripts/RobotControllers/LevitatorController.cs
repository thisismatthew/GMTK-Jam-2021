using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitatorController : Controller
{
    public Transform _target;
    private Vector2 _newTargetPos;
    public GameObject LevitationObject;

    private void Awake()
    {
        _newTargetPos = _target.position;
    }

    private void FixedUpdate()
    {
        //Target controlls
        if (_horizontalInput > 0.01f || _horizontalInput < -0.01f)
        {
            
            _newTargetPos.x = _target.transform.position.x + _horizontalInput;
        }
        if (_verticalInput > 0.01f || _verticalInput < -0.01f)
        {
            _newTargetPos.y = _target.transform.position.y + _verticalInput;
        }
        _target.position = Vector2.Lerp(_target.position, _newTargetPos, 0.05f);

        if (_jumpCharging && LevitationObject != null)
        {
            LevitationObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            LevitationObject.transform.position = Vector2.Lerp(_target.position, LevitationObject.transform.position, 0.05f);
        }

        if (!_jumpCharging)
        {
            LevitationObject = null;
        }

    }

}
