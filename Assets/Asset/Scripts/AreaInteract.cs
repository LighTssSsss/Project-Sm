using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaInteract : MonoBehaviour
{
   public bool puedoTomarlo;
   public ObjectInteract objetInter;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            puedoTomarlo = true;
            objetInter = other.GetComponent<ObjectInteract>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            puedoTomarlo = false;
            objetInter = null;
            
        }
    }
}
