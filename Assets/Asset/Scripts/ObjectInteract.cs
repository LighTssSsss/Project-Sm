using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
   public GameObject objects;
   public Transform hand;
   public bool tomo;
   public bool loTiene;
   public bool losuelta;
    

    private void Awake()
    {
        GameObject handOb = GameObject.FindGameObjectWithTag("Hand");

        hand = handOb.GetComponent<Transform>();
    }

    private void Update()
    {
        if(tomo == true && loTiene == false )
        {
            objects.transform.SetParent(hand);
            objects.transform.position = hand.position;
            objects.GetComponent<Rigidbody>().isKinematic = true;
            loTiene = true;
           
        }

        if (losuelta == true && objects != null)
        {
            objects.transform.SetParent(null);
            objects.GetComponent<Rigidbody>().isKinematic = false;
            
            Debug.Log("Lo suelta");
            loTiene = false;
            tomo = false;
            losuelta = false;
        }
        
       
    }




}
