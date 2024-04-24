using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.TextCore.Text;

public partial class MoveAndAnimatorController : MonoBehaviour
{
    public CharacterController characterController;
    public CollisionHead colisionHead;
    PlayerInputs playerInputs;
    public Animator animator;
    public float speed;

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

    public bool isFallingg;
    public bool isClimbing;
    bool isSprintTime;
    bool isSprint;
    bool isJump;
    bool isCrouch;
    bool push;
    bool interactPush;
    bool dropObject;
    bool isLanding;
    bool isMovementPressed;
    bool isRunPressed;
    bool isTrajectoryPressed;
    bool isActionPushin;

    public bool inParkour;
    public bool playerInAction { get; private set; }
    public bool isJumpAnimation = false;  
    public bool isLeavePressed;
    public bool isCrouchPressed;
    public bool isInteractPressed;
    public bool isDropPressed;
    public bool isReleasePressed;
    public bool playerControl = true;
    public bool isJumpPressed = false;
    public bool isJumping = false;
    public bool playerHaging { get; set; }

    public float pushForce;
    public float rotationFactorPerFrame;
    public float runMultiplier;
    public float multiplierSprint;
    public float maxRayDistance;
    public float timeSprint;


    Quaternion requiredRotation;

   
    //float initialJumpVelocity;
    
   
   
   // Vector3 initialPosition;

    


    [Header("Gravity Setting")] // Cambiar
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    [SerializeField] private float maxFallGravity = -10;


    [Header("Jump")]
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpCooldown;
     private float velocityG;
    //public float timeInAir;
    //public float minTimeInAirForFall;
    public float landingAnimationDuration;
    public bool canJump = true;

    [Header("Fall and Land")]
    public LayerMask groundLayer;
    public float minFallHeightForAnimation;



    //private EnviromentChecker checkers;
    [Header("References")]
    private CheckerEnviroment checks;
    private ClimbingSystem climb;
    private MoveableObject moveObject;
    public AreaInteract areaInt;
    private HealthSystem healthSyst;
    private PhysicalMove physicalM;

    [Header("Trajectory")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform releasePosition;
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private int lineSegmentCount = 20;
    private List<Vector3> linePoint = new List<Vector3>();

    [Header("Display Controls")]
    [SerializeField]
    [Range(10, 100)]
    private int linePoints = 25;
    [SerializeField]
    [Range(0.01f, 0.25f)]
    private float timeBetweenPoint = 0.1f;
    private float throwStrength = 10f;

    [Header("Variable references")]
    public bool pushObject;
    public Camera cam;
    public float forceLauch;

  

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
        healthSyst = GetComponent<HealthSystem>();
        physicalM = GetComponent<PhysicalMove>();


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

        SubscribeInput();

        cameraObject = Camera.main.transform;

        isFallingg = false;      
    }

    void Update()
    {
        // Ver lo del sato gravedad, arreglar hit distancia y por ultimo lo de la caja

        /*float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        */

        if (!playerControl)
        {
            return;
        }


        if (playerHaging)
        {
            return;
        }


        if (isRunPressed && checks.pushInteract == false && inParkour == false && moveObject.push == false && isCrouchPressed == false && colisionHead.obstaculoencima == false)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
            
        }

        else
        {
            characterController.Move(currentMovement * Time.deltaTime); 

            //physicalM.Move(new Vector3(x * speed, 0, z * speed) * Time.deltaTime );
        }

