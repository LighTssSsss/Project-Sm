using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health;
    public bool recupera;

    public void SetRecoverHealth(float recover)
    {
        if(health < 85f)
        {
            this.health += recover;
        }

        if(health > 100f)
        {
            health = 100f;
        }
       
        recupera = false;
    }

    public void SetDamageHealth(float damage)
    {
        this.health -= damage;
    }
}
