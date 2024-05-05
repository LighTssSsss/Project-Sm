using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   [SerializeField] private HealthSystem player;
   [SerializeField] private GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.health <= 0)
        {
            panel.SetActive(true);
        }

       
    }
}
