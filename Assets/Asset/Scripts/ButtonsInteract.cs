using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsInteract : MonoBehaviour
{
    public void Reiniciar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
       
    }
}
