using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llave : MonoBehaviour
{
    public bool toma;
    public GameObject texto;
    public EventoSonido sonidos;
    public GameObject luz;
    // Start is called before the first frame update
    void Start()
    {
        texto.SetActive(false);
        luz.SetActive(true);
        //sonidos = FindObjectOfType<EventoSonido>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toma == true)
        {
            sonidos.SonidoLlave();
            luz.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("interracion"))
        {
            texto.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("interracion"))
        {
            texto.SetActive(false);
        }
    }
}
