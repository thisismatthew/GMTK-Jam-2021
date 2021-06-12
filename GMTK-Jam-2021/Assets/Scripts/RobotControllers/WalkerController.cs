using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerController : Controller
{
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
    }
}
