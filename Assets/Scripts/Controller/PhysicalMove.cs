using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalMove : MonoBehaviour
{
    
    [SerializeField] private CharacterController controlador;
    [SerializeField] private CheckerEnviroment checkeo;
    [SerializeField] private float gravedadSalto = 1;
    [SerializeField] private float gravedaCaida = 3;
    [SerializeField] private float velocidadMaximaCaida = -10; 

    public bool puedoSaltar;
    public bool estaEnSuelo;
    public Vector3 velocidad;
    private float desactivarDeteccionSuelo;
    
    // Start is called before the first frame update
    void Start()
    {
        puedoSaltar = true;
    }

    // Update is called once per frame
    void Update()
    {

        float gravityScale = gravedaCaida;

        if (controlador.velocity.y >= 0) gravityScale = gravedadSalto; 
        
        if (estaEnSuelo == true && desactivarDeteccionSuelo == 0 && checkeo.colisionConObstaculo == false)
        {
            velocidad.y = 0;
            puedoSaltar = true;
           
            // Debug.Log("Puede");
        }

        else
        {
            velocidad.y += Physics.gravity.y * gravityScale * Time.deltaTime;
            puedoSaltar = false;

            desactivarDeteccionSuelo -= Time.deltaTime;
            desactivarDeteccionSuelo = Mathf.Max(0, desactivarDeteccionSuelo);
        }

        velocidad.y = Mathf.Max(velocidad.y, velocidadMaximaCaida);
    }




    public void Jump(float force)
    {
        
        if (puedoSaltar == false) return;

        velocidad.y = force;
        puedoSaltar = false;

        desactivarDeteccionSuelo = 0.2f;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            estaEnSuelo = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            estaEnSuelo = false;
        }
    }
}
