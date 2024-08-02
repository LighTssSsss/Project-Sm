using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoObjeto : MonoBehaviour
{
    public Rigidbody rb;
    public AudioSource sonido;
    // Start is called before the first frame update
    void Start()
    {
        
        rb.GetComponent<Rigidbody>();
        sonido.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.x >= 0 || rb.velocity.z >= 0)
        {
            sonido.Play();
        }
    }
}
