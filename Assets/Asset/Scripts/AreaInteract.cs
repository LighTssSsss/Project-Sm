using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaInteract : MonoBehaviour
{
   public bool puedoTomarlo;
   public bool puedTomarMedicina;
   public ObjectInteract objetInter;
   public bool loToma;



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
            Debug.Log("Medicina");
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
    }
  
}
