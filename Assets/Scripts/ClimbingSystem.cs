using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class ClimbingSystem : MonoBehaviour
{
    public CheckerEnviroment checkeo;  
    public Animator animator;
    public EventoSonido eventoS;



    [Header("Acciones de Parkour")]
    public List<NewParkour> Parkours;


    private MoveAndAnimatorController movimiento;
    private CharacterController controlador;

    
    

    private void Awake()
    {
        movimiento = GetComponent<MoveAndAnimatorController>();
        controlador = GetComponent<CharacterController>();
        eventoS = FindObjectOfType<EventoSonido>();
    }

  
    private void Update()
    {
                
        if (movimiento.isJumpPressed && !movimiento.playerInAction && checkeo.colisionConObstaculo == true && !movimiento.playerHaging && !movimiento.inParkour && checkeo.notJumpAction == false && movimiento.isJumping == false)
        {

            //Solo hacia adelante

           // transform.rotation = Quaternion.LookRotation(Vector3.forward);
                             
            ObstacleInfo hitData = checkeo.CheckObstacle();

       
            foreach (NewParkour action in Parkours)
                {
               
                   if (action.CheckIfAvaible(hitData, transform))
                   {
                    
                    StartCoroutine(PerformParkourAction(action));
                    break;

                   }
                }

              
         }

       
      
    }

   

    IEnumerator PerformParkourAction(NewParkour action)
    {

      //  Quaternion forwardRotation = Quaternion.LookRotation(Vector3.forward);
        movimiento.inParkour = true;
        controlador.enabled = false;
        controlador.detectCollisions = false;
        animator.applyRootMotion = true;
        eventoS.SonidoSaltoSorteo();

        CompareTargetParameter compareTargetParameter = null;

        if (action.AlloTargetMatching)
        {
            compareTargetParameter = new CompareTargetParameter()
            {
                posicion = action.comparePosition,
                parteDelCuerpo = action.CompareBodyPart,
                posicionAcho = action.ComparePositionWeight,
                comienzo = action.CompareStartTime,
                final = action.CompareEndTime,
            };
        }

        yield return movimiento.RealizaAccion(action.AnimationName, compareTargetParameter, /*forwardRotation,*/ action.requiredRotation, action.LookAtObstacle, action.ParkourActionDelay);

        controlador.enabled = true;
        movimiento.inParkour = false;
        controlador.detectCollisions = true;
        animator.applyRootMotion = false;
                      
    }



    void CompareTarget(NewParkour action)
    {
        animator.MatchTarget(action.comparePosition, transform.rotation, action.CompareBodyPart, new MatchTargetWeightMask((action.ComparePositionWeight), 0), action.CompareStartTime, action.CompareEndTime);
    }



  


}
