using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CambioObjectivo : MonoBehaviour
{
    [SerializeField] private float forwardOffset = 1;
    [SerializeField] private float verticalOffset = 0;
    [SerializeField] private Vector3 size = Vector3.one;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private string objetivo;
    [SerializeField] private GameObject puerta;
    [SerializeField] private GameObject obstaculo;
    [SerializeField] private TextMeshProUGUI textoObjectivo;
    private void Start()
    {
        puerta.SetActive(true);
        obstaculo.SetActive(false);
    }

    // Update is called once per frame
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
            puerta.SetActive(false);
            obstaculo.SetActive(true);
            textoObjectivo.text = objetivo;
            StartCoroutine(Close());
            Debug.Log("Ejecuta cinematica para cambio del mundo hacia la pesadilla y actualiza el objetivo");


        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 center = transform.position + (transform.forward * forwardOffset);
        center.y += verticalOffset;
        Gizmos.DrawWireCube(center, size);
    }

    IEnumerator Close()
    {
       
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
        
    }
}
