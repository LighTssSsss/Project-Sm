using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventoSonido : MonoBehaviour
{
    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }
    
    // Sonido Jugador

    public void SonidoPasos1()
    {
        soundManager.SeleccionAudio(0, 0.02f);
    }

    public void SonidoPasos2()
    {
        soundManager.SeleccionAudio(1, 0.02f);
    }

    public void SonidoSaltoSorteo()
    {
        soundManager.SeleccionAudio(2, 0.02f);
    }

    public void SonidoDano()
    {
        soundManager.SeleccionAudio(3, 0.02f);
    }

    //Mundo

    public void SonidoLlave()
    {
        soundManager.SeleccionAudio(4, 0.02f);
    }

    public void SonidoPapelTomando()
    {
        soundManager.SeleccionAudio(5, 0.02f);
    }

    public void SonidoPuertaAbriendose()
    {
        soundManager.SeleccionAudio(6, 0.02f);
    }

    public void SonidoRejaAbriendose()
    {
        soundManager.SeleccionAudio(7, 0.02f);
    }


    public void SonidoPresionBajando()
    {
        soundManager.SeleccionAudio(8, 0.01f);
    }

    public void SonidoMedicina()
    {
        soundManager.SeleccionAudio(9, 0.02f);
    }

    public void SonidoAgachado()
    {
        soundManager.SeleccionAudio(10, 0.05f);
    }
}
