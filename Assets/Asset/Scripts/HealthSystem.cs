using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float health;
    public bool recupera;
    public Image healthDamage;

    private void Update()
    {
        HealthDamagFeedback();

        if(health <= 0)
        {
            Destroy(gameObject);
        }

    }

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

    public void HealthDamagFeedback()
    {
        float transparency = 1f - (health / 100);
        Color imageColor = Color.white;
        imageColor.a = transparency;
        healthDamage.color = imageColor;
    }
}
