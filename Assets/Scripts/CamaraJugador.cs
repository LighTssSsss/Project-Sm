using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraJugador : MonoBehaviour
{
    [SerializeField] private Transform objetoASeguir;
    [SerializeField] private float velocidadCamara = 120f;
    [SerializeField] private float sensibilidad = 150f;
    [SerializeField] private float maxDistancia = 5f;

    private float mouseX;
    private float mouseY;
    private float rotY = 0f;
    private float rotX = 0f;
    private float distance;
    private Vector2 tamanoPlanoCercano;
    public new Camera camera;

    public LayerMask capaColision;

    // Start is called before the first frame update
    void Start()
    {
      //  camera = GetComponent<Camera>();
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.z;
        CalcularNearPlaneSize();

    }

   

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        rotY += mouseX * sensibilidad * Time.deltaTime;
        rotX -= mouseY * sensibilidad * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -60f, 60);
        transform.rotation = Quaternion.Euler(rotX, rotY, 0f);
    }

    private void CalcularNearPlaneSize()
    {
        float alto = Mathf.Tan(camera.fieldOfView * Mathf.Deg2Rad / 2) * camera.nearClipPlane;
        float ancho = alto * camera.aspect;

        tamanoPlanoCercano = new Vector2(ancho, alto);
    }

    private Vector3[] GetCamaraCollisionPuntos(Vector3 direction)
    {
        Vector3 position = objetoASeguir.position;
        Vector3 center = position + direction * (camera.nearClipPlane + 0.1f);

        Vector3 derecha = transform.right * tamanoPlanoCercano.x;
        Vector3 arriba = transform.up * tamanoPlanoCercano.y;
        return new Vector3[]
        {
            center - derecha + arriba,
            center + derecha + arriba,
            center - derecha - arriba,
            center + derecha - arriba
        };
    }


    private void LateUpdate() // hace despues del update
    {
        if(objetoASeguir == null)
        {
            return;
        }


        Vector3 direccion = transform.forward * -distance;
        Vector3 nuevaPosicion = objetoASeguir.position + direccion;

        RaycastHit hit;
        distance = maxDistancia;
       Vector3[] puntos = GetCamaraCollisionPuntos(direccion);

        foreach(Vector3 punto in puntos)
        {
            if (Physics.Raycast(objetoASeguir.position, direccion, out hit, maxDistancia, capaColision))
            {
                distance = Mathf.Min ((hit.point - objetoASeguir.position).magnitude,distance);
            }
        }
      
       
        // transform.position = Vector3.MoveTowards(transform.position, objetoASeguir.position, velocidadCamara * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, nuevaPosicion, velocidadCamara * Time.deltaTime);


    }



    
}
