using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [Header("Movement")]
    public float Acceleration = 0.01f;
    public float MaxSpeed = 6f;
    protected Vector2 _velocity = Vector2.zero;
    protected float _horizontalInput = 0f;

    [Header("Jumping")]
    public float ChargeUpTime = 0.5f;
    public float RegularJumpHeight = 10f;
    public float ChargedJumpHeight = 15f;
    public float TimeToJumpApex = 0.5f;
    protected bool _jumpCharging = false;
    protected bool _jumpRequested = false;
    protected bool _jumpCompleted = false;
    protected float _jumpChargeTime = 0f;
    protected bool _grounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    public virtual void RecieveInputs(ref PlayerInputs inputs)
    {
        if (inputs.JumpDown)
        {
            _jumpCharging = true;
        }
        if (inputs.JumpUp)
        {
            _jumpCharging = false;
            _jumpRequested = true;
        }
        _horizontalInput = inputs.HorizonalMovement;
    }

    protected void UpdateHorizontalMovementVelocity()
    {
        if (_horizontalInput > 0.01f || _horizontalInput < -0.01f)
        {
            _velocity.x += _horizontalInput * Acceleration;
        }
        else
        {
            _velocity.x = 0;
        }
    }

    protected void UpdateJumpVelocity()
    {
        float gravity, jumpHeight;
        //check if jump has been requested
        if (_jumpRequested)
        {
            Debug.Log("Jumping");
            //check if the player charged enough for a super jump. 
            if (_jumpChargeTime > ChargeUpTime)
                jumpHeight = ChargedJumpHeight;
            else
                jumpHeight = RegularJumpHeight;
            _jumpCompleted = true;
            //calculate jump velocity
            gravity = CalculateJumpGravity(jumpHeight);
            _velocity.y = gravity * TimeToJumpApex;
        }
    }
    protected virtual void Execute() { }

    protected float CalculateJumpGravity(float jumpHeight)
    {
        return (2 * jumpHeight) / Mathf.Pow(2, TimeToJumpApex);
    }

    protected bool IsGrounded()
    {

        Vector2 direction = Vector2.down;
        float distance = 1f;
        Color debugGroundColor = Color.red;
        Vector2 position = transform.position;
        position.y += distance;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance);
        if (hit.collider != null)
        {
            debugGroundColor = Color.green;
            Debug.DrawLine(position, (position + (Vector2.down * distance)), debugGroundColor);
            return true;
        }

        Debug.DrawLine(position, (position + (Vector2.down * distance)), debugGroundColor);
        return false;
    }

    protected void Move(Vector2 newVelocity)
    {
        //make sure we only add to the y axis when we are grounded
        if (!IsGrounded())
            newVelocity.y = 0;

        //make sure that we are adding negative force to keep us under the speed limit
        if (_rigidbody.velocity.x > MaxSpeed)
        {
            //find the amount we are over the speed limit and offset it. 
            Vector2 diff = new Vector2(-(_rigidbody.velocity.x - MaxSpeed), 0);
            newVelocity += diff;
        }

        if (_rigidbody.velocity.x < -MaxSpeed)
        {
            //find the amount we are under the speed limit and offset it. 
            Vector2 diff = new Vector2(-(_rigidbody.velocity.x + MaxSpeed), 0);
            newVelocity += diff;
        }



        _rigidbody.velocity += newVelocity;


    }
}
