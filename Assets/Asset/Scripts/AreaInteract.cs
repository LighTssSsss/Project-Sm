using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaInteract : MonoBehaviour
{
   public bool puedoTomarlo;
   public bool puedTomarMedicina;
   public ObjectInteract objetInter;
   public bool loToma;
   private bool llame1;

    //public GameObject notas;
    public bool puedoTomarNota;

    private void Update()
    {
        
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
            llame1 = true;
        }

        if (other.CompareTag("PuertaPrimeraPesadilla") && llame1 == true)
        {
            
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
