using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class WalkerController : Controller
{
    [Header("IK Animation")]
    public float StepSpeed = 0.1f;
    public float StepSize = 1f;
    public float FootHeightOffset = 0.95f;
    public Transform[] AnimationTargets;
    public Transform[] SolverTargets;
    public Transform[] UpdateTargets;
    public LayerMask CollisionLayer;
    private bool _plungeRequested = false;
    private bool _plungeComplete = true;
    private float _plungeTimer = 0f;
    public Animator plunger;

    void FixedUpdate()
    {
        
        UpdateHorizontalMovementVelocity();
        Move(_velocity);

       
    }

    protected override void Move(Vector2 newVelocity)
    {
        
        if (!IsGrounded())
            newVelocity.y = 0;

        base.Move(newVelocity);
    }

    public override void RecieveInputs(ref PlayerInputs inputs)
    {
        if (inputs.JumpDown && _plungeComplete == true)
        {
            _plungeRequested = true;
            plunger.Play("plungerAnim");
            Debug.Log("Play");
        }
        if (_plungeRequested)
        {
            _plungeTimer += Time.deltaTime;
        }
        if (_plungeTimer>= 1.5f)
        {
            _plungeComplete = true;
            _plungeRequested = false;
            _plungeTimer = 0;
        }

        _horizontalInput = inputs.HorizonalMovement;
        _verticalInput = inputs.VerticalMovement;
        
    }

    public void Update()
    {
        

        if (IsGrounded())
        {
            GetComponent<IKManager2D>().enabled = true;
            for (int i = 0; i<SolverTargets.Length; i++)
            {
                SnapFeetToGround(ref SolverTargets[i], ref AnimationTargets[i], UpdateTargets[i]);
            }
        }
        else
        {
            GetComponent<IKManager2D>().enabled = false;
        }

    }

    public void SnapFeetToGround(ref Transform solverTarget, ref Transform animationTarget, Transform updateTarget)
    {

        //shoot a blue ray down to the ground from the update point that moves with the player
        RaycastHit2D updateTargetHit = Physics2D.Raycast(updateTarget.position, Vector2.down, 100f, CollisionLayer);
        Debug.DrawLine(updateTarget.position, updateTargetHit.point, Color.blue);

        //if the solver target gets too far from the update target position
        //move the animation target to the update targets position. 
        if (Vector2.Distance(solverTarget.transform.position, updateTargetHit.point) > StepSize)
        {
            //Debug.Log("pointMoved");
            animationTarget.position = updateTarget.position;
        }

        //shoot a cyan ray down to the ground to show where the solver targets new position should be. 
        RaycastHit2D animationTargetHit = Physics2D.Raycast(animationTarget.position, Vector2.down, 100f, CollisionLayer);
        Debug.DrawLine(animationTarget.position, animationTargetHit.point, Color.cyan);

        //add an offset for the height of the feet.
        Vector2 offset = new Vector2(0, FootHeightOffset);

        //smoothly lerp the solver towards that position
        solverTarget.position = Vector2.Lerp(solverTarget.position, animationTargetHit.point + offset, StepSpeed);

        Debug.DrawLine(solverTarget.position, updateTargetHit.point + offset, Color.red);
            

    }
}
