using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoverhealth : MonoBehaviour
{
    [SerializeField] private float recoverAmount;
    [SerializeField] private float healthRecover;
    public EventoSonido sonidos;
    private HealthSystem health;
  
    private void Awake()
    {
       // health = FindFirstObjectByType<HealthSystem>();
        health = FindObjectOfType<HealthSystem>();
        sonidos = FindObjectOfType<EventoSonido>();
    }

    private void Update()
    {
        if(health.recupera == true)
        {
            //health.GetComponent<HealthSystem>().SetRecoverHealth(healthRecover * recoverAmount);
            sonidos.SonidoMedicina();
            float recuperaValue = healthRecover * recoverAmount;
            health.SetRecoverHealth(recuperaValue);
            health.recupera = false;
            Destroy(this.gameObject);
        }
    }
}
