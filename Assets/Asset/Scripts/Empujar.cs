using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empujar : MonoBehaviour
{
    public GameObject sortear;
    private bool textoSortear;
    // Start is called before the first frame update

    void Start()
    {
        sortear.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (textoSortear == true)
        {
            StartCoroutine(SaltoDesaparece());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && textoSortear == false)
        {
            sortear.SetActive(true);
            textoSortear = true;

        }
    }


    IEnumerator SaltoDesaparece()
    {
        yield return new WaitForSeconds(2);
        sortear.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
