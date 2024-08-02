using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;


public partial class MoveAndAnimatorController : MonoBehaviour
{

    void SubscribeInput()
    {

        playerInputs.Movement.Move.started += OnMovementInput;
        playerInputs.Movement.Move.performed += OnMovementInput;
        playerInputs.Movement.Move.canceled += OnMovementInput;

        playerInputs.Movement.Leave.performed += OnLeave;
        playerInputs.Movement.Leave.canceled -= OnLeave;

        playerInputs.Movement.Run.started += OnRun;
        playerInputs.Movement.Run.canceled += OnRun;

        playerInputs.Movement.Crounch.performed += OnCrouch;
        playerInputs.Movement.Crounch.canceled += OnCrouch;

        playerInputs.Movement.Jump.performed += OnJump;
        playerInputs.Movement.Jump.canceled += OnJump;

        playerInputs.Movement.InteractObject.performed += OnInteract;
        playerInputs.Movement.InteractObject.canceled += OnInteract;

        playerInputs.Movement.DropObject.started += OnDrop;
        playerInputs.Movement.DropObject.canceled += OnDrop;

        playerInputs.Movement.Release.performed += OnRelease;
        playerInputs.Movement.Release.canceled += OnRelease;

        playerInputs.Movement.Projectile.started += OnTrajectory;
        playerInputs.Movement.Projectile.canceled += OnTrajectory;

    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void OnRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void OnLeave(InputAction.CallbackContext context)
    {
        isLeavePressed = context.ReadValueAsButton();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        // Debug.Log(isJumpPressed);
    }

    void OnCrouch(InputAction.CallbackContext context)
    {
        isCrouchPressed = context.ReadValueAsButton();
    }

    void OnInteract(InputAction.CallbackContext context)
    {
        isInteractPressed = context.ReadValueAsButton();
    }

    void OnDrop(InputAction.CallbackContext context)
    {
        isDropPressed = context.ReadValueAsButton();
    }

    void OnRelease(InputAction.CallbackContext context)
    {
        isReleasePressed = context.ReadValueAsButton();
    }

    void OnTrajectory(InputAction.CallbackContext context)
    {
        isTrajectoryPressed = context.ReadValueAsButton();
    }


}
