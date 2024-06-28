using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderData;

public class Botones : MonoBehaviour
{
    public GameObject pausa;
    public void Comenzar()
    {

    }

    public void Continuar()
    {

    }

    public void Salir()
    {

    }

    public void ContinuarPausa()
    {
        Game.Instance.SetPausa(false);
        //Debug.Log("presiono");
        //Time.timeScale = 1;
        //Cursor.lockState = CursorLockMode.Locked;
        //pausa.SetActive(false);
    }
}
