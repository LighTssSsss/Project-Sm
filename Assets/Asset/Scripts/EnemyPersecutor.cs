using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPersecutor : MonoBehaviour
{
    [SerializeField] private float damage;
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
        if (hit.collider.CompareTag("Player") && hit != null)
        {
            Debug.Log("Daño");
            HealthSystem health = hit.collider.GetComponent<HealthSystem>();
            if(health != null)
            {
                health.GetComponent<HealthSystem>().SetDamageHealth(damage);
            }
        }
    }
}
