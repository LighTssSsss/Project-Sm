using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Guardador : MonoBehaviour
{

    [SerializeField] private float forwardOffset = 1;
    [SerializeField] private float verticalOffset = 0;
    [SerializeField] private Vector3 size = Vector3.one;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private bool guarda;
    // public HealthPlayer hp;

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
           DataManager.data.posicion = this.transform.position;

           //DataManager.data.cinematicaTermino = true;
          // DataManager.data.cinematicaSaltada = true; // Bool que salta la cinematica
           DataManager.data.posicionPlayer = true;
            //DataManager.data.guardaMusica = true;
           // DataManager.data.vidaActual = hp.vida;
           DataManager.Save();

           //Debug.Log("Guarda");

        }
    }
    */



    private void Update()
    {
        Guardado();
    }

    /* private void OnTriggerEnter(Collider collision)
     {
         if (collision.CompareTag("Player"))
         {

             DataManager.data.posicion = this.transform.position;

             //DataManager.data.cinematicaTermino = true;
             // DataManager.data.cinematicaSaltada = true; // Bool que salta la cinematica
             DataManager.data.posicionPlayer = true;
             //DataManager.data.guardaMusica = true;
             // DataManager.data.vidaActual = hp.vida;
             DataManager.Save();

             //Debug.Log("Guarda");

         }
     }*/

    

    private void Guardado()
    {
        Vector3 center = transform.position + (transform.forward * forwardOffset);
        center.y += verticalOffset;

        Collider[] colliders = Physics.OverlapBox(center, size, transform.rotation, layerMask);

        foreach (Collider player in colliders)
        {            
           
            DataManager.data.posicion = this.transform.position;
            Debug.Log("Guarda");

            
        }

            

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 center = transform.position + (transform.forward * forwardOffset);
        center.y += verticalOffset;

        Gizmos.DrawWireCube(center, size);
    }

    IEnumerator Guardados()
    {
        guarda = false;
        
        Debug.Log("guarda");
        yield return new WaitForSeconds(5);
        guarda = true;
    }
}
