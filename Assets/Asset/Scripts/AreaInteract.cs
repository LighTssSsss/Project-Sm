using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaInteract : MonoBehaviour
{
   public bool puedoTomarlo;
   public bool puedTomarMedicina;
   public ObjectInteract objetInter;
   public bool loToma;
   private bool llave1;

    //public GameObject notas;
    public bool puedoTomarNota;
    private GameObject llave;

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.E) && llave1 == true && llave != null)
        {
            llave.GetComponent<Llave>().toma = true;
        }*/
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
            other.gameObject.SetActive(false);

           // other.GetComponent<Llave>().toma = true;
            
        }

        if (other.CompareTag("PuertaPrimeraPesadilla") && llave1 == true && Input.GetKeyDown(KeyCode.E))
        {
            other.gameObject.GetComponent<PuertaUno>().abre = true;
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

        
    }
  
}
