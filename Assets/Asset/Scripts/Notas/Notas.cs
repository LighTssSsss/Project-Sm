using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Notas : MonoBehaviour
{
    public SOnotas notas;

    public TextMeshProUGUI texto;

    public bool LoToco;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((LoToco))
        {
            texto.text = notas.informacion;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("interracion"))
        {
            LoToco = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("interracion"))
        {
            LoToco = false;
        }
    }
}
