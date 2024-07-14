using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSonido : MonoBehaviour
{
    public GameObject sonidoseleccionar;
    public GameObject sonidoPresionar;
   


    public void Seleccionar()
    {
        Instantiate(sonidoseleccionar);
    }

    public void Presionar()
    {
        Instantiate(sonidoPresionar);
    }
}
