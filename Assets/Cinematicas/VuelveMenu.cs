using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class VuelveMenu : MonoBehaviour
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
    private bool puedoHacerlo;


    public GameObject objetos;
    public GameObject texto;
    public Animator textoD;
    public MoveAndAnimatorController move;
    // Start is called before the first frame update
    void Start()
    {
        move = FindObjectOfType<MoveAndAnimatorController>();
       
        texto.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ReproduceCinematicas();

        if (Input.GetKeyDown(KeyCode.E) && puedoR == true && puedoHacerlo == true)
        {
            move.enabled = false;
            cinematica.Play();
            StartCoroutine(Apaga());
            StartCoroutine(ApagaObjeto());
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            texto.SetActive(true);
            puedoHacerlo = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            //ReproduceTexto("InteractuarDesaparece");
            texto.SetActive(false);
            puedoHacerlo = false;
        }

    }

    public void ReproduceTexto(string nombreA)
    {
        textoD.Play(nombreA);
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
        move.enabled = true;
        SceneManager.LoadScene(0);
       // this.gameObject.SetActive(false);
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

    IEnumerator ApagaObjeto()
    {
        yield return new WaitForSeconds(2);
        objetos.SetActive(false);




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
