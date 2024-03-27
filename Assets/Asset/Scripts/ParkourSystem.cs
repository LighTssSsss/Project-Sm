using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourSystem : MonoBehaviour
{

    Animator animator;
    CharacterController characterC;

    [SerializeField] private CheckerEnviroment check;
    [SerializeField] private MoveAndAnimatorController movement;

    bool esta;
    bool inAction;
    private void Awake()
    {
        check = GetComponent<CheckerEnviroment>();
        movement = GetComponent<MoveAndAnimatorController>();
        animator = GetComponent<Animator>();
        characterC = GetComponent<CharacterController>();
    }


    private void Update()
    {
        if (movement.isJumpPressed)
        {
            ObstacleInfo hitData = check.CheckObstacle();
            if (check.obstacleCollision == true)
            {

                StartCoroutine(Clibimg());

            }
          
        }
    }


  


    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject.tag == "ParedAlta")
        {
            esta = true;
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ParedAlta")
        {
            esta = false;
        }
    }

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
    }

}
