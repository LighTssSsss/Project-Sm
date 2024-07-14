using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float health;
    public bool recupera;
    public Image healthDamage;
    public GameObject murio;

    private void Start()
    {
        murio.SetActive(false);
    }

    private void Update()
    {
        HealthDamagFeedback();

        if(health <= 0)
        {
            Destroy(gameObject);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            murio.SetActive(true);
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Vacio")
        {
            health = 0;
        }
        
    }
}
