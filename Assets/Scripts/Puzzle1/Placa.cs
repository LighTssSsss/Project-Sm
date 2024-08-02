using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placa : MonoBehaviour
{
    public Animator placa;
    public Animator puerta;
    private bool ModoAbre;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ModoAbre = true;
           
        }


        if (ModoAbre == true)
        {
            ReproduceAnimacion("PlacaBaja");

            ReproduceAnimacionDos("PuertaAbriendose");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "CajaP1")
        {
            ReproduceAnimacion("PlacaBaja"); 

            ReproduceAnimacionDos("PuertaAbriendose"); 
        }
    }


    private void ReproduceAnimacion(string animacion)
    {
        placa.Play(animacion);
    }

    private void ReproduceAnimacionDos(string animacion)
    {
        puerta.Play(animacion);
    }

}
