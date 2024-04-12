using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay;


using UnityEngine.AI; // Para usar el NavMesh Agente

public class FunctionalAdult : MonoBehaviour,  IHears
{
    [SerializeField] private NavMeshAgent agent = null;

    private void Awake()
    {
        if(!TryGetComponent(out agent))
        {
            Debug.LogWarning(name + "No tiene la IA agente");
        }
       
    }

    public void RespondToSound(SoundChecker soundCheck)
    {
        
        if(soundCheck.soundType == SoundChecker.SoundType.Insteresting)
        {
            MoveTo(soundCheck.pos);
        }

        else if(soundCheck.soundType == SoundChecker.SoundType.RespondPlayer)
        {
            Vector3 dir = (soundCheck.pos - transform.position).normalized;
            MoveTo(transform.position - (dir * 10f));
        }
        
        Debug.Log(name + " responde al sonido de "+ soundCheck.pos);
    }

    private void MoveTo(Vector3 posi)
    {
        agent.SetDestination(posi);
       // agent.isStopped = false;
    }
}
