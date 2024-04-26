using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPersecutor : MonoBehaviour
{
    [SerializeField] private float damage;

    public Transform centro;
    public float radio;
    public LayerMask layermask;
    bool estaAtacando = false;


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
            Debug.Log(" Se detecto el choque con Player");
            estaAtacando = true;
            if(estaAtacando == true)
            {
                player.GetComponent<HealthSystem>().SetDamageHealth(damage);
                estaAtacando = false;
            }
        }

       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(centro.position, radio);
    }

    IEnumerator NotDamage()
    {
        if(estaAtacando == true)
        {
            yield return new WaitForSeconds(2);
            estaAtacando = false;
        }
       
    }
}
