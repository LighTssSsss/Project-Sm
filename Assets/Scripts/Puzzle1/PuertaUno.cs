using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaUno : MonoBehaviour
{
    public bool abre;
    public Animator animation;

    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(abre == true)
        {
            ReproducePuerta("PuertaAbre2");
        }
    }


    private void ReproducePuerta(string puerta)
    {
        animation.Play(puerta);
    }
}
