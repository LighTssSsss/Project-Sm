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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DamageWithHand();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
       /* if (hit.collider.CompareTag("Player") && hit != null)
        {
            Debug.Log("Daño");
            HealthSystem health = hit.collider.GetComponent<HealthSystem>();
            if(health != null)
            {
                health.GetComponent<HealthSystem>().SetDamageHealth(damage);
            }
        }*/
    }

    private void DamageWithHand()
    {
        Collider[] collider = Physics.OverlapSphere(centro.position, radio, layermask);

        
        /*
        for(int i = 0; i < collider.Length; i++)
        {
            Debug.Log(" Se detecto el choque con Player");
            estaAtacando = true;

            if(estaAtacando == true)
            {
               
            }


            StartCoroutine(NotDamage());

        }*/
        //Añadir una corutina para hacer daño

        foreach(Collider player in collider)
        {
           
            if(player == null)
            {
                return;
                Debug.Log(" Se detecto que es nulo");
            }
            Debug.Log(" Se detecto el choque con Player");

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

          /*  if (canDamage == true && player != null)
            {
                player.GetComponent<HealthSystem>().SetDamageHealth(damage);
                 StartCoroutine(Damage());
                //canDamage = false;
            }*/
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
