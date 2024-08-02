using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GamePlay
{
    public static class SoundsCh
    {
        public static void MakeSound(SoundChecker sound)
        {
            Collider[] col = Physics.OverlapSphere(sound.pos, sound.ranges);

            for(int i = 0; i < col.Length; i++)
            {
                if (col[i].TryGetComponent(out IHears hearer))
                {
                    hearer.RespondToSound(sound);
                }
            }
        }
    }

}

