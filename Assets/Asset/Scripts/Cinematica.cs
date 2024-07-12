using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cinematica : MonoBehaviour
{

    [SerializeField] private float forwardOffset = 1;
    [SerializeField] private float verticalOffset = 0;
    [SerializeField] private Vector3 size = Vector3.one;
    [SerializeField] private LayerMask layerMask;
     public PlayableDirector cinematica;

    public float tiempoApaga;
    public bool poseeCinematica;
    public bool tengoCinematica = true;
    private bool CinematicaListaPlayer;
    private bool puedoR;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        ReproduceCinematicas();

    if(Input.GetKeyDown(KeyCode.E) && puedoR == true)
      {
            cinematica.Play();
            StartCoroutine(Apaga());
       }
    }



    private void ReproduceCinematicas()
    {
        Vector3 center = transform.position + (transform.forward * forwardOffset);
        center.y += verticalOffset;

        Collider[] colliders = Physics.OverlapBox(center, size, transform.rotation, layerMask);

        foreach (Collider player in colliders)
        {
            /*
            DataManager.data.posicionPlayer = true;
            DataManager.data.posicion = this.transform.position;
            DataManager.data.posicion = this.transform.position;*/
            puedoR = true;
            //cinematica.Play();
            Debug.Log("Puedo iniciar la cinematica");


        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 center = transform.position + (transform.forward * forwardOffset);
        center.y += verticalOffset;

        Gizmos.DrawWireCube(center, size);
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
