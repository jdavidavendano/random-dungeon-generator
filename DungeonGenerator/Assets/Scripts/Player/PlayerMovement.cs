using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float _moveSpeed = 5f;
    private Rigidbody2D _rb;
    private Animator _animator;
    private Vector2 _movement;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    void Update() {
        // Input
        // Diagonal movement
        _movement.x = Input.GetAxisRaw("Horizontal") * _moveSpeed * Time.deltaTime;
        _movement.y = Input.GetAxisRaw("Vertical") * _moveSpeed * Time.deltaTime;

        // No diagonal movement
        if (_movement.y == 0) {
            _movement.x = Input.GetAxisRaw("Horizontal") * _moveSpeed * Time.deltaTime;
        }

        if (_movement.x == 0) {
            _movement.y = Input.GetAxisRaw("Vertical") * _moveSpeed * Time.deltaTime;
        }

        _movement.Normalize();

        if (_movement.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_movement.x > 0) {
            transform.localScale = Vector3.one;
        }

        _animator.SetFloat("Speed", _movement.sqrMagnitude);
        // m_animator.SetFloat("Horizontal", m_movement.x);
        // m_animator.SetFloat("Vertical", m_movement.y);

    }

    // Como el framerate puede cambiar, se manejan las físicas aquí
    // Ejecutado en un tiempo no encapsulado por el framerate (50 veces por segundo)
    void FixedUpdate() {
        // Movement
        _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
    }
}