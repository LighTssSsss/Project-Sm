using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Animator fade;

    // Start is called before the first frame update
   

    public void Comenzar()
    {
        Time.timeScale = 1;
        StartCoroutine(ApareceFadeMenu());      
        StartCoroutine(Jugar());
    }

    public void SalirJuego()
    {
        Application.Quit();
    }

    public void Cambio(string nomb)
    {
        fade.Play(nomb);
    }

    private IEnumerator ApareceFadeMenu()
    {
        yield return new WaitForSeconds(0.45f);
        fade.Play("FadeMenuOut");
    }

    private IEnumerator Jugar()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }


   
}
