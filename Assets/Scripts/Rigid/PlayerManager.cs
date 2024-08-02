using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    InputManager inputManager;
    //CameraManager cameraManager;
    PlayerLocomotion playerLocomotion;
  //  CollisionCamera cameraCollision;

    public bool isInteracting;
    


    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        //cameraManager = FindFirstObjectByType<CameraManager>();
        playerLocomotion = GetComponent <PlayerLocomotion> ();
       // cameraCollision = FindFirstObjectByType<CollisionCamera>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        //cameraManager.HandleAllCameraMovement();
        //cameraCollision.HandleAllCollision();


        isInteracting = animator.GetBool("isInteracting");
        playerLocomotion.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", playerLocomotion.isGrounded);
    }

   
}