        if (isSprint)
        {
            
            currentRunMovement *= multiplierSprint;
        }

        
        if (!playerInAction)
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }
        
        

        Vector3 cameraForward = cameraObject.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        
        currentMovement = cameraForward * currentMovementInput.y + cameraObject.right * currentMovementInput.x;
        currentMovement.Normalize();
        


        currentRunMovement = currentMovement * runMultiplier;

      
        if(isTrajectoryPressed == true && areaInt.loToma == true)
        {
            DrawProjection();
           
        }

        else
        {
            lineRenderer.enabled = false;
           
        }

        Gravity();
        CheckGrounded();      
        HandleRotation();      
        HandleAnimation();
        HandleJump();





        Debug.Log(velocityG);
    }

   
    private void Gravity()
    {

        bool isFalling = currentMovement.y <= 0.0F;  //ORIGINAl
        
        if (characterController.isGrounded && velocityG < 0.0f)
        {
            //velocityG = -1.0f;
            isClimbing = false;
            if (isJumpAnimation)
            {
                animator.SetBool(isJumpingHash, false);
                // isJumpAnimation = false;

            }
        }

        else if (isFalling)
        {
            
              velocityG += gravity * gravityMultiplier * Time.deltaTime;
            

            
        }

        else
        {
            velocityG += gravity * gravityMultiplier * Time.deltaTime;
        }

        if(velocityG <= -10f)
        {
            velocityG = - 9.8f;
        }



        Vector3 gravityVector = new Vector3(0, velocityG, 0);

        characterController.Move(gravityVector * Time.deltaTime);

    }


    private void HandleJump()
    {
       
        if (!isJumping && isJumpPressed && canJump && checks.obstacleCollision == false && isClimbing == false && isActionPushin == false && isCrouchPressed == false && colisionHead.obstaculoencima == false)
        {
            
            isJumpAnimation = true;
            //physicalM.Jump(jumpPower);
            velocityG = jumpPower;        
            StartCoroutine(WaitJump(jumpCooldown));
            isJumping = true;
            canJump = false;

            

        }

        else if (isJumping && !isJumpPressed || isJumpPressed && !characterController.isGrounded)
        {
            isJumping = false; // El jugador ya no está en el aire
            animator.SetBool(isJumpingHash, true);
            //animator.SetBool(isJumpingHash, false);
            isJumpAnimation = false;
            //canJump = true;
            checks.obstacleCollision = false;
        }

        else if(isJumping == false && characterController.isGrounded)
        {
            animator.SetBool(isJumpingHash, false);
            //canJump = true;
            
        }

        else if (isJumping == false && characterController.isGrounded && !isMovementPressed && !isRunPressed) 
        {
            animator.SetBool(isJumpingHash, false);
           
            // physicalM.canJump = true;
        }




    }

 

    private IEnumerator WaitJump(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool(isJumpingHash, false);
        canJump = true; // Habilitar el salto después del tiempo de espera
       // physicalM.canJumps = true;
        isLanding = false;

    }



    void HandleRotation()
    {
       
        Vector3 positionLookAt;
        positionLookAt.x = currentMovement.x;
        positionLookAt.y = 0.0f;
        positionLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed && !inParkour && moveObject.push == false)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);

        }

    }

    // ver lo del hit.collider hacia el piso, bloquear movimiento en x o al presionar botones o mover la camara cuando muevo la caja y mejorar IA


    void HandleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunnigHash);
        bool isSprinting = animator.GetBool(isSprintigHash);
        bool isCrouchMovement = animator.GetBool(isMoveCrouchHash);



        if (isMovementPressed && !isWalking && isFallingg == false)
        {
            animator.SetBool(isWalkingHash, true);
            timeSprint = 0;
        }

        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
            timeSprint = 0;
        }



        if (isMovementPressed && isRunPressed && !isRunning && isFallingg == false)
        {
            animator.SetBool(isRunnigHash, true);

        }

        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunnigHash, false);


        }


        if (isMovementPressed && timeSprint >= 4 && !isSprinting && isFallingg == false)
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



        if (isRunPressed && isCrouchPressed == false && colisionHead.obstaculoencima == false)
        {
            timeSprint += Time.deltaTime;

        }


        else
        {
            timeSprint = 0;
            isSprint = false;
        }

        if (isCrouchPressed && isFallingg == false)
        {
            animator.SetBool(isCrouchHash, true);
            characterController.center = new Vector3(0, 0.58f, 0);
            characterController.radius = 0.1846104f;
            characterController.height = 1.043544f;

         
            //sphereHead.enabled = true;
        }

         else
        {
            if(colisionHead.obstaculoencima == false)
            {
                animator.SetBool(isCrouchHash, false);


                characterController.center = new Vector3(0, 0.84f, 0);
                characterController.radius = 0.1846104f;
                characterController.height = 1.61f;
                //sphereHead.enabled = false;
            }

            
        }

        if(!isMovementPressed && colisionHead.obstaculoencima == true)
        {
            animator.SetBool(isCrouchHash, true);
            animator.SetBool(isMoveCrouchHash, false);
        }

        if (isCrouchPressed && isMovementPressed && !isCrouchMovement || isMovementPressed && colisionHead.obstaculoencima == true)
        {
            animator.SetBool(isMoveCrouchHash, true);
        }

        else if (isCrouchPressed && !isMovementPressed)
        {
            animator.SetBool(isMoveCrouchHash, false);
            animator.SetBool(isCrouchHash, true);

        }

        else if(isMovementPressed && colisionHead.obstaculoencima == false && !isCrouchPressed)
        {
            animator.SetBool(isCrouchHash, false);
            animator.SetBool(isMoveCrouchHash, false);
            animator.SetBool(isWalkingHash, true);
        }

        else if (isMovementPressed && colisionHead.obstaculoencima == false && !isCrouchPressed && isRunPressed)
        {
            animator.SetBool(isCrouchHash, false);
            animator.SetBool(isMoveCrouchHash, false);
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isRunnigHash, false);
        }




        if (isInteractPressed && checks.pushInteract && areaInt.loToma == false)
        {
            animator.SetBool(isPushHash, true);
            //playerInAction = true;
            isActionPushin = true;

            if (isMovementPressed && areaInt.loToma == false)
            {
                animator.SetBool(isPushMoveHash, true);
                pushObject = true;
               // isActionPushin = true;

            }

            else
            {
                animator.SetBool(isPushMoveHash, false);
                pushObject = false;
               // isActionPushin = false;
            }

        }



        else
        {
            animator.SetBool(isPushHash, false);
            animator.SetBool(isPushMoveHash, false);
            pushObject = false;
            playerInAction = false;
            isActionPushin = false;
        }

        if (isInteractPressed && areaInt != null && checks.pushInteract == false && areaInt.puedoTomarlo == true  && areaInt.objetInter.loTiene == false)
        {
            // Animacion de tomar
            areaInt.objetInter.tomo = true;
            areaInt.loToma = true;
            moveObject.enabled = false;
           // Debug.Log("Lo tomo");
        }

        if(isDropPressed && areaInt.objetInter != null &&  areaInt.objetInter.loTiene == true )
        {
            areaInt.objetInter.tomo = false;
            areaInt.objetInter.losuelta = true;
            areaInt.loToma = false;
            areaInt.puedoTomarlo = false;
            moveObject.enabled = true;
          
        }

        if(isReleasePressed && areaInt.objetInter != null && areaInt.objetInter.loTiene == true)
        {
           
            ReleaseObject();
            isTrajectoryPressed = false;
            moveObject.enabled = true;
           

        }

        if (isInteractPressed && checks.pushInteract == false && areaInt.puedTomarMedicina == true && areaInt != null)
        {
            // Animacion de tomar
            healthSyst.recupera = true;
            areaInt.puedTomarMedicina = false;
           
        }



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
       
        Debug.DrawRay(transform.position, Vector3.down * maxRayDistance, Color.red);

        if (isGrounded && hit.distance > 1f)
        {
            Debug.Log(hit.distance);
            isLanding = true;
            isFallingg = true;
            Debug.Log("Callo");
              
            animator.SetBool(isFallingHash, true);
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isRunnigHash, false);
            animator.SetBool(isSprintigHash, false);
        }

        if(isGrounded && hit.distance < 1.5f)
        {
            // Si el personaje está en el suelo, reinicia el tiempo en el aire Antiguo
            animator.SetBool(isFallingHash, false);
            Debug.Log("Suelo");
        }

        if (isFallingg == true && characterController.isGrounded)
        {
             animator.SetBool(isLandingHash, true);

             animator.SetBool(isLandingHash, false);

             StartCoroutine(DisableLandingAnimation(landingAnimationDuration));
        }

        if (isLanding && characterController.isGrounded)
        {
            animator.SetBool(isLandingHash, true);
            Debug.Log("animacion suelo");
        }

        else
        {
             animator.SetBool(isLandingHash, false);
        }



        /*
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

        */


        /*
      if (!characterController.isGrounded)
      {
          timeInAir += Time.deltaTime;
          if (timeInAir >= minTimeInAirForFall)
          {
              //isFallingg = true;
              Debug.Log("Callo");
             // isLanding = true;
             // animator.SetBool(isFallingHash, true);
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
          //Debug.Log("animacion suelo");
      }

      else
      {
         // animator.SetBool(isLandingHash, false);
      }

      */

        // Activa la animación de caída si es necesario




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

            //Hace que el jugador mire directo al obsatculo donde hace parkour
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


    private void DrawProjection()
    {
        lineRenderer.enabled = true;
        lineRenderer.positionCount = Mathf.CeilToInt(linePoints / timeBetweenPoint) + 1;

        Vector3 startPosition = releasePosition.position;
        Vector3 startvelocity = throwStrength * cam.transform.forward; // añadir division object.mass
        int i = 0;
        lineRenderer.SetPosition(i, startPosition);

        for(float time = 0; time < linePoints; time += timeBetweenPoint)
        {
            i++;
            Vector3 point = startPosition + time * startvelocity;
            point.y = startPosition.y + startvelocity.y * time + (Physics.gravity.y / 2f * time * time);

            lineRenderer.SetPosition(i, point);

            Vector3 lastPosition = lineRenderer.GetPosition(i - 1);

            if(Physics.Raycast(lastPosition,(point - lastPosition).normalized, out RaycastHit hit, (point - lastPosition).magnitude, collisionMask))
            {
                lineRenderer.SetPosition(i, hit.point);
                lineRenderer.positionCount = i + 1;
                return;
            }
        }
    }

    
    private void ReleaseObject()
    {      
        areaInt.objetInter.losuelta = true;
        areaInt.objetInter.rigidObject.isKinematic = false;
        areaInt.objetInter.rigidObject.freezeRotation = false;
        areaInt.objetInter.objects.transform.SetParent(null);
        areaInt.objetInter.rigidObject.angularVelocity = Vector3.zero;
        areaInt.objetInter.rigidObject.AddForce(cam.transform.forward * throwStrength, ForceMode.Impulse);
        areaInt.objetInter.objectBroke.isRelease = true;
        areaInt.loToma = false;
        areaInt.puedoTomarlo = false;
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





