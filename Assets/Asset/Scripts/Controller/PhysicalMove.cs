using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalMove : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [SerializeField] private float jumpGravity = 1;
    [SerializeField] private float fallingGravity = 3;
    [SerializeField] private float maxFallVelocity = -10;
    public bool canJumps;
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

        if(controller.velocity.y >= 0)
        {
            gravityScale = jumpGravity;
        }

     
        if (controller.isGrounded && disableGroundDetection == 0)
        {
            velocity.y = 0;
            canJumps = true;
            //Debug.Log("Esta en el suelo");
        }

        else
        {
            velocity.y += Physics.gravity.y * gravityScale * Time.deltaTime;
            //Debug.Log("Esta en el aire");
            disableGroundDetection -= Time.deltaTime;
            disableGroundDetection = Mathf.Max(0, disableGroundDetection);
        }

       

        velocity.y = Mathf.Max(velocity.y, maxFallVelocity);


        controller.Move(velocity * Time.deltaTime);
    }

  


    public void Jump(float force)
    {
        if (!canJumps) return;
        
        velocity.y = force;
        canJumps = false;

        disableGroundDetection = 0.2f;
    }
}
