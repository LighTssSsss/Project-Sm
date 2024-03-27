using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentChecker : MonoBehaviour
{
    public Vector3 rayOffset = new Vector3(0, 0.2f, 0);
    public float rayLength = 0.09f;
    public LayerMask obstacleLayer;
    public float hegihtRaylength;
    public bool forwarObstacle;

    public ObstaclesInfo CheckObstacle()
    {


        var hitData = new ObstaclesInfo();


        var rayOrigin = transform.position + rayOffset;
        hitData.hitFound = Physics.Raycast(rayOrigin, transform.forward, out hitData.hitInfo, rayLength, obstacleLayer);

        Debug.DrawRay(rayOrigin, transform.forward * rayLength, (hitData.hitFound) ? Color.red : Color.green);

        if (hitData.hitFound)
        {
            var heightOrigin = hitData.hitInfo.point + Vector3.up * hegihtRaylength;
           hitData.heightHitFound = Physics.Raycast(heightOrigin, Vector3.down, out hitData.heighInfo, hegihtRaylength, obstacleLayer);

            Debug.DrawRay(heightOrigin,Vector3.down * hegihtRaylength, (hitData.heightHitFound) ? Color.blue : Color.green);
            forwarObstacle = true;

        }

        else
        {
            forwarObstacle = false;
        }

        

        return hitData;
    }

   

}

public struct ObstaclesInfo
{
    public bool hitFound;
    public bool heightHitFound;
    public RaycastHit hitInfo;

    public RaycastHit heighInfo;
}





