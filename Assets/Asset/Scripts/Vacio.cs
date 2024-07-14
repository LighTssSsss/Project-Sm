using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacio : MonoBehaviour
{
    [SerializeField] private float forwardOffset = 1;
    [SerializeField] private float verticalOffset = 0;
    [SerializeField] private Vector3 size = Vector3.one;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private bool canDamage = true;
    private float damage = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 center = transform.position + (transform.forward * forwardOffset);
        center.y += verticalOffset;

        Collider[] colliders = Physics.OverlapBox(center, size, transform.rotation, layerMask);

        foreach (Collider player in colliders)
        {
            if (player == null)
            {
                return;
                //Debug.Log(" Se detecto que es nulo");
            }

            // Debug.Log(" Se detecto el choque con Player");

            HealthSystem healthSystem = player.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                
                    Debug.Log("Hace daño");
                    healthSystem.SetDamageHealth(damage);
  
                
            }

            else
            {
                Debug.Log("no tiene el componenete");
            }
        }*/
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

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 center = transform.position + (transform.forward * forwardOffset);
        center.y += verticalOffset;
        Gizmos.DrawWireCube(center, size);
    }*/
}
