using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class CheckerEnviroment : MonoBehaviour
{
    [Header("Checkear")]
    public Vector3 alturaRayoDeParkour = new Vector3(0, 0.2f, 0);
    public float distanciaRayoParkour = 0.9f;
    public float alturaRayoDistancia = 6f;
    public LayerMask capaColisionParkour;
    private float alturaDistanciaRayo;
    private float distanciaRayo = 0.6f;
    private float distanciaRayoX = 0.7f;

    [Header("checkear escalada y parkour")]
    [SerializeField] private float escaladaRayoDistancia = 1.6f;
    [SerializeField] private LayerMask escaladaCapa;
   // public int numberOfRays = 12;
    public bool notJumpAction;

    [Header("Toco o no el objeto")]
    public bool colisionConObstaculo;
    public bool estaEmpujando;
   // public bool interactuaConObjeto;
   // public bool ObjetoEnMano;

    private void Update()
    {
        Vector3 rayOrigin2 = transform.position + alturaRayoDeParkour;

        // Primer raycast para verificar obstáculos frente al jugador
        bool hitFound = Physics.Raycast(rayOrigin2, transform.forward, distanciaRayoParkour, capaColisionParkour);

        Debug.DrawRay(rayOrigin2, transform.forward * distanciaRayoParkour, (hitFound) ? Color.yellow : Color.green);

        colisionConObstaculo = hitFound;

        CheckInteract();      
    }

    public ObstacleInfo CheckObstacle()
    {
        ObstacleInfo hitData = new ObstacleInfo();

        Vector3 rayOrigin = transform.position + alturaRayoDeParkour;
        hitData.hitFound = Physics.Raycast(rayOrigin, transform.forward, out hitData.hitInfo, distanciaRayoParkour, capaColisionParkour);

        Debug.DrawRay(rayOrigin, transform.forward * distanciaRayoParkour, (hitData.hitFound) ? Color.red : Color.green);
        //obstacleCollision = true;

        if (hitData.hitFound)
        {
          
            Vector3 heighOrigin = hitData.hitInfo.point + Vector3.up * alturaRayoDistancia;
            hitData.heightHitFound = Physics.Raycast(heighOrigin, Vector3.down, out hitData.heightInfo, alturaRayoDistancia, capaColisionParkour);
            Debug.DrawRay(heighOrigin,Vector3.down * alturaRayoDistancia, (hitData.heightHitFound) ? Color.red : Color.green);

            if (hitData.heightHitFound)
            {
                distanciaRayo = Vector3.Distance(hitData.hitInfo.point, hitData.heightInfo.point);
               
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

       /* for(int i = 0; i < numberOfRays; i++)
        {
            Debug.DrawRay(climbOrigin + climbOffset * i, climbDirection,Color.blue);

           if( Physics.Raycast(climbOrigin + climbOffset * i, climbDirection, out RaycastHit hit, escaladaRayoDistancia, escaladaCapa))
            {
                climbInfo = hit;
                return true;
            }
        }*/

        return false;
    }


    public bool ChechkDropClimbPoint(out RaycastHit DropHit)
    {
        DropHit = new RaycastHit();

        var origin = transform.position + Vector3.down * 0.1f + transform.forward * 2f;

        if(Physics.Raycast(origin, -transform.forward, out RaycastHit hit, 3, escaladaCapa))
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

        if (Physics.Raycast(rayOrigin, transform.forward, out hit, distanciaRayo, LayerMask.GetMask("InteractPush")))
        {

            estaEmpujando = true;
           
        }
        else
        {

            estaEmpujando = false;
        }

        if (Physics.Raycast(rayOrigin, transform.forward, out hit, distanciaRayo, LayerMask.GetMask("Walls")))
        {
           
            notJumpAction = true;
          

        }

        else if (Physics.Raycast(rayOrigin, transform.right, out hit, distanciaRayoX, LayerMask.GetMask("Walls")))
        {

            notJumpAction = true;
           

        }

        else if (Physics.Raycast(rayOrigin, -transform.right, out hit, distanciaRayoX, LayerMask.GetMask("Walls")))
        {

            notJumpAction = true;
            

        }

        else
        {
            notJumpAction = false;
           
        }
    
        Debug.DrawRay(rayOrigin, transform.forward * distanciaRayo, (hit.collider != null) ? Color.yellow : Color.red);

        Debug.DrawRay(rayOrigin, transform.right * distanciaRayoX, (hit.collider != null) ? Color.yellow : Color.red);

        Debug.DrawRay(rayOrigin, -transform.right * distanciaRayoX, (hit.collider != null) ? Color.yellow : Color.red);
    }

}

public struct ObstacleInfo
{
    public bool hitFound;
    public bool heightHitFound;
    public RaycastHit hitInfo;
    public RaycastHit heightInfo;

    public Vector3 posicion;
}
