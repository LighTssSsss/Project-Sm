using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalMove : MonoBehaviour
{
    //[SerializeField] private MoveAndAnimatorController move;
    [SerializeField] private CharacterController controller;
    [SerializeField] private CheckerEnviroment check;
    [SerializeField] private float jumpGravity = 1;
    [SerializeField] private float fallingGravity = 3;
    [SerializeField] private float maxFallVelocity = -10; 

    public bool canJumps;
    public bool isGrounded;
    public Vector3 velocity;
    private float disableGroundDetection;
    
    // Start is called before the first frame update
    void Start()
    {
        canJumps = true;
    }

    // Update is called once per frame
    void Update()
    {

        float gravityScale = fallingGravity;

        if (controller.velocity.y >= 0) gravityScale = jumpGravity; 
        
        if (isGrounded == true && disableGroundDetection == 0 && check.obstacleCollision == false)
        {
            velocity.y = 0;
            canJumps = true;
            
        }

        else
        {
            velocity.y += Physics.gravity.y * gravityScale * Time.deltaTime;
            canJumps = false;

            disableGroundDetection -= Time.deltaTime;
            disableGroundDetection = Mathf.Max(0, disableGroundDetection);
        }
       
         velocity.y = Mathf.Max(velocity.y, maxFallVelocity);
    }




    public void Jump(float force)
    {
        if (!canJumps) return;
        
        velocity.y = force;
        canJumps = false;
        Debug.Log("Fuerza salto: " + force);
        disableGroundDetection = 0.2f;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
