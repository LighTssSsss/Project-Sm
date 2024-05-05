using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToDeactivate;

    // Método para inicializar el script
    private void Awake()
    {
        // Si necesitas que los objetos estén activos al principio
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(true);
        }
    }

    // Método para manejar el evento de colisión
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entra tiene el tag "Push"
        if (other.gameObject.CompareTag("Push"))
        {
            // Desactiva todos los objetos en la lista
            foreach (GameObject obj in objectsToDeactivate)
            {
                obj.SetActive(false);
            }
        }
    }
    /*[SerializeField] private GameObject door;

    private void Awake()
    {
        //door.SetActive(true);
    }
  

   /* private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Push"))
        {
            door.SetActive(false);
        }
    }*/
}
