using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PhysicalMovement : MonoBehaviour
{
    // Variables
    [Header("Gravity")]
    [SerializeField] private float m_normalScale = 0.5f;
    [SerializeField] private float m_fallingScale = 1.5f;

    private CharacterController m_controller;

    private float m_gravity;
    private Vector3 m_velocity;

    // Methods
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        m_controller = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (m_controller.isGrounded)
        {
            m_gravity = 0;
        }
        else
        {
            float scale = m_controller.velocity.y >= 0 ? m_normalScale : m_fallingScale;
            m_gravity += Physics.gravity.y * scale * Time.deltaTime;
        }

        Vector3 velocity = m_velocity;
        velocity.y += m_gravity;

        m_controller.Move(velocity * Time.deltaTime);
    }

    /// <summary>
    /// Set the velocity of the physics.
    /// </summary>
    public void Move(Vector3 velocity)
    {
        m_velocity = velocity;
    }
}
