using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaJugador : MonoBehaviour
{
    public float vidas;
    public bool recupera;
    public Image sangre;
    public GameObject murio;
    public EventoSonido eventoSonidos;
    public Shake efectoShake;

   

    private void Start()
    {
        murio.SetActive(false);
    }

    private void Update()
    {
        PantallaDano();

        if (vidas <= 0)
        {
            Destroy(gameObject);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            eventoSonidos.SonidoDano();
            murio.SetActive(true);
        }

    }

    public void RecuperaVida(float recuperar)
    {
        if (vidas < 95f)
        {
            this.vidas += recuperar;

        }

        if (vidas > 100f)
        {
            vidas = 100f;
        }

        recupera = false;
    }

    public void SetDanoVidaJugador(float dano)
    {
        this.vidas -= dano;
        efectoShake.EfectoShake();
        eventoSonidos.SonidoDano();
    }

    public void PantallaDano()
    {
        float transparencia = 1f - (vidas / 100);
        Color imagenColor = Color.white;
        imagenColor.a = transparencia;

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Vacio")
        {
            vidas = 0;
        }

    }
}
