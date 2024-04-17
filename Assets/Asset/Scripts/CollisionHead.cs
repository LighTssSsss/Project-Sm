using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHead : MonoBehaviour
{
    public bool obstaculoencima;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ParedBaja"))
        {
            obstaculoencima = true;
            
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ParedBaja"))
        {
            obstaculoencima = false;
           
        }
    }
}
