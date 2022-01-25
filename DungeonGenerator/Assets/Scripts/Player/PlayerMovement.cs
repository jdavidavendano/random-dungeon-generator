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

        _animator.SetFloat("Horizontal", _movement.x);
        _animator.SetFloat("Vertical", _movement.y);
        _animator.SetFloat("Speed", _movement.sqrMagnitude); // Raíz cuadrada de la magnitud del vector de movimiento

        if (Input.GetMouseButtonDown(0)) {
            StartCoroutine(AttackCo());
        }
    }

    // Como el framerate puede cambiar, se manejan las físicas aquí
    // Ejecutado en un tiempo no encapsulado por el framerate (50 veces por segundo)
    void FixedUpdate() {
        // Movement
        _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator AttackCo() {
        _animator.SetBool("Attacking", true);
        // yield return null;
        yield return new WaitForSeconds(0.05f);
        Debug.Log("a");
        _animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(0.05f);
        Debug.Log("b");
    }
}