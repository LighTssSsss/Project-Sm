using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraColision : MonoBehaviour
{
    [SerializeField] private float minDistancia = 1f;
    [SerializeField] private float maxDistancia = 5f;
    [SerializeField] private float suavidad = 10f;
 

    private float distancia;
    private Vector3 direccion;
    private Vector3 posicionOriginal;

    private void Start()
    {
        direccion = transform.localPosition.normalized;
        distancia = transform.localPosition.magnitude;
        posicionOriginal = transform.localPosition;
    }

    private void Update()
    {
        Vector3 startPos = transform.parent.position;
        Vector3 direction = direccion * maxDistancia;
        Vector3 posDeCamara = startPos + direction;

       
        Debug.DrawLine(startPos, posDeCamara, Color.red);

        
        RaycastHit hit;
        if (Physics.Linecast(startPos, posDeCamara, out hit))
        {
            //distancia = Mathf.Clamp(hit.distance * 0.85f, minDistancia, maxDistancia);
           
          
            Debug.Log("Hit: " + hit.collider.name);
        }
        else
        {
            distancia = maxDistancia;
        }

        Vector3 nuevaPosicion = posicionOriginal + direccion * distancia;
        transform.localPosition = Vector3.Lerp(transform.localPosition, nuevaPosicion, suavidad * Time.deltaTime);

        // Verifica el rayo manualmente si es necesario
        DebugRay();
    }

    void DebugRay()
    {
        Vector3 startPos = transform.parent.position;
        Vector3 direction = direccion * maxDistancia;

        Debug.DrawRay(startPos, direction, Color.yellow); // Dibuja el rayo en amarillo
    }

}
