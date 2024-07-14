using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moverse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Aparece());
        StartCoroutine(Desaparece());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator Aparece()
    {
        yield return new WaitForSeconds(45);
        this.gameObject.SetActive(true);
    }


    IEnumerator Desaparece()
    {
        yield return new WaitForSeconds(49);
        this.gameObject.SetActive(false);
    }
}
