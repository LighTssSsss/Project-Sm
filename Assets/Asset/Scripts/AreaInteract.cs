using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaInteract : MonoBehaviour
{
   public bool puedoTomarlo;
   public bool puedTomarMedicina;
   public ObjectInteract objetInter;
   public bool loToma;
   public bool llave1;

    //public GameObject notas;
    public bool puedoTomarNota;
    private PuertaUno puerta;
    private Llave llaves;
    private bool puertaUno;
    public GameObject textoPuerta;
    private bool noAparece;
    private void Start()
    {
       puerta = FindObjectOfType<PuertaUno>();
       llaves = FindObjectOfType<Llave>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && llave1 == true && puerta != null && puertaUno == true)
        {
            Debug.Log("La tomo");
            puerta.abre = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && llave1 == true && llaves != null && noAparece == false)
        {
            Debug.Log("Abrio");
            textoPuerta.SetActive(false);
            llaves.toma = true;
            noAparece = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            puedoTomarlo = true;
            objetInter = other.GetComponent<ObjectInteract>();
        }

        if (other.CompareTag("Medicine"))
        {
            puedTomarMedicina = true;
            //Debug.Log("Medicina");
        }

        if (other.CompareTag("Nota"))
        {
            puedoTomarNota = true;
        }

        if (other.CompareTag("LlaveP1"))
        {
            llave1 = true;
            //other.gameObject.SetActive(false);
            Debug.Log("Se Acerco");
           // other.GetComponent<Llave>().toma = true;
            
        }

        if (other.CompareTag("PuertaPrimeraPesadilla")  && llave1 == true )
        {
            puertaUno = true;
            //other.gameObject.GetComponent<PuertaUno>().abre = true;
            textoPuerta.SetActive(true);
            Debug.Log("Abre Puerta");
        }


    }

   

    /* private void OnTriggerStay(Collider other)
     {
         if (other.CompareTag("Object"))
         {
             puedoTomarlo = true;
             objetInter = other.GetComponent<ObjectInteract>();
         }
     }*/

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Object") && loToma == false)
        {
            puedoTomarlo = false;
            objetInter = null;
            
        }

        if (other.CompareTag("Medicine"))
        {
            puedTomarMedicina = false;
        }


        if (other.CompareTag("Nota"))
        {
            puedoTomarNota = false;
        }

        if (other.CompareTag("LlaveP1"))
        {
            llave1 = false;
            //other.gameObject.SetActive(false);

            // other.GetComponent<Llave>().toma = true;

        }

        if (other.CompareTag("PuertaPrimeraPesadilla") && llave1 == true)
        {
            puertaUno = false;
            textoPuerta.SetActive(false);
            //other.gameObject.GetComponent<PuertaUno>().abre = true;
            Debug.Log("Abre Puerta");
        }


    }
  
}
