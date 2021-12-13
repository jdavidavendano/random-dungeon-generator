using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float m_moveSpeed = 5f;
    [SerializeField] private Rigidbody2D m_rb;
    [SerializeField] Animator m_animator;
    private Vector2 m_movement;

    void Update() {
        // Input
        // Diagonal movement
        // m_movement.x = Input.GetAxisRaw("Horizontal");
        // m_movement.y = Input.GetAxisRaw("Vertical");

        // No diagonal movement
        if (m_movement.y == 0) {
            m_movement.x = Input.GetAxisRaw("Horizontal");
        }

        if (m_movement.x == 0) {
            m_movement.y = Input.GetAxisRaw("Vertical");
        }

        m_movement.Normalize();

        if (m_movement.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (m_movement.x > 0) {
            transform.localScale = Vector3.one;
        }

        m_animator.SetFloat("Horizontal", m_movement.x);
        m_animator.SetFloat("Vertical", m_movement.y);
        m_animator.SetFloat("Speed", m_movement.sqrMagnitude);
    }

    void FixedUpdate() {
        // Movement
        m_rb.MovePosition(m_rb.position + m_movement * m_moveSpeed * Time.fixedDeltaTime);
    }
}
