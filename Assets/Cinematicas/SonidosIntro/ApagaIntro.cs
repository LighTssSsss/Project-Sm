using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class ApagaIntro : MonoBehaviour
{
    public PlayableDirector cinematica;
    public float tiempoApaga;
    public bool poseeCinematica;
    public bool tengoCinematica = true;
    private bool CinematicaListaPlayer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Apaga());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Apaga()
    {
        yield return new WaitForSeconds(tiempoApaga);
        this.gameObject.SetActive(false);
        if (cinematica != null)
        {
            cinematica.enabled = false;
            Debug.Log("Apaga");

        }

        if (poseeCinematica)
        {
            CinematicaListaPlayer = false;
            // DataManager.data.cinematicaTermino = CinematicaListaPlayer; // Este
            // Debug.Log("No se debe reproducir");
        }



    }


    public void Termina()
    {
        if (CinematicaListaPlayer)
        {
            double duracionTotal = cinematica.duration;
            //musica.SetActive(true);
            cinematica.time = duracionTotal;

            cinematica.playableGraph.Evaluate();
            cinematica.enabled = false;


        }

    }
}
