using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class MoveAndAnimatorController : MonoBehaviour
{
    PlayerInputs playerInputs;
    public CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunnigHash;
    int isSprintigHash;
    int isJumpingHash;
    int isFallingHash;
    int isLandingHash;
    int isCrouchHash;
    int isMoveCrouchHash;
    int isPushHash;
    int isPushMoveHash;
    int isDropHash;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    Transform cameraObject;

    bool isMovementPressed;
    bool isRunPressed;
    bool isSprintTime;
    bool isSprint;
    bool isJump;
    bool isCrouch;
    bool push;
    bool interactPush;
    bool dropObject;

    public bool playerInAction { get; private set; }
    public bool isJumpAnimation = false;
    public bool isFallingg;
    public bool isClimbing;
    public bool isLeavePressed;
    public bool isCrouchPressed;
    public bool isInteractPressed;
    public float pushForce;


    bool isLanding;
    public bool playerControl = true;
    Quaternion requiredRotation;

    public bool isJumpPressed = false;
    float initialJumpVelocity;
    //public float maxJumpHeight = 1;
    // public float maxJumpTime = 0.5f;
    public bool isJumping = false;
    public float timeSprint;
    Vector3 initialPosition;

    public float rotationFactorPerFrame;
    public float runMultiplier;
    public float multiplierSprint;
    public float maxRayDistance;


    [Header("Gravity Setting")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;


    public bool playerHaging { get; set; }

    [Header("Jump")]
    [SerializeField] private float jumpPower;
    public bool canJump = true;

    [SerializeField] private float jumpCooldown;

    private float velocityG;
    public float timeInAir;
    public float minTimeInAirForFall;
    public float landingAnimationDuration;

    [Header("Fall and Land")]
    public LayerMask groundLayer;
    public float minFallHeightForAnimation;



    //private EnviromentChecker checkers;
    [Header("References")]
    private CheckerEnviroment checks;
    private ClimbingSystem climb;
    private MoveableObject moveObject;
    private AreaInteract areaInt;

    [Header("Variable references")]
    public bool pushObject;

    // private MoveAndAnimatorController movement;

    private void OnEnable()
    {
        playerInputs.Movement.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Movement.Disable();
    }



    private void Awake()
    {
        playerInputs = new PlayerInputs();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        checks = GetComponent<CheckerEnviroment>();
        climb = GetComponent<ClimbingSystem>();
        moveObject = GetComponent<MoveableObject>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunnigHash = Animator.StringToHash("isRunning");
        isSprintigHash = Animator.StringToHash("isSprinting");
        isJumpingHash = Animator.StringToHash("isJumping");
        isFallingHash = Animator.StringToHash("isFalling");
        isLandingHash = Animator.StringToHash("isLanding");
        isCrouchHash = Animator.StringToHash("isCrounch");
        isMoveCrouchHash = Animator.StringToHash("isMoveCrouch");
        isPushHash = Animator.StringToHash("isPush");
        isPushMoveHash = Animator.StringToHash("isPushMove");


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

        playerInputs.Movement.InteractAndDrop.performed += OnInteract;
        playerInputs.Movement.InteractAndDrop.canceled += OnInteract;

        cameraObject = Camera.main.transform;

        isFallingg = false;


    }


    void Update()
    {

        if (!playerControl)
        {
            return;
        }


        if (playerHaging)
        {
            return;
        }


        if (isRunPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);

        }

        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }

        if (isSprint)
        {
            //currentRunMovement = currentMovement * runMultiplier * multiplierSprint;
            currentRunMovement *= multiplierSprint;
        }

        characterController.Move(currentMovement * Time.deltaTime);

        Vector3 cameraForward = cameraObject.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        if (!playerInAction)
        {
            currentMovement = cameraForward * currentMovementInput.y + cameraObject.right * currentMovementInput.x;
            currentMovement.Normalize();
        }
         

        currentRunMovement = currentMovement * runMultiplier;

        /* if (!playerControl)
         {
             return;
         }*/





        Gravity();
        // CheckForFalling();
        CheckGrounded();
        HandleRotation();
        HandleAnimation();
        HandleJump();






    }


    private void Gravity()
    {
        bool isFalling = currentMovement.y <= 0.0F;  //ORIGINAl
        float fallMultiplier = 2.0f;
        if (characterController.isGrounded && velocityG < 0.0f)
        {
            velocityG = -1.0f;
            if (isJumpAnimation)
            {
                animator.SetBool(isJumpingHash, false);
                // isJumpAnimation = false;

            }
        }

        else if (isFalling)
        {
            velocityG += gravity * fallMultiplier * Time.deltaTime;
        }

        else
        {
            velocityG += gravity * Time.deltaTime;
        }

        Vector3 gravityVector = new Vector3(0, velocityG, 0);
        characterController.Move(gravityVector * Time.deltaTime);
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



    private void HandleJump()
    {
        /*  if (!isJumping && characterController.isGrounded && isJumpPressed)
          {
              velocityG += jumpPower;

          }
          else if(!isJumpPressed && isJumping && characterController.isGrounded)
          {
              isJumping = false;
          }*/        
            if (!isJumping && characterController.isGrounded && isJumpPressed && canJump && checks.obstacleCollision == false && isClimbing == false)
           {
                animator.SetBool(isJumpingHash, true);
                isJumpAnimation = true;
                velocityG = jumpPower;
                StartCoroutine(WaitJump(jumpCooldown));
                isJumping = true;
                canJump = false;

            }

            else if (isJumping && !isJumpPressed && characterController.isGrounded)
            {
                isJumping = false; // El jugador ya no está en el aire
                animator.SetBool(isJumpingHash, false);
                isJumpAnimation = false;
               checks.obstacleCollision = false;
            }

        
        

    }

    private IEnumerator WaitJump(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool(isJumpingHash, false);
        canJump = true; // Habilitar el salto después del tiempo de espera
        isLanding = false;
       
    }



    void HandleRotation()
    {
        Vector3 positionLookAt;
        positionLookAt.x = currentMovement.x;
        positionLookAt.y = 0.0f;
        positionLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);

        }
       
    }


    void HandleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunnigHash);
        bool isSprinting = animator.GetBool(isSprintigHash);
        bool isCrouchMovement = animator.GetBool(isMoveCrouchHash);



        if (isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
            timeSprint = 0;
        }

        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
            timeSprint = 0;
        }



        if (isMovementPressed && isRunPressed && !isRunning)
        {
            animator.SetBool(isRunnigHash, true);

        }

        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunnigHash, false);
      

        }


        if (isMovementPressed && timeSprint >= 4 && !isSprinting)
        {
            timeSprint = 4;
            animator.SetBool(isSprintigHash, true);
            isSprint = true;
        }

        else if ((!isMovementPressed || !isRunPressed) && timeSprint <= 0 && isSprinting)
        {
            timeSprint = 0;
            animator.SetBool(isSprintigHash, false);
        }



        if (isRunPressed)
        {
            timeSprint += Time.deltaTime;
            
        }


        else
        {
            timeSprint = 0;
            isSprint = false;
        }

        if(isCrouchPressed)
        {
            animator.SetBool(isCrouchHash, true);
            Debug.Log("agachate");
        }

        else
        {
            animator.SetBool(isCrouchHash, false);
            Debug.Log("parate");
        }

        if(isCrouchPressed && isMovementPressed && !isCrouchMovement)
        {
            animator.SetBool(isMoveCrouchHash, true);
            
            Debug.Log("agachate y muevete");
            
        }

        else if(isCrouchPressed && !isMovementPressed)
        {
            animator.SetBool(isMoveCrouchHash, false);
            Debug.Log("dejalo");
        }

        if(isInteractPressed && checks.pushInteract)
        {
            animator.SetBool(isPushHash, true);
            playerInAction = true;

            if (isMovementPressed)
            {
                animator.SetBool(isPushMoveHash, true);
                pushObject = true;
               

            }

            else
            {
                animator.SetBool(isPushMoveHash, false);
                pushObject = false;
            }

        }

        else
        {
            animator.SetBool(isPushHash, false);
            animator.SetBool(isPushMoveHash, false);
            pushObject = false;
            playerInAction = false;
        }

        if(isInteractPressed && checks.objectInteract && areaInt.puedoTomarlo == true)
        {
            // Animacion de tomar
            areaInt.objetInter.tomo = true;
        }

       

    }

    void CheckForFalling()
    {
        /*
        // Dispara un rayo hacia abajo desde el personaje
        Ray ray = new Ray(transform.position, Vector3.down * maxRayDistance);
        RaycastHit hit;

        // Realiza el raycast
        if (Physics.Raycast(ray, out hit))
        {
            // Calcula la distancia desde el suelo

            float distanceToGround = hit.distance;
            //Debug.Log(distanceToGround);

            // Dibuja el rayo en el Editor de Unity
           // Debug.DrawRay(ray.origin, ray.direction * distanceToGround, Color.red);

            // Verifica si la distancia desde el suelo es mayor que el umbral mínimo
            if (distanceToGround > minFallHeightForAnimation && !characterController.isGrounded && !isFallingg)
            {
                // Activa la animación de caída
                isFallingg = true;
                Debug.Log(isFallingg);
                // animator.SetBool(isFallingHash, true);
            }
            else
            {
                // Desactiva la animación de caída
                isFallingg = false;
                animator.SetBool(isFallingHash, false);
            }

            /*if (isFalling && characterController.isGrounded)
            {
                animator.SetBool(isLandingHash, true);

            }*/




        // Verifica si el personaje está cayendo
        bool isFallingg = characterController.velocity.y < 0 && !characterController.isGrounded;

        // Si el personaje ha estado en el aire durante un tiempo prolongado, también activa isFallingg

        if (!characterController.isGrounded)
        {
            timeInAir += Time.deltaTime;
            if (timeInAir >= minTimeInAirForFall)
            {
                isFallingg = true;
                Debug.Log("Callo");
                isLanding = true;
                animator.SetBool(isFallingHash, true);
            }
        }
        else
        {
            // Si el personaje está en el suelo, reinicia el tiempo en el aire
            animator.SetBool(isFallingHash, false);
            timeInAir = 0f;
            Debug.Log("Suelo");
        }

        if(!isFallingg && characterController.isGrounded)
        {
          // animator.SetBool(isLandingHash, true);
          
          // animator.SetBool(isLandingHash, false);
            //StartCoroutine(DisableLandingAnimation(landingAnimationDuration));
        }

        if(isLanding && characterController.isGrounded)
        {
            animator.SetBool(isLandingHash, true);
            Debug.Log("animacion suelo");
        }

        else
        {
           // animator.SetBool(isLandingHash, false);
        }

        // Activa la animación de caída si es necesario
         
    }

    IEnumerator DisableLandingAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool(isLandingHash, false);
    }

    private void CheckGrounded()
    {
        RaycastHit hit;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, maxRayDistance, groundLayer);
       // Debug.Log(isGrounded);
        Debug.DrawRay(transform.position, Vector3.down * maxRayDistance, Color.red);

        if (!isGrounded && minFallHeightForAnimation > hit.distance)
        {
            isFallingg = true;
            isLanding = true;
            animator.SetBool(isFallingHash, true);
           // Debug.Log("Animacion Falling");
           
        }
        
        else
        {
            isFallingg = false;
            animator.SetBool(isFallingHash, false);
        }

        if (isLanding && isGrounded)
        {
            animator.SetBool(isLandingHash, true);
            
          
        }

        else
        {
             animator.SetBool(isLandingHash, false);
        }

        
    }


    public IEnumerator PerformAction(string AnimationName, CompareTargetParameter ctp = null, Quaternion RequiredRotation = new Quaternion(), bool LookAtObstacle = false, float ParkourActionDelay = 0f)
    {

        animator.CrossFadeInFixedTime(AnimationName, 0.2f);
        
        playerInAction = true;
        characterController.enabled = false;
        characterController.detectCollisions = false;

        animator.applyRootMotion = true;

        //movement.SetControl(false);

        yield return null;

        var animationState = animator.GetNextAnimatorStateInfo(0);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationName))
        {
            Debug.Log("Animation Name is Incorrect");
        }

        //yield return new WaitForSeconds(animationState.length);
        float rotateStartTime = (ctp != null) ? ctp.startTime : 0f;


        float timerCounter = 0f;

        while (timerCounter <= animationState.length)
        {
            timerCounter += Time.deltaTime;


            float normalizedTimerCounter = timerCounter / animationState.length;

            //Make player to look towards the obstacle
            if (LookAtObstacle && normalizedTimerCounter > rotateStartTime)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, RequiredRotation, rotationFactorPerFrame * Time.deltaTime);
            }

            if (ctp != null)
            {
                CompareTarget(ctp);
            }


            if (animator.IsInTransition(0) && timerCounter > 0.5f)
            {
                break;
            }


            yield return null;
        }

        yield return new WaitForSeconds(ParkourActionDelay);

        // movement.SetControl(true);

        playerInAction = false;
        characterController.enabled = true;
        characterController.detectCollisions = true;
        animator.applyRootMotion = false;


    }



    void CompareTarget(CompareTargetParameter compareTargetParameter)
    {
        animator.MatchTarget(compareTargetParameter.position, transform.rotation, compareTargetParameter.bodyPart, new MatchTargetWeightMask((compareTargetParameter.positionWeight), 0), compareTargetParameter.startTime, compareTargetParameter.endTime);
    }

   



    public void SetControl(bool hasControl)
    {
        this.playerControl = hasControl;
       // characterController.enabled = hasControl;
        
        if (!hasControl)
        {            
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isJumpingHash, false);
            animator.SetBool(isRunnigHash, false);
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isFallingHash, false);
            animator.SetBool(isLandingHash, false);
            animator.SetBool(isCrouchHash, false);

            requiredRotation = transform.rotation;
        }
    }

    public void EnableCC(bool enabled)
    {
        characterController.enabled = enabled;
    }

    public void ResetRequiredRotation()
    {
        requiredRotation = transform.rotation;
    }



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigg = hit.collider.attachedRigidbody;

        if (rigg != null )
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rigg.AddForceAtPosition(forceDirection * pushForce, transform.position, ForceMode.Impulse);
        }
    }



}

public class CompareTargetParameter
{
    public Vector3 position;
    public AvatarTarget bodyPart;
    public Vector3 positionWeight;
    public float startTime;
    public float endTime;

}

    /* void CheckForFallingAnimation()
     {
         // Calcula la altura de la caída
         float fallHeight = CalculateFallHeight();

         // Comprueba si la altura de la caída supera el umbral mínimo
         if (fallHeight > minFallHeightForAnimation && !characterController.isGrounded)
         {
             // Reproduce la animación de caída
             animator.SetBool(isFallingHash, true);

         }
         else
         {
             // Mantén la animación actual (probablemente una animación de idle)
             animator.SetBool(isFallingHash, false);
         }
     }

     // Calcula la altura de la caída
     float CalculateFallHeight()
     {
         // Calcula la diferencia entre la posición actual y la posición inicial del personaje en el eje Y
         return transform.position.y - initialPosition.y;
     }*/




