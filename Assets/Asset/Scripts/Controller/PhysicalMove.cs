using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalMove : MonoBehaviour
{
    //[SerializeField] private MoveAndAnimatorController move;

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
        //move = GetComponent<MoveAndAnimatorController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Revisar el codigo

        float gravityScale = fallingGravity;

        //if (move.characterController.velocity.y >= 0) gravityScale = jumpGravity;
             
        Debug.Log(isGrounded);

        if (isGrounded == true && disableGroundDetection == 0)
        {
            velocity.y = 0;
            canJumps = true;
            
        }

        else
        {
            velocity.y += Physics.gravity.y * gravityScale * Time.deltaTime;
           

            disableGroundDetection -= Time.deltaTime;
            disableGroundDetection = Mathf.Max(0, disableGroundDetection);
        }

        

        //velocity.y = Mathf.Max(velocity.y, maxFallVelocity);
       // controller.Move(velocity * Time.deltaTime);
    }

  


    public void Jump(float force)
    {
        if (!canJumps) return;
        
        velocity.y = force;
        canJumps = false;

        //disableGroundDetection = 0.2f;
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
