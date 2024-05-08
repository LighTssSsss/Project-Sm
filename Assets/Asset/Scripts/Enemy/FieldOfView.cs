using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerReft;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    public bool estadoPersecusion;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        playerReft = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindWithTag("Player").transform;
        StartCoroutine(FOVRoutine());
    }

    // Update is called once per frame
    


    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);


        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
            
        }


    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;


            if(Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);



                if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    agent.speed = 4f;
                    estadoPersecusion = true;
                }

                else
                {
                    StartCoroutine(LostObjectTime());
                   // canSeePlayer = false;
                   // estadoPersecusion = false;
                }
            }

            else
            {
                canSeePlayer = false;
               // StartCoroutine(LostObjectTime());
                //estadoPersecusion = false;
            }
        }

        else if (canSeePlayer)
        {
            //canSeePlayer = false;
            StartCoroutine(LostObjectTime());
            
            agent.speed = 3.5f;
            //estadoPersecusion = false;
        }
    }


    void Update()
    {
        if (estadoPersecusion == false)
        {
            return;
        }

        if(target == null)
        {
            return;
        }

        else
        {
            if(Vector3.Distance(transform.position,target.position ) < angle)
            {
                agent.SetDestination(target.position);
            }
        }
    }

    IEnumerator LostObjectTime()
    {      
        yield return new WaitForSeconds(4);
        canSeePlayer = false;
        estadoPersecusion = false;        
    }
}
