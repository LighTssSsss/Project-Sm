using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamaraSigue : MonoBehaviour
{
    [SerializeField] private Transform sigueAlObjetivo;
    [SerializeField] private float velocidadDeRotacion = 30f;
    [SerializeField] private float parteSuperior = 70f; // Limita el eje vertical
    [SerializeField] private float botonClamp = -40f;
    [SerializeField] private float distancia = 5f; // Distancia fija desde el objetivo

    private Transform camTransform;
    private float cinemachineObjetivoYaw;
    private float cinemachineObjetivoPitch;

    private void Start()
    {
        // Inicializa el transform de la c�mara
        camTransform = transform;

        // Ajusta la posici�n inicial de la c�mara detr�s del objetivo
        AjustarPosicionInicial();
    }

    private void LateUpdate()
    {
        CamaraLogica();
    }

    private void CamaraLogica()
    {
        if (sigueAlObjetivo == null)
            return;

        // Obt�n la entrada del mouse y ajusta la rotaci�n
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        cinemachineObjetivoYaw += mouseX * velocidadDeRotacion * Time.deltaTime;
        cinemachineObjetivoPitch -= mouseY * velocidadDeRotacion * Time.deltaTime;

        cinemachineObjetivoPitch = Mathf.Clamp(cinemachineObjetivoPitch, botonClamp, parteSuperior);

        // Calcula la rotaci�n de la c�mara en torno al objetivo
        Quaternion rotation = Quaternion.Euler(cinemachineObjetivoPitch, cinemachineObjetivoYaw, 0f);

        // Calcula la posici�n deseada detr�s del objetivo en el espacio global
        Vector3 offset = new Vector3(0, 0, -distancia); // Mant�n la distancia fija
        Vector3 desiredPosition = sigueAlObjetivo.position + rotation * offset;

        // Ajusta la posici�n y orientaci�n de la c�mara
        camTransform.position = desiredPosition;
        camTransform.LookAt(sigueAlObjetivo); // Asegura que la c�mara mire al objetivo
    }

    private void AjustarPosicionInicial()
    {
        if (sigueAlObjetivo != null && camTransform != null)
        {
            // Ajusta la posici�n inicial de la c�mara detr�s del objetivo
            Vector3 initialOffset = new Vector3(0, 0, -distancia);
            Vector3 initialPosition = sigueAlObjetivo.position + initialOffset;
            camTransform.position = initialPosition;
            camTransform.LookAt(sigueAlObjetivo); // Asegura que la c�mara mire al objetivo al iniciar
        }
    }
}
