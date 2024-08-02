using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay;


using UnityEngine.AI; // Para usar el NavMesh Agente

public class FunctionalAdult : MonoBehaviour,  IHears
{
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private FieldOfView field;
    public bool cambio;
    private void Awake()
    {
        if(!TryGetComponent(out agent))
        {
            Debug.LogWarning(name + "No tiene la IA agente");
        }
       
        field = GetComponent<FieldOfView>();
    }

    private void Update()
    {
        if (cambio)
        {
            StartCoroutine(Vuelve());
        }
    }

    public void RespondToSound(SoundChecker soundCheck)
    {
        
        if(soundCheck.soundType == SoundChecker.SoundType.Insteresting && field.canSeePlayer == false)
        {
            MoveTo(soundCheck.pos);
            cambio = true;
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



    IEnumerator Vuelve()
    {
        yield return new WaitForSeconds(5f);
        cambio = false;
    }
}
