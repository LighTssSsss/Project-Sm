using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
   public GameObject objects;
   public Transform hand;


    private void Awake()
    {
        GameObject handOb = GameObject.FindGameObjectWithTag("Hand");

        hand = handOb.GetComponent<Transform>();
    }

    private void Update()
    {
        
    }

}
