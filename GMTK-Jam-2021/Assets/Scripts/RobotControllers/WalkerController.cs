using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerController : Controller
{
    [Header("IK Animation")]
    public float StepSpeed = 0.1f;
    public float StepSize = 1f;
    public Transform[] AnimationTargets;
    public Transform[] SolverTargets;
    public Transform[] UpdateTargets;
    public LayerMask CollisionLayer;

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

    public void Update()
    {
        if (IsGrounded())
        {
            for (int i = 0; i<SolverTargets.Length; i++)
            {
                SnapFeetToGround(ref SolverTargets[i], ref AnimationTargets[i], UpdateTargets[i]);
            }

        }

    }

    public void SnapFeetToGround(ref Transform solverTarget, ref Transform animationTarget, Transform updateTarget)
    {
        //shoot a blue ray down to the ground from the update point that moves with the player
        RaycastHit2D updateTargetHit = Physics2D.Raycast(updateTarget.position, Vector2.down, 3f, CollisionLayer);
        Debug.DrawLine(updateTarget.position, updateTargetHit.point, Color.blue);

        //if the solver target gets too far from the update target position
        //move the animation target to the update targets position. 
        if (Vector2.Distance(solverTarget.transform.position, updateTargetHit.point) > StepSize)
        {
            //Debug.Log("pointMoved");
            animationTarget.position = updateTarget.position;
        }

        //shoot a cyan ray down to the ground to show where the solver targets new position should be. 
        RaycastHit2D animationTargetHit = Physics2D.Raycast(animationTarget.position, Vector2.down, 3f, CollisionLayer);
        Debug.DrawLine(animationTarget.position, animationTargetHit.point, Color.cyan);

        //smoothly lerp the solver towards that position
        solverTarget.position = Vector2.Lerp(solverTarget.position, animationTargetHit.point, StepSpeed);

            

    }
}
