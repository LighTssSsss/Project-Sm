using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class GuardadoPosicionYCinematica : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
      
       
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && DataManager.data.posicionPlayer == true)
        {
            DataManager.data.posicion = this.transform.position;

        }
    }

   
}

