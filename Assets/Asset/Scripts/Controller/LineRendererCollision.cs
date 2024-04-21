using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererCollision : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRender;
    [SerializeField] private LayerMask collisionLayer;
  

    // Update is called once per frame
    void Update()
    {
        //Lanzo un raycast entre el LineRenderer

        Vector3[] points = new Vector3[lineRender.positionCount];
        lineRender.GetPositions(points);

        for(int i = 0; i < points.Length - 1; i++)
        {
            // Lanzo el raycast entre puntos de lineRender

            Vector3 start = points[i];
            Vector3 end = points[i - 1];
            Vector3 direction = (end - start).normalized;
            float distance = Vector3.Distance(start, end);

            RaycastHit hit;

            if(Physics.Raycast(start, direction, out hit, distance, collisionLayer))
            {
                points[i + 1] = hit.point; //Ajusto el punto donde se detecto la colision;

                lineRender.positionCount = i + 2; //Reducir el numero de puntos;

                break; // Termino el ciclo

            }
            lineRender.SetPositions(points); // Lo actualizo
        }
    }
}
