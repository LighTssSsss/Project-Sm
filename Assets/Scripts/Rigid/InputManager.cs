using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerController playerControllers;
    PlayerLocomotion playerLocomotion;
    AnimatorManager animatorManager;


    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;


    public float moveAmout;
    public float verticalInput;
    public float horizontalInput;


    public bool inputRun;
    public bool inputJump;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void OnEnable()
    {
        if (playerControllers == null)
        {
            playerControllers = new PlayerController();


            playerControllers.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControllers.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();


            playerControllers.PlayerActions.B.performed += i => inputRun = true;
            playerControllers.PlayerActions.B.canceled += i => inputRun = false;
            playerControllers.PlayerActions.Jump.performed += i => inputJump = true;

        }

        playerControllers.Enable();
    }


    private void OnDisable()
    {
        playerControllers.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpInput();
        //HandleActionInput;
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        moveAmout = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmout, playerLocomotion.isSprinting);

    }


    private void HandleSprintingInput()
    {
        if (inputRun && moveAmout > 0.5f)
        {
            playerLocomotion.isSprinting = true;

        }

        else
        {
            playerLocomotion.isSprinting = false;
        }
    }


    private void HandleJumpInput()
    {
        if (inputJump)
        {
            inputJump = false;
            playerLocomotion.HandleJumping();
        }
    }
}
