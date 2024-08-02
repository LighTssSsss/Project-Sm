using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
    PlayerManager playerManager;
    AnimatorManager animatorManager;


    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingSpeed;
    public float rayCastHeightOffSet = 0.5f;
    public float sphereRadius = 0.2f;
    public float maxDistanceToGround;
    public LayerMask groundLayer;




    [Header("Movement Flags")]
    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;

    [Header("Movement Speeds")]
    public float walkingSoeed = 1.5f;
    public float runningSpeed = 5f;
    public float sprintingSpeed = 7f;
    public float rotationSpeed = 15;


    [Header("Jump Speeds")]
    public float gravityIntesity = -15;
    public float jumpHeight = 3;



    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        isGrounded = true;
        playerManager = GetComponent<PlayerManager>();
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        // HandleFallingAndLanding();
        HandleFallingAndLading();
        if (playerManager.isInteracting)
        {
            return;
        }

           
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if (isJumping)
        {
            return;
        }

        moveDirection = cameraObject.forward * inputManager.verticalInput; //moveDirection = new Vector3(cameraObject.forward.x, 0f, cameraObject.forward.z) * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * walkingSoeed;


        if (isSprinting)
        {
            moveDirection = moveDirection * sprintingSpeed;
        }

        else
        {
            if (inputManager.moveAmout >= 0.5f)
            {
                moveDirection = moveDirection * runningSpeed;
            }

            else
            {
                moveDirection = moveDirection * walkingSoeed;
            }
        }


       


        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;
    }


    private void HandleRotation()
    {
        if (isJumping)
        {
            return;
        }

        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if(targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        //Vector3 rayCastOrigin = transform.position;
        //rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffSet;
        Vector3 rayCastOrigin = transform.position + Vector3.up * rayCastHeightOffSet;
       

        if(!isGrounded)
        {
            if (!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Falling", true);
            }

            inAirTimer = inAirTimer + Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(Vector3.down * fallingSpeed * inAirTimer);

        }

        /* if(Physics.SphereCast(rayCastOrigin, sphereRadius, Vector3.down, out hit, groundLayer))
         {
             //Debug.DrawRay(rayCastOrigin, -Vector3.up * 1f, Color.red);
             if (!isGrounded)
             {
                if (playerManager.isInteracting)
                {
                    animatorManager.PlayTargetAnimation("Lading", true);

                }
                inAirTimer = 0;
            }     
             
             isGrounded = true;

         }*/

        if(Physics.SphereCast(rayCastOrigin, sphereRadius, Vector3.down, out hit, groundLayer))
         {
             //Debug.DrawRay(rayCastOrigin, -Vector3.up * 1f, Color.red);
             if (!isGrounded && !isJumping)
             {
                if (playerManager.isInteracting)
                {
                    animatorManager.PlayTargetAnimation("Lading", true);

                }
                inAirTimer = 0;
            }     
             
             isGrounded = true;

         }

        else
        {
            isGrounded = false;
        }
    }

    private void HandleFallingAndLading()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position + Vector3.up * rayCastHeightOffSet;

        Vector3 targetPosition;
        targetPosition = transform.position;

        if (!isGrounded)
        {
            if (!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Falling", true);
            }

            inAirTimer += Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(Vector3.down * fallingSpeed * inAirTimer);
        }

        if (Physics.Raycast(rayCastOrigin, Vector3.down, out hit, maxDistanceToGround, groundLayer))
        {
            if (!isGrounded)
            {
                if (playerManager.isInteracting)
                {
                    animatorManager.PlayTargetAnimation("Lading", true);
                }

               
                inAirTimer = 0;
            }

            Vector3 rayCastHitPoint = hit.point;
            targetPosition.y = rayCastHitPoint.y;

            isGrounded = true;

            playerManager.isInteracting = false;
        }
        else
        {
            isGrounded = false;
        }

        if(isGrounded && !isJumping)
        {
            if(playerManager.isInteracting || inputManager.moveAmout > 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
            }

            else
            {
                transform.position = targetPosition;
            }
        }
    }

    public void HandleJumping()
    {
        if (isGrounded)
        {
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jump",false);


            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntesity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            playerRigidbody.velocity = playerVelocity;
        }
    }


}
