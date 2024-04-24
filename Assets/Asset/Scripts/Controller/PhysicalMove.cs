using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalMove : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [SerializeField] private float jumpingGravity = 1;
    [SerializeField] private float fallingGravity = 3;
    [SerializeField] private MoveAndAnimatorController move;
    public Vector3 velocity;
    private float disableGroundDetection;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<MoveAndAnimatorController>();
    }

    // Update is called once per frame
    void Update()
    {
        float gravityScale = fallingGravity;


        if (controller.velocity.y >= 0)
        {
            gravityScale = jumpingGravity;
        }

     


        if (controller.isGrounded)
        {
            velocity.y = 0;
        }

        else
        {
            velocity.y += Physics.gravity.y * gravityScale * Time.deltaTime;
            disableGroundDetection -= Time.deltaTime;
            disableGroundDetection = Mathf.Max(0, disableGroundDetection);
        }

       
        controller.Move(velocity * Time.deltaTime);
    }
}
