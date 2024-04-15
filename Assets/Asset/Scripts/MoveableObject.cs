using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    public float pushForce;
    private CheckerEnviroment check;
    public MoveAndAnimatorController move;

    private void Awake()
    {
        check = GetComponent<CheckerEnviroment>();
        move = GetComponent<MoveAndAnimatorController>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigg = hit.collider.attachedRigidbody;

        if(rigg != null && check.pushInteract == true && move.pushObject)
        {
            Vector3 forceDirection = hit.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();
            // rigg.AddForceAtPosition(forceDirection * pushForce, transform.position, ForceMode.Impulse);
            rigg.AddForce(forceDirection * pushForce, ForceMode.Impulse);


        }
    }
}
