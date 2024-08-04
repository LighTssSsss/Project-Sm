using Cinemachine;
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
    public CharacterController controladorPersonaje;
    public CollisionHead cabezaColision;
    private PlayerInputs playerInputs;
    public Animator animator;
    public float velocidadCorrer;
    public PhysicalMove movimientoFisico;

    private int isWalkingHash;
    private int isRunnigHash;
    private int isSprintigHash;
    private int isJumpingHash;
    private int isFallingHash;
    private int isLandingHash;
    private int isCrouchHash;
    private int isMoveCrouchHash;
    private int isPushHash;
    private int isPushMoveHash;
    private int isDropHash;

    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private Vector3 currentRunMovement;
    private Transform cameraObject;

    public bool isFallingg;
    public bool isClimbing;
    private  bool isSprintTime;
    private bool isSprint;
    private bool isJump;
    private bool isCrouch;
    private bool push;
    private bool interactPush;
    private bool dropObject;
    private bool isLanding;
    private bool isMovementPressed;
    private bool isRunPressed;
    private bool isTrajectoryPressed;
    private bool isActionPushin;
   

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

    public bool empujando;
    public bool enPersecucion;
    public bool sprintLiberado;
    public float fuerzaEmpuje;
    public float factorRotacionPorFrame;
    public float multiplicadorDeCorrer;
    public float multiplicadorSprint;
    public float distanciaMaximaRayo;
    private float tiempoSprint;


    private Quaternion requiredRotation;
   

    [Header("Salto")]
    [SerializeField] private float poderSalto = 3.2f;
    [SerializeField] private float cooldownSalto = 1f;
    public float duracionLandingAnimacion = 1f;
    public bool puedoSaltar = true;
    private bool estaCorriendo;


    [Header("Caida y Parada")]
    public LayerMask capaSuelo;
    public float minimaAlturaCaidaParaAnimacion;



  
    [Header("Referencias")]
    private CheckerEnviroment checkeos;
    private MoveableObject movimientoObjeto;
    public AreaInteract areaInterracion;
    private VidaJugador vida;
    public EventoSonido eventoSo;

    [Header("Trayectoria de lanzamiento objeto")]
    [SerializeField] private LineRenderer linea;
    [SerializeField] private Transform posicionDeLanzamiento;
    [SerializeField] private LayerMask capaDeColision;
    [SerializeField] private int segmentoDeLineas = 20;
    private List<Vector3> lineaDePuntos = new List<Vector3>();

    [Header("Controles Mostrados")]
    [SerializeField]
    [Range(10, 100)]
    private int puntosLineas = 25;
    [SerializeField]
    [Range(0.01f, 0.25f)]
    private float tiempoEntrePuntos = 0.1f;
    private float fuerzaDeLanzamiento = 10f;

    [Header("Variable de referencias")]
    public bool empujandoObjetos;
    public Transform camara;
    public float fuerzaLanzamiento;


    [Header("Animacion Arbol")]
    public float valor;
    public float velocidad = 0.0f;
    public float aceleracion = 0.1f;
    public float desaceleracion = 0.5f;


    [Header("Notas")]
    public GameObject notas;
    public bool tomoLaNota;

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
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        playerInputs = new PlayerInputs();
        controladorPersonaje = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        checkeos = GetComponent<CheckerEnviroment>();     
        movimientoObjeto = GetComponent<MoveableObject>();
        vida = GetComponent<VidaJugador>();
        movimientoFisico = GetComponent<PhysicalMove>();
     

        isJumpingHash = Animator.StringToHash("isJumping");
        isCrouchHash = Animator.StringToHash("isCrounch");
        isMoveCrouchHash = Animator.StringToHash("isMoveCrouch");
        isPushHash = Animator.StringToHash("isPushing");
        isPushMoveHash = Animator.StringToHash("isPushWalk");
        isFallingHash = Animator.StringToHash("isFalling");
        isLandingHash = Animator.StringToHash("isLanding");

        SubscribeInput();

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

        if(tomoLaNota == true)
        {
            Time.timeScale = 0;
        }

       
       


        if (isRunPressed && checkeos.estaEmpujando == false && inParkour == false && movimientoObjeto.estoyEmpujandolo == false && isCrouchPressed == false && cabezaColision.obstaculoencima == false && isCrouchPressed == false)
        {
            movimientoFisico.velocidad = new Vector3(currentRunMovement.x, movimientoFisico.velocidad.y, currentRunMovement.z);
            estaCorriendo = true;
            controladorPersonaje.Move(movimientoFisico.velocidad * Time.deltaTime * velocidadCorrer);
        }

        else
        {
            movimientoFisico.velocidad = new Vector3(currentMovement.x, movimientoFisico.velocidad.y, currentMovement.z);
            estaCorriendo = false;

            controladorPersonaje.Move(movimientoFisico.velocidad * Time.deltaTime);
                        

        }

        if (isSprint && isCrouchPressed == false && tomoLaNota == false )
        {
            
            currentRunMovement *= multiplicadorSprint;
        }

        
        if (!playerInAction)
        {
            controladorPersonaje.Move(currentMovement * Time.deltaTime);
        }

        
        

        Vector3 cameraForward = cameraObject.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        //Cambiar el current movement en la direccion donde este viendo la caja

        if(empujando == false)
        {
            currentMovement = cameraForward * currentMovementInput.y + cameraObject.right * currentMovementInput.x;
            currentMovement.Normalize();
         
        }

        if (empujando)
        {
            currentMovement = transform.forward * currentMovementInput.y;

            currentMovement.Normalize();
           
        }
                                    
        currentRunMovement = currentMovement * multiplicadorSprint;
           
        if(isTrajectoryPressed == true && areaInterracion.loToma == true)
        {
            DibujarLinea();
           
        }

        else
        {
            linea.enabled = false;
           
        }

        CheckeoSuelo();
        ManejoRotacion();
        ManejoAnimaciones();
        ManejoDeSalto();

       

    }


    private void ManejoDeSalto()
    {

        if (!isJumping && isJumpPressed && movimientoFisico.puedoSaltar == true && checkeos.colisionConObstaculo == false && isClimbing == false && isActionPushin == false && isCrouchPressed == false && cabezaColision.obstaculoencima == false)
        {
            animator.SetBool(isJumpingHash, true);
            isJumpAnimation = true;
            puedoSaltar = false;    
            isJumping = true;
            StartCoroutine(EsperarSalto(1f));
            movimientoFisico.Jump(poderSalto);
            Debug.Log("Salto");
        }

       else if (isJumping && !isJumpPressed && movimientoFisico.estaEnSuelo)
        {
            animator.SetBool(isJumpingHash, false);
            isJumpAnimation = false;
            checkeos.colisionConObstaculo = false;
            Debug.Log("Aterrizó");
        }

        else if (isJumping == false && movimientoFisico.estaEnSuelo && isJumpPressed)
        {
            animator.SetBool(isJumpingHash, false);
            isJumpAnimation = false;
        }

        else if (isJumping == false && movimientoFisico.estaEnSuelo && !isMovementPressed && !isRunPressed)
        {
            animator.SetBool(isJumpingHash, false);
            isJumpAnimation = false;
        }


    }
 
    private IEnumerator EsperarSalto(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool(isJumpingHash, false);
        puedoSaltar = true;
        isJumping = false;
        movimientoFisico.puedoSaltar = true;
        isLanding = false;
    }

    private void ManejoRotacion()
    {
       
        Vector3 positionLookAt;
        positionLookAt.x = currentMovement.x;
        positionLookAt.y = 0.0f;
        positionLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed && !inParkour && movimientoObjeto.estoyEmpujandolo == false  && empujando == false)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, factorRotacionPorFrame * Time.deltaTime);
          

        }

       

    }


    void ManejoAnimaciones()
    {
        bool isCrouchMovement = animator.GetBool(isMoveCrouchHash);

        if (isMovementPressed  && isFallingg == false && isRunPressed == false )
        {   
            velocidad += Time.deltaTime * aceleracion;
            valor = Mathf.Lerp(valor, 0.59f, Time.deltaTime * aceleracion);
            velocidad = Mathf.Clamp(velocidad, 0, valor);
            animator.SetFloat("Velocidad", velocidad);


            tiempoSprint = 0;
        }

        else 
        {

            velocidad -= Time.deltaTime * desaceleracion;
            velocidad = Mathf.Clamp(velocidad, 0, 1f);
            animator.SetFloat("Velocidad", velocidad);

        }


        if (isMovementPressed && isRunPressed  && isFallingg == false && isCrouchPressed == false && enPersecucion == false)
        {

            velocidad += Time.deltaTime * aceleracion;
            valor = Mathf.Lerp(valor, 0.8f, Time.deltaTime * aceleracion);
            velocidad = Mathf.Clamp(velocidad, 0, valor);
            animator.SetFloat("Velocidad", velocidad);
            
           
        }

        else if ((isMovementPressed || !isRunPressed)  && velocidad > 1.0f && velocidad <= 0.1f)
        {
            velocidad -= Time.deltaTime * desaceleracion;
            valor = Mathf.Lerp(valor, 0.59f, Time.deltaTime * desaceleracion);
            velocidad = Mathf.Clamp(velocidad, 0, valor);
            animator.SetFloat("Velocidad", velocidad);
                     

        }

        else if ((!isMovementPressed || !isRunPressed) && velocidad > 1.0f)
        {       
            velocidad -= Time.deltaTime * desaceleracion;
            velocidad = Mathf.Clamp(velocidad, 0, 1f);
            animator.SetFloat("Velocidad", velocidad);
          

        }



        if (isMovementPressed  && isFallingg == false && isCrouchPressed == false && enPersecucion == true)
        {
            tiempoSprint = 2;
            valor = Mathf.Lerp(valor, 1.5f, Time.deltaTime * desaceleracion);
            velocidad = Mathf.Clamp(velocidad, 0, valor);
            animator.SetFloat("Velocidad", velocidad);

            isSprint = true;
        }

        else if ((!isMovementPressed || !isRunPressed) && tiempoSprint <= 0 && velocidad > 1.0f)
        {
            velocidad -= Time.deltaTime * desaceleracion;
            velocidad = Mathf.Clamp(velocidad, 0, 1f);
            animator.SetFloat("Velocidad", velocidad);
        }



        else
        {
           
            isSprint = false;
        }



        if (isCrouchPressed && isFallingg == false && isRunPressed == false && estaCorriendo == false && empujando == false)
        {
            animator.SetBool(isCrouchHash, true);
            controladorPersonaje.center = new Vector3(0, 0.58f, 0);
            controladorPersonaje.radius = 0.1846104f;
            controladorPersonaje.height = 1.043544f;
            
        }

         else
        {
            if(cabezaColision.obstaculoencima == false)
            {
                animator.SetBool(isCrouchHash, false);


                controladorPersonaje.center = new Vector3(0, 1.26f, -0.04f);
                controladorPersonaje.radius = 0.35f;
                controladorPersonaje.height = 2.18f;
            }

            
        }

        if(!isMovementPressed && cabezaColision.obstaculoencima == true)
        {
            animator.SetBool(isCrouchHash, true);
            animator.SetBool(isMoveCrouchHash, false);
        }

        if (isCrouchPressed && isMovementPressed && !isCrouchMovement || isMovementPressed && cabezaColision.obstaculoencima == true)
        {
            animator.SetBool(isMoveCrouchHash, true);
        }

        else if (isCrouchPressed && !isMovementPressed)
        {
            animator.SetBool(isMoveCrouchHash, false);
            animator.SetBool(isCrouchHash, true);

        }

        else if(isMovementPressed && cabezaColision.obstaculoencima == false && !isCrouchPressed && !isRunPressed)
        {
            animator.SetBool(isCrouchHash, false);
            animator.SetBool(isMoveCrouchHash, false);
            animator.SetFloat("Velocidad", velocidad);
        }

        else if (isMovementPressed && cabezaColision.obstaculoencima == false && !isCrouchPressed && isRunPressed)
        {
            animator.SetBool(isCrouchHash, false);
            animator.SetBool(isMoveCrouchHash, false);
            velocidad = 0.79f;
            animator.SetFloat("Velocidad", velocidad);
        }

        else if (isMovementPressed && cabezaColision.obstaculoencima == false && !isCrouchPressed)
        {
            animator.SetBool(isCrouchHash, false);
            animator.SetBool(isMoveCrouchHash, false);
            animator.SetFloat("Velocidad", velocidad);

        }


        if (isInteractPressed && checkeos.estaEmpujando && areaInterracion.loToma == false)
        {
            animator.SetBool(isPushHash, true);
            isActionPushin = true;

            if (isMovementPressed && areaInterracion.loToma == false)
            {
                animator.SetBool(isPushMoveHash, true);
                empujando = true;
              
                empujandoObjetos = true;

            }

            else
            {
                animator.SetBool(isPushMoveHash, false);
                empujandoObjetos = false;
               
            }

        }



        else
        {
            animator.SetBool(isPushHash, false);
            animator.SetBool(isPushMoveHash, false);
            empujandoObjetos = false;
            playerInAction = false;
            isActionPushin = false;
            movimientoObjeto.estoyEmpujandolo = false;
            empujando = false;
        }

        if (isInteractPressed && areaInterracion != null && checkeos.estaEmpujando == false && areaInterracion.puedoTomarlo == true  && areaInterracion.objetInter.loTiene == false)
        {
            areaInterracion.objetInter.tomo = true;
            areaInterracion.loToma = true;
            movimientoObjeto.enabled = false;
        }

        if(isDropPressed && areaInterracion.objetInter != null && areaInterracion.objetInter.loTiene == true )
        {
            areaInterracion.objetInter.tomo = false;
            areaInterracion.objetInter.losuelta = true;
            areaInterracion.loToma = false;
            areaInterracion.puedoTomarlo = false;
            movimientoObjeto.enabled = true;
          
        }

        if(isReleasePressed && areaInterracion.objetInter != null && areaInterracion.objetInter.loTiene == true)
        {

            LanzarObjeto();
            isTrajectoryPressed = false;
            movimientoObjeto.enabled = true;
           

        }

        if (isInteractPressed && checkeos.estaEmpujando == false && areaInterracion.puedTomarMedicina == true && areaInterracion != null && vida.vidas <= 95)
        {
            // Animacion de tomar
            vida.recupera = true;
            areaInterracion.puedTomarMedicina = false;
           
        }

        //Aqui Notas
        if(isInteractPressed && areaInterracion.puedoTomarNota == true && tomoLaNota == false)
        {
            tomoLaNota = true;
            notas.SetActive(true);
            eventoSo.SonidoPapelTomando();
           
        }


        if (isDropPressed && areaInterracion.puedoTomarNota == true && tomoLaNota == true)
        {
            tomoLaNota = false;
            notas.SetActive(false);
            Time.timeScale = 1;
            eventoSo.SonidoPapelTomando();
            velocidadCorrer = 2;
        }



    }

    

    

    IEnumerator DesactivarAnimacionCaida(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool(isLandingHash, false);
    }

    private void CheckeoSuelo()
    {
        RaycastHit hit;
         bool isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, distanciaMaximaRayo, capaSuelo);
       
        Debug.DrawRay(transform.position, Vector3.down * distanciaMaximaRayo, Color.red);

        if (isGrounded && hit.distance > 3.5f)
        {
            
            isLanding = true;
            isFallingg = true;
              
            animator.SetBool(isFallingHash, true);
            animator.SetBool(isJumpingHash, false);
        }

        if (isFallingg == true && movimientoFisico.estaEnSuelo == true && hit.distance <= 0.5f)
        {
            animator.SetBool(isFallingHash, false);
            animator.SetBool(isLandingHash, true);
            isFallingg = false;
            isLanding = false;

            StartCoroutine(DesactivarAnimacionCaida(duracionLandingAnimacion));
        }
    }


    public IEnumerator RealizaAccion (string AnimationName, CompareTargetParameter ctp = null, Quaternion RequiredRotation = new Quaternion(), bool LookAtObstacle = false, float ParkourActionDelay = 0f)
    {

        animator.CrossFadeInFixedTime(AnimationName, 0.2f);
        
        playerInAction = true;
        controladorPersonaje.enabled = false;
        controladorPersonaje.detectCollisions = false;

        animator.applyRootMotion = true;

        //movement.SetControl(false);

        yield return null;

        var animationState = animator.GetNextAnimatorStateInfo(0);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationName))
        {
            Debug.Log("Nombre de animacion incorrecta");
        }

      
        float rotateStartTime = (ctp != null) ? ctp.comienzo : 0f;


        float timerCounter = 0f;

        while (timerCounter <= animationState.length)
        {
            timerCounter += Time.deltaTime;


            float normalizedTimerCounter = timerCounter / animationState.length;

            //Hace que el jugador mire directo al obsatculo donde hace parkour
            if (LookAtObstacle && normalizedTimerCounter > rotateStartTime)
            {
               
                // esto esta afuera
                transform.rotation = Quaternion.RotateTowards(transform.rotation, RequiredRotation, factorRotacionPorFrame * Time.deltaTime);
            }

            if (ctp != null)
            {
                CompararObjetivo(ctp);
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
        controladorPersonaje.enabled = true;
        controladorPersonaje.detectCollisions = true;
        animator.applyRootMotion = false;

       
    }



    void CompararObjetivo(CompareTargetParameter compareTargetParameter)
    {
        animator.MatchTarget(compareTargetParameter.posicion, transform.rotation, compareTargetParameter.parteDelCuerpo, new MatchTargetWeightMask((compareTargetParameter.posicionAcho), 0), compareTargetParameter.comienzo, compareTargetParameter.final);
    }

   



    public void Control(bool hasControl)
    {
        this.playerControl = hasControl;
      
        
        if (!hasControl)
        {
            animator.SetFloat("Velocidad", 0f);
            animator.SetBool(isFallingHash, false);
            animator.SetBool(isLandingHash, false);
            animator.SetBool(isCrouchHash, false);

            requiredRotation = transform.rotation;
        }
    }

    public void EnableCC(bool enabled)
    {
        controladorPersonaje.enabled = enabled;
    }

    public void ResetarPosicionRequerida()
    {
        requiredRotation = transform.rotation;
    }


    private void DibujarLinea()
    {
        linea.enabled = true;
        linea.positionCount = Mathf.CeilToInt(puntosLineas / tiempoEntrePuntos) + 1;

        Vector3 startPosition = posicionDeLanzamiento.position;
        Vector3 startvelocity = fuerzaDeLanzamiento * camara.transform.forward; // añadir division object.mass
        int i = 0;
        linea.SetPosition(i, startPosition);

        for(float time = 0; time < puntosLineas; time += tiempoEntrePuntos)
        {
            i++;
            Vector3 point = startPosition + time * startvelocity;
            point.y = startPosition.y + startvelocity.y * time + (Physics.gravity.y / 2f * time * time);

            linea.SetPosition(i, point);

            Vector3 lastPosition = linea.GetPosition(i - 1);

            if(Physics.Raycast(lastPosition,(point - lastPosition).normalized, out RaycastHit hit, (point - lastPosition).magnitude, capaDeColision))
            {
                linea.SetPosition(i, hit.point);
                linea.positionCount = i + 1;
                return;
            }
        }
    }

    
    private void LanzarObjeto()
    {
        areaInterracion.objetInter.losuelta = true;
        areaInterracion.objetInter.rigidObject.isKinematic = false;
        areaInterracion.objetInter.rigidObject.freezeRotation = false;
        areaInterracion.objetInter.objects.transform.SetParent(null);
        areaInterracion.objetInter.rigidObject.angularVelocity = Vector3.zero;
        areaInterracion.objetInter.rigidObject.AddForce(camara.transform.forward * fuerzaDeLanzamiento, ForceMode.Impulse);
        areaInterracion.objetInter.objectBroke.isRelease = true;
        areaInterracion.loToma = false;
        areaInterracion.puedoTomarlo = false;
    }

}

public class CompareTargetParameter
{
    public Vector3 posicion;
    public AvatarTarget parteDelCuerpo;
    public Vector3 posicionAcho;
    public float comienzo;
    public float final;

}





