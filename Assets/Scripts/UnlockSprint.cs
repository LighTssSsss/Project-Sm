using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSprint : MonoBehaviour
{
    [SerializeField] private float forwardOffset = 1;
    [SerializeField] private float verticalOffset = 0;
    [SerializeField] private Vector3 size = Vector3.one;
    [SerializeField] private LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {

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

            // Debug.Log(" Se detecto el choque con Player");

            MoveAndAnimatorController moveSystem = player.GetComponent<MoveAndAnimatorController>();
            if (moveSystem != null)
            {
               moveSystem.sprintLiberado = true;
            }

            else
            {
                Debug.Log("no tiene el componenete");
            }

            StartCoroutine(Elimated());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 center = transform.position + (transform.forward * forwardOffset);
        center.y += verticalOffset;
        Gizmos.DrawWireCube(center, size);
    }

    IEnumerator Elimated()
    {
        
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
