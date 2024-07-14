using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Game : MonoBehaviour
{

    public static Game Instance;

    PlayerInputs playerInput;
    public static bool estaPausado;
    public GameObject pausa;
    public HealthSystem health;
    // Start is called before the first frame update
    
    private void OnEnable()
    {
        playerInput.Game.Enable();
    }

    private void OnDisable()
    {
        playerInput.Game.Disable();
    }

    private void Awake()
    {
        Instance  = this;
        health.GetComponent<HealthSystem>();
        playerInput = new PlayerInputs();
        playerInput.Game.Pausa.started += OnPausa;
        playerInput.Game.Pausa.canceled -= OnPausa;
        pausa.SetActive(false);
    }
  
    void OnPausa(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() && health.health >= 1 )
        {
            Toggle();
        }
    }

    private void Toggle()
    {
        estaPausado = !estaPausado;
        SetPausa(estaPausado);
    }

    public void SetPausa(bool active)
    {
        if (active)
        {
            Time.timeScale = 0;
            pausa.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Debug.Log("presiono");
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            pausa.SetActive(false);
        }
    } 


    
}
