using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInputs
{
    public float HorizonalMovement;
    public float VerticalMovement;
    public bool JumpDown;
    public bool JumpUp;
    public bool Action;
}

public class PlayerInput : MonoBehaviour
{
    public Controller CurrentController;
    private Controller _core;
    private PlayerInputs _currentInput;
    private FixedJoint2D _joint;

    private void Start()
    {
        _joint = GetComponent<FixedJoint2D>();
        _core = CurrentController;
    }

    void Update()
    {
        _currentInput.HorizonalMovement = Input.GetAxisRaw("Horizontal");
        _currentInput.VerticalMovement = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
            _currentInput.JumpDown = true;     
        if (Input.GetKeyUp(KeyCode.Space))
            _currentInput.JumpUp = true;
        if (Input.GetKeyDown(KeyCode.E))
        {
            CurrentController = _core;
            _joint.enabled = false;
            _joint.connectedBody = null;
        }
            

        CurrentController.RecieveInputs(ref _currentInput);
        ResetInput();
    }

    private void ResetInput()
    {
        _currentInput.VerticalMovement = 0f;
        _currentInput.HorizonalMovement = 0f;
        _currentInput.JumpDown = false;
        _currentInput.JumpUp = false;
        _currentInput.Action = false;
    }
}
