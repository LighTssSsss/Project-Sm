using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPersecutor : MonoBehaviour
{
    [SerializeField] private float damage;

    public Transform centro;
    public float radio;
    public LayerMask layermask;
    //bool estaAtacando = false;
    [SerializeField] private bool canDamage = true;

    // Update is called once per frame
    void Update()
    {
        DamageWithHand();
    }

   

    private void DamageWithHand()
    {
        Collider[] collider = Physics.OverlapSphere(centro.position, radio, layermask);
               
        //Añadir una corutina para hacer daño
        foreach(Collider player in collider)
        {
           
            if(player == null)
            {
                return;
                //Debug.Log(" Se detecto que es nulo");
            }

           // Debug.Log(" Se detecto el choque con Player");

            HealthSystem healthSystem = player.GetComponent<HealthSystem>();
            if(healthSystem != null)
            {
                if (canDamage)
                {
                    Debug.Log("Hace daño");
                    healthSystem.SetDamageHealth(damage);
                    StartCoroutine(Damage());
                }
            }

            else
            {
                Debug.Log("no tiene el componenete");
            }         
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(centro.position, radio);
    }

    IEnumerator Damage()
    {        
            canDamage = false;
            yield return new WaitForSeconds(2);
            canDamage = true;               
    }
}
