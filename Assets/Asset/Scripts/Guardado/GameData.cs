using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


[System.Serializable]
public class GameData
{
    public float vidaActual; // variable vida que quiero guardar
    public Vector3 posicion; // posicion que quiero guardar del player
    public bool cinematicaTermino; // veo si la cinematica ya se reprodujo
    public bool parkour;
    public bool persecucion;

   // public bool desactivado;

    public bool primeraVez;
    public bool primeraVez2;
    public bool primeraVez3;
    public bool primeraVez4;
    public bool primeraVez5;
   // public bool cinematicaSaltada; // Bool de Salto de cinematica

    public bool posicionPlayer; // Veo si esta true para guardar lo posicion


   // public bool guardaMusica;
    public bool camaras;

    public GameData()
    {
        vidaActual = 3;
        posicionPlayer = false;

        posicion = new Vector3(0, 0, 0);

        cinematicaTermino = false;
        parkour = false; //dash
        //desactivado = false;
        //puedodash = false;
        persecucion = false;
       // cinematicaSaltada = false;
        primeraVez = false;
        primeraVez2 = false;
        primeraVez3 = false;
        primeraVez4 = false;
        primeraVez5 = false;
        camaras = false;
        //guardaMusica = false;

    }
}
