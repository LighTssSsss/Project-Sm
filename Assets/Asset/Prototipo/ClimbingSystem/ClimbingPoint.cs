using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClimbingPoint : MonoBehaviour
{
    public bool mountPoint;

    public List<Neighbour> neighbours;



    private void Awake()
    {
        var twoWayClimbingNeighbour = neighbours.Where(n => n.isPointTwoWay);

        foreach(var neighbour in twoWayClimbingNeighbour)
        {
            neighbour.climbingPoint?.CreaterPointConnection(this, -neighbour.pointDirection, neighbour.connectionType, neighbour.isPointTwoWay);
        }
    }


    public void CreaterPointConnection(ClimbingPoint climbinPoint, Vector2 pointDirection, ConnectionType connectionType, bool isPointTwoWay)
    {
        var neighbour = new Neighbour()
        {
            climbingPoint = climbinPoint,
            pointDirection = pointDirection,
            connectionType = connectionType,
            isPointTwoWay = isPointTwoWay
        };

        neighbours.Add(neighbour);
    }

    
    public Neighbour GetNeighbour( Vector2 climDirection)
    {
        Neighbour neighbour = null;

        if(climDirection.y != 0)
        {
            neighbour = neighbours.FirstOrDefault(n => n.pointDirection.y == climDirection.y);
        }

        if (neighbour == null && climDirection.x != 0)
        {
            neighbour = neighbours.FirstOrDefault(n => n.pointDirection.x == climDirection.x);
        }

        return neighbour;


    }


    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.blue);

        foreach(var neighbour in neighbours) //visible la linea de conexion
        {
            if (neighbour.climbingPoint != null)
            {
                Debug.DrawLine(transform.position, neighbour.climbingPoint.transform.position, (neighbour.isPointTwoWay) ? Color.green : Color.black);
            }
            
        }
    }

}



[System.Serializable]
public class Neighbour
{
    public ClimbingPoint climbingPoint;
    public Vector2 pointDirection;
    public ConnectionType connectionType;
    public bool isPointTwoWay = true;



}

public enum ConnectionType {Jump, Move}

