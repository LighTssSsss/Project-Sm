using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoverhealth : MonoBehaviour
{
    [SerializeField] private float recoverAmount;
    [SerializeField] private float healthRecover;
    private HealthSystem health;
  
    private void Awake()
    {
        health = FindFirstObjectByType<HealthSystem>();
     
    }

    private void Update()
    {
        if(health.recupera == true)
        {
            health.GetComponent<HealthSystem>().SetRecoverHealth(healthRecover * recoverAmount);
            health.recupera = false;
            Destroy(this.gameObject);
        }
    }
}