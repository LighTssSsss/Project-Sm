using GamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBroke : MonoBehaviour
{
    public bool isRelease;
    [SerializeField] private AudioSource source = null;
    [SerializeField] private float soundRange;


    private void OnCollisionEnter(Collision collision)
    {
        if (isRelease)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                Debug.Log("Reproduce Sonido");
                SoundPlay();
            }
        }
    }


   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, soundRange);

    }


    public void SoundPlay()
    {
        if (source.isPlaying)
        {
            return;
        }

        source.Play();

        var sound = new SoundChecker(transform.position, soundRange);

        Debug.Log("Sound wit position: " + (sound.pos) + "and range: " + (sound.ranges));
        sound.soundType = SoundChecker.SoundType.Insteresting;
        SoundsCh.MakeSound(sound);
    }

}
