using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class CheckerEnviroment : MonoBehaviour
{
    [Header("Check")]
    public Vector3 rayOffset = new Vector3(0, 0.2f, 0);
    public float raylenght = 0.9f;
    public float heightRaylenght = 6f;
    public float jumpRayLength = 6f;
    public LayerMask obstacleLayer;
    public float heightRayDistance;
    public float rayLength = 5f;
   
    [Header("Climbing Check")]
    [SerializeField] private float climbingRayLength = 1.6f;
    [SerializeField] private LayerMask climbingLayer;
    public int numberOfRays = 12;

    [Header("Touch or not object")]
    public bool obstacleCollision;
    public bool pushInteract;
    public bool objectInteract;
    public bool objectInHand;
   // public AreaInteract interact;


    
    private void Update()
    {
        Vector3 rayOrigin2 = transform.position + rayOffset;

        // Primer raycast para verificar obstáculos frente al jugador
        bool hitFound = Physics.Raycast(rayOrigin2, transform.forward, raylenght, obstacleLayer);

        Debug.DrawRay(rayOrigin2, transform.forward * raylenght, (hitFound) ? Color.yellow : Color.green);

        obstacleCollision = hitFound;

        // Si el primer raycast golpea un objeto en la capa especificada, establecer obstacleCollision en true
        /*
        if (hitFound)
        {
            obstacleCollision = true;
        }
        else
        {
            obstacleCollision = false;
        }*/

        CheckInteract();
       

    }

    public ObstacleInfo CheckObstacle()
    {
        ObstacleInfo hitData = new ObstacleInfo();

        Vector3 rayOrigin = transform.position + rayOffset;
        hitData.hitFound = Physics.Raycast(rayOrigin, transform.forward, out hitData.hitInfo, raylenght, obstacleLayer);

        Debug.DrawRay(rayOrigin, transform.forward * raylenght, (hitData.hitFound) ? Color.red : Color.green);
        //obstacleCollision = true;

        if (hitData.hitFound)
        {
          
            Vector3 heighOrigin = hitData.hitInfo.point + Vector3.up * heightRaylenght;
            hitData.heightHitFound = Physics.Raycast(heighOrigin, Vector3.down, out hitData.heightInfo, heightRaylenght, obstacleLayer);
            Debug.DrawRay(heighOrigin,Vector3.down * heightRaylenght, (hitData.heightHitFound) ? Color.red : Color.green);

            if (hitData.heightHitFound)
            {
                heightRayDistance = Vector3.Distance(hitData.hitInfo.point, hitData.heightInfo.point);
                Debug.Log("Distancia del segundo rayo: " + heightRayDistance);
            }

        }

       /* else
        {
            obstacleCollision = false;
        }*/

        

        return hitData;

      
    }

    public bool CheckClimbing(Vector3 climbDirection,out RaycastHit climbInfo)
    {
        climbInfo = new RaycastHit();

        if(climbDirection == Vector3.zero)
        {
            return false;
        }

        var climbOrigin = transform.position + Vector3.up * 1.5f;
        var climbOffset = new Vector3(0, 0.19f, 0);

        for(int i = 0; i < numberOfRays; i++)
        {
            Debug.DrawRay(climbOrigin + climbOffset * i, climbDirection,Color.blue);

           if( Physics.Raycast(climbOrigin + climbOffset * i, climbDirection, out RaycastHit hit,  climbingRayLength, climbingLayer))
            {
                climbInfo = hit;
                return true;
            }
        }

        return false;
    }


    public bool ChechkDropClimbPoint(out RaycastHit DropHit)
    {
        DropHit = new RaycastHit();

        var origin = transform.position + Vector3.down * 0.1f + transform.forward * 2f;

        if(Physics.Raycast(origin, -transform.forward, out RaycastHit hit, 3, climbingLayer))
        {
            DropHit = hit;
            return true;
        }
        return false;
    }

    public void CheckInteract()
    {
        Vector3 rayOrigin = transform.position ;
        RaycastHit hit ;

        if (Physics.Raycast(rayOrigin, transform.forward, out hit, rayLength, LayerMask.GetMask("InteractPush")))
        {
            Debug.Log(" Se acerco, ahora lo puede empujar");
            pushInteract = true;
           
        }
        else
        {
            Debug.Log("Se alejo, ya no lo puede empujar");
            pushInteract = false;
        }

      /*  if(Physics.Raycast(rayOrigin, transform.forward, out hit, rayLength, LayerMask.GetMask("InteractObject")) && objectInHand == false)
        {
            objectInteract = true;
            interact = hit.collider.GetComponent<ObjectInteract>();
            Debug.Log(" Se acerco, ahora lo puede tomar");
        }

        else
        {
            interact = null;
            Debug.Log(" Se alejo, ya no lo puede tomar");
        }*/



            Debug.DrawRay(rayOrigin, transform.forward * rayLength, (hit.collider != null) ? Color.yellow : Color.red);
    }

}

public struct ObstacleInfo
{
    public bool hitFound;
    public bool heightHitFound;
    public RaycastHit hitInfo;
    public RaycastHit heightInfo;
}
