using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    
    [SerializeField] private GameObject door;

    private void Awake()
    {
        door.SetActive(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Push"))
        {
            door.SetActive(false);
        }

        
    }
}
