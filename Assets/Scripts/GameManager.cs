using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject musica;
    public VidaJugador health;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        musica.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(health.vidas <= 0)
        {
            musica.SetActive(false);
        }
       
    }
}
