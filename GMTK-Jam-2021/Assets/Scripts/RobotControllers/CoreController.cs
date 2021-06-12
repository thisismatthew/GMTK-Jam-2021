using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreController : Controller
{
    private PlayerInput _input;
    private FixedJoint2D _joint;
    private Transform _lastConnectionPoint;
    public float ReconectionRadius = 2f;

    private void Start()
    {
        _joint = GetComponent<FixedJoint2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerInput>();
    }
    void FixedUpdate()
    {
        if (_jumpCharging)
        {
            _jumpChargeTime += Time.deltaTime;
        }

        UpdateJumpVelocity();
        UpdateHorizontalMovementVelocity();
        Move(_velocity);

        if (_jumpCompleted)
        {
            _velocity.y = 0;
            _jumpCompleted = false;
            _jumpChargeTime = 0;
            _jumpRequested = false;
        }

        if (_lastConnectionPoint != null)
        {
            if (Vector2.Distance(_lastConnectionPoint.transform.position, this.transform.position)> ReconectionRadius)
            {
                _lastConnectionPoint.GetComponent<Attatcher>().CoreConected = false;
            }
        }


    }

    public void ConnectToHost(GameObject host, GameObject attatcher)
    {
        _lastConnectionPoint = attatcher.transform;
        _input.CurrentController = host.GetComponent<Controller>();
        _joint.enabled = true;
        _joint.connectedBody = host.GetComponent<Rigidbody2D>();

        _velocity = Vector2.zero;
    }
}
