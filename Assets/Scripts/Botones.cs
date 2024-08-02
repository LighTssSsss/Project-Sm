using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.ShaderData;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{
    public Animator fade;
    public GameObject pausa;

    
    public void Comenzar()
    {
        Time.timeScale = 1;
        StartCoroutine(ApareceFade());
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(Jugar());
    }

    public void SalirJuego()
    {
        Application.Quit();
    }

    public void SalirMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(ApareceFade());
        StartCoroutine(IrMenu());
    }

    public void ContinuarPausa()
    {
        Game.Instance.SetPausa(false);
        //Debug.Log("presiono");
        //Time.timeScale = 1;
        //Cursor.lockState = CursorLockMode.Locked;
        //pausa.SetActive(false);
    }


    public void Cambio(string nomb)
    {
        fade.Play(nomb);
    }


    private IEnumerator ApareceFade()
    {
        yield return new WaitForSeconds(0.45f);
        fade.Play("FadeOut");
    }

    private  IEnumerator Jugar()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }

    private IEnumerator IrMenu()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
}
