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
  

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Push"))
        {
            door.SetActive(false);
        }
    }
}
