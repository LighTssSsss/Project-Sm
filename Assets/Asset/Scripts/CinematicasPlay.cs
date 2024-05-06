using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CinematicasPlay : MonoBehaviour
{
    [SerializeField] private float forwardOffset = 1;
    [SerializeField] private float verticalOffset = 0;
    [SerializeField] private Vector3 size = Vector3.one;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject objeto;

    
    private void Start()
    {
        objeto.SetActive(false);
    }

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

            // Debug.Log(" Se detecto el choque con Player");

            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            objeto.SetActive(true);
            StartCoroutine(CloseCambios());
            Debug.Log("Muestrame cinematica de salida del mundo o de la pesadilla");

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
