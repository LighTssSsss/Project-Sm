using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Game : MonoBehaviour
{

    PlayerInputs playerInput;
    public bool pausePressed;
    public GameObject pausa;
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
        playerInput = new PlayerInputs();
        playerInput.Game.Pausa.started += OnPausa;
        playerInput.Game.Pausa.canceled -= OnPausa;
        pausa.SetActive(false);
    }
  
    // Update is called once per frame
    void Update()
    {
        if(pausePressed)
        {
           Time.timeScale = 0;
           pausa.SetActive(true);
           Cursor.lockState = CursorLockMode.None;
        }

       else
        {
           
        }
    }

    void OnPausa(InputAction.CallbackContext context)
    {
        pausePressed = context.ReadValueAsButton();
    }


    
}
