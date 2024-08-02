using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField] private float duracionShake;
    [SerializeField] private float intensidadShake;

    private Vector3 posicionInicial;
    private float duracionActualShake = 0f;

    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(duracionActualShake > 0)
        {
            Vector3 randomOffset = Random.insideUnitSphere * intensidadShake;
            transform.localPosition = Vector3.Lerp(transform.localPosition, posicionInicial + randomOffset, Time.deltaTime * 10);

            duracionActualShake -= Time.deltaTime;
        }

        else
        {
            //transform.localPosition = posicionInicial;
            transform.localPosition = Vector3.Lerp(transform.localPosition, posicionInicial, Time.deltaTime * 10);

        }
    }

    public void EfectoShake()
    {
        duracionActualShake = duracionShake;
    }
}
