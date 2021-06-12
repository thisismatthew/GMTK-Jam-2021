using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInputs
{
    public float HorizonalMovement;
    public bool JumpDown;
    public bool JumpUp;
    public bool Action;
    public bool Abandon;
}

public class PlayerInput : MonoBehaviour
{
    public Controller CurrentController;
    private PlayerInputs _currentInput;

    void Update()
    {
        _currentInput.HorizonalMovement = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            _currentInput.JumpDown = true;     
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W))
            _currentInput.JumpUp = true;
        if (Input.GetKeyDown(KeyCode.E))
            _currentInput.Action = true;
        if (Input.GetKeyDown(KeyCode.Q))
            _currentInput.Abandon = true;

        CurrentController.RecieveInputs(ref _currentInput);
        ResetInput();
    }

    private void ResetInput()
    {
        _currentInput.HorizonalMovement = 0f;
        _currentInput.JumpDown = false;
        _currentInput.JumpUp = false;
        _currentInput.Action = false;
        _currentInput.Abandon = false;
    }
}
