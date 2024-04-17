using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public partial class MoveAndAnimatorController : MonoBehaviour
{
    public CharacterController characterController;
    public Collider sphereHead;
    public CollisionHead colisionHead;
    PlayerInputs playerInputs;
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

   
    float initialJumpVelocity;
    
   
   
    Vector3 initialPosition;

    


    [Header("Gravity Setting")] // Cambiar
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;


    

    [Header("Jump")]
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpCooldown;
    private float velocityG;
    public float timeInAir;
    public float minTimeInAirForFall;
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


    [Header("Trajectory")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform releasePosition;
    [SerializeField] private LayerMask collisionMask;


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
        healthSyst = GetComponent<HealthSystem>();
        


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
        sphereHead.enabled = false;

        /*
        int objectLayer = areaInt.objetInter.objects.layer;

        for (int i = 0; i < 32; i++)
        {
            if (!Physics.GetIgnoreLayerCollision(objectLayer, i))
            {
                collisionMask |= 1 << i;
            }
        }*/

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

        if(isTrajectoryPressed == true && areaInt.loToma == true)
        {
            DrawProjection();
            Debug.Log("Aparece");
        }

        else
        {
            lineRenderer.enabled = false;
        }





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
            isClimbing = false;
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
        if (!isJumping && characterController.isGrounded && isJumpPressed && canJump && checks.obstacleCollision == false && isClimbing == false && isActionPushin == false)
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

        if (isCrouchPressed)
        {
            animator.SetBool(isCrouchHash, true);
            characterController.center = new Vector3(0, 0.58f, 0);
            characterController.radius = 0.1846104f;
            characterController.height = 1.043544f;

            Debug.Log("agachate");
            sphereHead.enabled = true;
        }

         else
        {
            if(colisionHead.obstaculoencima == false)

            animator.SetBool(isCrouchHash, false);
            Debug.Log("parate");
            
            characterController.center = new Vector3(0, 0.84f, 0);
            characterController.radius = 0.1846104f;
            characterController.height = 1.61f;
            sphereHead.enabled = false;
        }

        if (isCrouchPressed && isMovementPressed && !isCrouchMovement || isMovementPressed && colisionHead.obstaculoencima == true)
        {
            animator.SetBool(isMoveCrouchHash, true);

            Debug.Log("agachate y muevete");

        }

        else if (isCrouchPressed && !isMovementPressed)
        {
            animator.SetBool(isMoveCrouchHash, false);
            Debug.Log("dejalo");
        }


        if (isInteractPressed && checks.pushInteract)
        {
            animator.SetBool(isPushHash, true);
            //playerInAction = true;
            isActionPushin = true;

            if (isMovementPressed)
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
            Debug.Log("Lo tomo");
        }

        if(isDropPressed && areaInt.objetInter != null &&  areaInt.objetInter.loTiene == true )
        {
            areaInt.objetInter.tomo = false;
            areaInt.objetInter.losuelta = true;
            areaInt.loToma = false;
           // areaInt.objetInter = null;
            Debug.Log("Solto");
        }

        if(isReleasePressed && areaInt.objetInter != null && areaInt.objetInter.loTiene == true )
        {
            /*areaInt.objetInter.tomo = false;
            areaInt.objetInter.losuelta = true;
            Debug.Log("Lanzo");
            areaInt.objetInter.objects.transform.SetParent(null);
            areaInt.objetInter.GetComponent<Rigidbody>().isKinematic = false;
            areaInt.objetInter.GetComponent<Rigidbody>().AddForce(cam.transform.forward, ForceMode.Impulse);*/

            ReleaseObject();
            Debug.Log("Presiono");

        }

        if (isInteractPressed && checks.pushInteract == false && areaInt.puedTomarMedicina == true && areaInt != null)
        {
            // Animacion de tomar
            healthSyst.recupera = true;
            areaInt.puedTomarMedicina = false;
            Debug.Log("Tomo Medicina");
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
      //  bool isFallingg = characterController.velocity.y < 0 && !characterController.isGrounded;

        // Si el personaje ha estado en el aire durante un tiempo prolongado, también activa isFallingg

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
        /*areaInt.objetInter.tomo = false;
           areaInt.objetInter.losuelta = true;
           Debug.Log("Lanzo");
           areaInt.objetInter.objects.transform.SetParent(null);
           areaInt.objetInter.GetComponent<Rigidbody>().isKinematic = false;
           areaInt.objetInter.GetComponent<Rigidbody>().AddForce(cam.transform.forward, ForceMode.Impulse);*/
        /*
        Grenade.velocity = Vector3.zero;
        Grenade.angularVelocity = Vector3.zero;
        Grenade.isKinematic = false;
        Grenade.freezeRotation = false;
        Grenade.transform.SetParent(null, true);
        Grenade.AddForce(Camera.transform.forward * ThrowStrength, ForceMode.Impulse);*/

        Debug.Log("Lanza el objeto");
        areaInt.objetInter.losuelta = true;
        areaInt.objetInter.rigidObject.isKinematic = false;
        areaInt.objetInter.rigidObject.freezeRotation = false;
        areaInt.objetInter.objects.transform.SetParent(null);
        areaInt.objetInter.rigidObject.angularVelocity = Vector3.zero;
        areaInt.objetInter.rigidObject.AddForce(cam.transform.forward * throwStrength, ForceMode.Impulse);
        areaInt.objetInter.objectBroke.isRelease = true;
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




