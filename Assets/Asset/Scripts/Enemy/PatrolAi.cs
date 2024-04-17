using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAi : MonoBehaviour
{
    public List<Transform> wayPoint;
    NavMeshAgent agent;
    public FieldOfView fieldView;
    public FunctionalAdult functionalAdu;
    public int currentWayPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        fieldView = GetComponent<FieldOfView>();
        functionalAdu = GetComponent<FunctionalAdult>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fieldView.canSeePlayer == false && functionalAdu.cambio == false )
        {
            Walking();
        }
    }


    private void Walking()
    {
        if(wayPoint.Count == 0)
        {
            return;
        }

        float distanceToWayPoint = Vector3.Distance(wayPoint[currentWayPointIndex].position, transform.position);

        if(distanceToWayPoint <= 3)
        {
            currentWayPointIndex = (currentWayPointIndex + 1) % wayPoint.Count;
        }
        agent.SetDestination(wayPoint[currentWayPointIndex].position);
    }
}
