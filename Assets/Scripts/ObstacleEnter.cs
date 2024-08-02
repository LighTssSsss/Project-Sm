using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEnter : MonoBehaviour
{
    [SerializeField] private float posicionVerticalAbajo = 1;
    [SerializeField] private float posicionVerticalArriba = 0;
    [SerializeField] private Vector3 ancho = Vector3.one;
    [SerializeField] private LayerMask capaDano;
    [SerializeField] private bool puedoDanar = true;
    [SerializeField] private float dano;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center = transform.position + (transform.forward * posicionVerticalAbajo);
        center.y += posicionVerticalArriba;

        Collider[] colliders = Physics.OverlapBox(center, ancho, transform.rotation, capaDano);

        foreach (Collider player in colliders)
        {
            if (player == null)
            {
                return;
               
            }


            VidaJugador vidas = player.GetComponent<VidaJugador>();
            if (vidas != null)
            {
                if (puedoDanar)
                {
                    vidas.SetDanoVidaJugador(dano);
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
        Gizmos.color = Color.red;

        Vector3 center = transform.position + (transform.forward * posicionVerticalAbajo);
        center.y += posicionVerticalArriba;
        Gizmos.DrawWireCube(center, ancho);
    }

    IEnumerator Damage()
    {
        puedoDanar = false;
        yield return new WaitForSeconds(2f);
        puedoDanar = true;
    }
}
