using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacio : MonoBehaviour
{
    private float damage = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Player")
        {
            HealthSystem healthSystem = hit.gameObject.GetComponent<HealthSystem>();

            if (healthSystem != null)
            {
                   Debug.Log("Hace daño");
                   healthSystem.SetDamageHealth(damage);
                 
                
            }

            else
            {
                Debug.Log("no tiene el componenete");
            }
        }
    }
}
