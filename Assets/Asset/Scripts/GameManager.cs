using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   [SerializeField] private MoveAndAnimatorController player;
   [SerializeField] private FieldOfView enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.canSeePlayer == true)
        {
            player.inPersecution = true;
        }

        else
        {
            player.inPersecution = false;
        }
    }
}
