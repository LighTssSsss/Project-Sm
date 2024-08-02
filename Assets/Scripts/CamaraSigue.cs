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
        // Inicializa el transform de la cámara
        camTransform = transform;

        // Ajusta la posición inicial de la cámara detrás del objetivo
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

        // Obtén la entrada del mouse y ajusta la rotación
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        cinemachineObjetivoYaw += mouseX * velocidadDeRotacion * Time.deltaTime;
        cinemachineObjetivoPitch -= mouseY * velocidadDeRotacion * Time.deltaTime;

        cinemachineObjetivoPitch = Mathf.Clamp(cinemachineObjetivoPitch, botonClamp, parteSuperior);

        // Calcula la rotación de la cámara en torno al objetivo
        Quaternion rotation = Quaternion.Euler(cinemachineObjetivoPitch, cinemachineObjetivoYaw, 0f);

        // Calcula la posición deseada detrás del objetivo en el espacio global
        Vector3 offset = new Vector3(0, 0, -distancia); // Mantén la distancia fija
        Vector3 desiredPosition = sigueAlObjetivo.position + rotation * offset;

        // Ajusta la posición y orientación de la cámara
        camTransform.position = desiredPosition;
        camTransform.LookAt(sigueAlObjetivo); // Asegura que la cámara mire al objetivo
    }

    private void AjustarPosicionInicial()
    {
        if (sigueAlObjetivo != null && camTransform != null)
        {
            // Ajusta la posición inicial de la cámara detrás del objetivo
            Vector3 initialOffset = new Vector3(0, 0, -distancia);
            Vector3 initialPosition = sigueAlObjetivo.position + initialOffset;
            camTransform.position = initialPosition;
            camTransform.LookAt(sigueAlObjetivo); // Asegura que la cámara mire al objetivo al iniciar
        }
    }
}
