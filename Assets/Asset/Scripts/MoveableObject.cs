using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    public float pushForce;
    private CheckerEnviroment check;
    public MoveAndAnimatorController move;
    [SerializeField] private AreaInteract areint;
    [SerializeField] Vector3 pushdir;
    [SerializeField] private CharacterController ch;

    private void Awake()
    {
        check = GetComponent<CheckerEnviroment>();
        move = GetComponent<MoveAndAnimatorController>();
        ch = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

      /*  if(body != null && check.pushInteract == true && move.pushObject)
        {
            Vector3 forceDirection = hit.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();
            // rigg.AddForceAtPosition(forceDirection * pushForce, transform.position, ForceMode.Impulse);
            rigg.AddForce(forceDirection * pushForce, ForceMode.Impulse);


        }*/

        if(body == null || body.isKinematic)
        {
            return;
        }

        if(hit.moveDirection.y < -0.3f)
        {
            return;
        }
        
        
        if (check.pushInteract == true && move.pushObject && areint.loToma == false)
        {
           
            pushdir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            //body.velocity = pushDir * pushPower;
            Vector3 collisionPoint = hit.point;
            
            body.AddForceAtPosition(pushdir * pushForce, collisionPoint, ForceMode.Impulse);
        }

        

        

       
    }
}
