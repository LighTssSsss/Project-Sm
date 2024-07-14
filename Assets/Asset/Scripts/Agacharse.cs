using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agacharse : MonoBehaviour
{
    public GameObject agacharse;
    private bool textoagacha;
    // Start is called before the first frame update
    void Start()
    {
        agacharse.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (textoagacha == true)
        {
            StartCoroutine(SaltoDesaparece());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && textoagacha == false)
        {
            agacharse.SetActive(true);
            textoagacha = true;

        }
    }


    IEnumerator SaltoDesaparece()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }

}
