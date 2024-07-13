using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltoTexto : MonoBehaviour
{
    public GameObject salto;
    private bool textoSalto;
    // Start is called before the first frame update
    void Start()
    {
        salto.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (textoSalto == true)
        {
            StartCoroutine(SaltoDesaparece());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && textoSalto == false)
        {
            salto.SetActive(true);
            textoSalto = true;
            
        }
    }


    IEnumerator SaltoDesaparece()
    {
        yield return new WaitForSeconds(4);
        this.gameObject.SetActive(false);
    }

    
}
