using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoverhealth : MonoBehaviour
{
    [SerializeField] private float porcentajeRecuperado;
    [SerializeField] private float vidaTotal;
    public EventoSonido sonidos;
    private VidaJugador vida;
    public GameObject texto;

    private void Awake()
    {    
        vida = FindObjectOfType<VidaJugador>();
        texto.SetActive(false);
    }

    private void Update()
    {
        if(vida.recupera == true)
        {
            
            sonidos.SonidoMedicina();
            texto.SetActive(false);
            transform.position = new Vector3(transform.position.x, 8f, transform.position.z);
            float recuperaValue = vidaTotal * porcentajeRecuperado;
            vida.RecuperaVida(recuperaValue);
            vida.recupera = false;
            Destroy(this.gameObject);
        }
    }
}
