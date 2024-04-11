using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
   

    public class SoundChecker
    {
        public enum SoundType { Default = -1, Insteresting, RespondPlayer }
        public readonly Vector3 pos;
        public readonly float ranges;
        public SoundType soundType;


        public SoundChecker(Vector3 position, float range)
        {
            pos = position;
            ranges = range;
        }

        
    }

   
}

