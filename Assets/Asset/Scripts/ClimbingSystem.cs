using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.InputSystem;

public class ClimbingSystem : MonoBehaviour
{
    // public EnviromentChecker enviromentCheckers;
    public CheckerEnviroment check;
    //bool playerInAction;
    public Animator animator;
   


    [Header("Parkour Action Area")]
    public List<NewParkour> newParkours;


    private MoveAndAnimatorController movement;
    private CharacterController character;

    
    

    private void Awake()
    {
        movement = GetComponent<MoveAndAnimatorController>();
        character = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {

        /* if (!movement.isJumping && movement.characterController.isGrounded && movement.isJumpPressed && movement.canJump && checker.forwarObstacle == true)
         {
             var hitData = enviromentCheckers.CheckObstacle();

             if (hitData.hitFound)
             {
                 foreach(var action in newParkours)
                 {
                     if (action.CheckIfAvaible(hitData, transform))
                     {
                         StartCoroutine(PerformParkourAction(action));
                         break;
                     }
                 }
                // Debug.Log("Object founded" + hitData.hitInfo.transform.name);
             }
         }*/

        /* var hitData = check.CheckObstacle();

         if (hitData.hitFound)
         {
             Debug.Log("Objeto:" + hitData.hitInfo.transform.name);
         }*/



      
        
        if (movement.isJumpPressed && !movement.playerInAction && check.obstacleCollision == true && !movement.playerHaging && !movement.inParkour && check.notJumpAction == false)
        {

            //Solo hacia adelante
            //transform.rotation = Quaternion.LookRotation(Vector3.forward);
                             
            ObstacleInfo hitData = check.CheckObstacle();

       
            foreach (NewParkour action in newParkours)
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

        //Quaternion forwardRotation = Quaternion.LookRotation(Vector3.forward);
        movement.inParkour = true;
        character.enabled = false;
        character.detectCollisions = false;
        animator.applyRootMotion = true;


        CompareTargetParameter compareTargetParameter = null;

        if (action.AlloTargetMatching)
        {
            compareTargetParameter = new CompareTargetParameter()
            {
                position = action.comparePosition,
                bodyPart = action.CompareBodyPart,
                positionWeight = action.ComparePositionWeight,
                startTime = action.CompareStartTime,
                endTime = action.CompareEndTime,
            };
        }

        yield return movement.PerformAction(action.AnimationName, compareTargetParameter, /*forwardRotation,*/ action.requiredRotation, action.LookAtObstacle, action.ParkourActionDelay);

        character.enabled = true;
        movement.inParkour = false;
        character.detectCollisions = true;
        animator.applyRootMotion = false;


        
        
       
    }



    void CompareTarget(NewParkour action)
    {
        animator.MatchTarget(action.comparePosition, transform.rotation, action.CompareBodyPart, new MatchTargetWeightMask((action.ComparePositionWeight), 0), action.CompareStartTime, action.CompareEndTime);
    }



    /* IEnumerator PerformParkourAction(NewParkour action)
     {

         animator.CrossFade(action.AnimationName, 0.2f);

         //playerInAction = true;
         character.enabled = false;
         character.detectCollisions = false;
         animator.applyRootMotion = true;

                     //movement.SetControl(false);

         yield return null;

         var animationState = animator.GetNextAnimatorStateInfo(0);

         if (!animator.GetCurrentAnimatorStateInfo(0).IsName(action.AnimationName))
         {
             Debug.Log("Animation Name is Incorrect");
         }

         //yield return new WaitForSeconds(animationState.length);

         float timerCounter = 0f;

         while (timerCounter <= animationState.length)
         {
             timerCounter += Time.deltaTime;

             //Make player to look towards the obstacle
             if (action.LookAtObstacle)
             {
                 transform.rotation =  Quaternion.RotateTowards(transform.rotation, action.requiredRotation, movement.rotationFactorPerFrame * Time.deltaTime);
             }

             if (action.AlloTargetMatching)
             {
                 CompareTarget(action);
             }


             if(animator.IsInTransition(0) && timerCounter > 0.5f)
             {
                 break;
             }


             yield return null;
         }

         yield return new WaitForSeconds(action.ParkourActionDelay);

        // movement.SetControl(true);
         playerInAction = false;
         character.enabled = true;
         character.detectCollisions = true;
         animator.applyRootMotion = false;


     }*/




    /*
    IEnumerator Clibimg()
    {

        animator.CrossFade("Running Jump", 0.2f);
        yield return null;


        inAction = true;
        animator.applyRootMotion = true;
        characterC.enabled = false;
        yield return new WaitForSeconds(animator.GetNextAnimatorStateInfo(0).length);

        inAction = false;
        animator.applyRootMotion = false;
        characterC.enabled = true;
    }*/

}
