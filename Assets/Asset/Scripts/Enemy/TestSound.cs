using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GamePlay
{
    public class TestSound : MonoBehaviour
    {
        [SerializeField] private AudioSource source = null;

        [SerializeField] private float soundRange;

        private void Start()
        {
            
        }

        private void Update()
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
}

