using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cambios : MonoBehaviour
{
    [SerializeField] private float forwardOffset = 1;
    [SerializeField] private float verticalOffset = 0;
    [SerializeField] private Vector3 size = Vector3.one;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private string objetivo;
    [SerializeField] private TextMeshProUGUI textoObjectivo;

    void Update()
    {
        Vector3 center = transform.position + (transform.forward * forwardOffset);
        center.y += verticalOffset;

        Collider[] colliders = Physics.OverlapBox(center, size, transform.rotation, layerMask);
        foreach (Collider player in colliders)
        {
            if (player == null)
            {
                return;
                //Debug.Log(" Se detecto que es nulo");
            }

            Debug.Log("Ejecuta cinematica de aparicion del primer enemigo y actualiza el objetivo");

            textoObjectivo.text = objetivo;
            StartCoroutine(CloseCambios());
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 center = transform.position + (transform.forward * forwardOffset);
        center.y += verticalOffset;
        Gizmos.DrawWireCube(center, size);
    }

    IEnumerator CloseCambios()
    {

        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);

    }
}
