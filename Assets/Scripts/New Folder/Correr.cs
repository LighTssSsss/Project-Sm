using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Correr : MonoBehaviour
{
    public GameObject correr;
    private bool textocorrer;
    // Start is called before the first frame update
    void Start()
    {
        correr.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (textocorrer == true)
        {
            StartCoroutine(SaltoDesaparece());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && textocorrer == false)
        {
            correr.SetActive(true);
            textocorrer = true;

        }
    }


    IEnumerator SaltoDesaparece()
    {
        yield return new WaitForSeconds(4);
        this.gameObject.SetActive(false);
    }

}
