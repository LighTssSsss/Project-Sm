using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldCode : MonoBehaviour
{
   


    private void Gravity()
    {

        //bool isFalling = currentMovement.y <= 0.0F;  //ORIGINAl

        //if (characterController.isGrounded && velocityG <= 0.0f)
        //{
        //    velocityG = 0f;
        //    isClimbing = false;

        //    if (isJumpAnimation)
        //    {
        //        animator.SetBool(isJumpingHash, false);
        //        // isJumpAnimation = false;

        //    }
        //}

        //else if (isFalling)
        //{

        //      velocityG += gravity * gravityMultiplier * Time.deltaTime;



        //}



        //if(velocityG <= -10f)
        //{
        //    velocityG = - 9.8f;
        //}



        //Vector3 gravityVector = new Vector3(0, velocityG, 0);

        //characterController.Move(gravityVector * Time.deltaTime);

    }

    /*private void CheckGrounded()
    {
        RaycastHit hit;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, maxRayDistance, groundLayer);

        Debug.DrawRay(transform.position, Vector3.down * maxRayDistance, Color.red);
        Debug.Log(hit.distance);

        if (isGrounded && hit.distance > 3.5f)
        {
            //Debug.Log(hit.distance);
            isLanding = true;
            isFallingg = true;
            Debug.Log("Esta cayendo");

            animator.SetBool(isFallingHash, true);
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isRunnigHash, false);
            animator.SetBool(isSprintigHash, false);
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isRunnigHash, false);
            animator.SetBool(isJumpingHash, false);
        }

        /* if(isGrounded && hit.distance < 1.5f)
         {
             // Si el personaje está en el suelo, reinicia el tiempo en el aire Antiguo
             //animator.SetBool(isFallingHash, false);
             Debug.Log("Toco el suelo");
         }
         */

    //if (isFallingg == true && physicalM.isGrounded == true && hit.distance <= 0.5f)
    //{
    //    animator.SetBool(isFallingHash, false);
    //    animator.SetBool(isLandingHash, true);
    //    isFallingg = false;
    //    isLanding = false;
    //    // animator.SetBool(isLandingHash, false);

    //    StartCoroutine(DisableLandingAnimation(landingAnimationDuration));
    //}

    //else if (isJumping && !isJumpPressed || isJumpPressed && !physicalM.isGrounded /*!characterController.isGrounded */)
    //    {
    //        isJumping = false; // El jugador ya no está en el aire
    //        animator.SetBool(isJumpingHash, true);
    //        //animator.SetBool(isJumpingHash, false);
    //        isJumpAnimation = false;
    //        //canJump = true;
    //        checks.obstacleCollision = false;
    //    }

    /*
            if (isLanding && physicalM.isGrounded == true)
            {
                animator.SetBool(isLandingHash, true);
                Debug.Log("animacion suelo");
            }

            else
            {
                 animator.SetBool(isLandingHash, false);
            }

            */


    //Aqui termina


    /*
       for(int i = 0; i < collider.Length; i++)
       {
           Debug.Log(" Se detecto el choque con Player");
           estaAtacando = true;

           if(estaAtacando == true)
           {

           }


           StartCoroutine(NotDamage());

       }*/



    /*  if (canDamage == true && player != null)
           {
               player.GetComponent<HealthSystem>().SetDamageHealth(damage);
                StartCoroutine(Damage());
               //canDamage = false;




     private void OnControllerColliderHit(ControllerColliderHit hit)
    {
       /* if (hit.collider.CompareTag("Player") && hit != null)
        {
            Debug.Log("Daño");
            HealthSystem health = hit.collider.GetComponent<HealthSystem>();
            if(health != null)
            {
                health.GetComponent<HealthSystem>().SetDamageHealth(damage);
            }
        }*/
}

           





