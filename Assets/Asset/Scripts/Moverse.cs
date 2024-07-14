using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moverse : MonoBehaviour
{
    public GameObject moverse;
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
        yield return new WaitForSeconds(44);
        moverse.SetActive(true);
    }


    IEnumerator Desaparece()
    {
        yield return new WaitForSeconds(49);
        moverse.SetActive(false);
    }
}
