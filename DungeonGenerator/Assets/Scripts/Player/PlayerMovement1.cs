using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerState {
    walk,
    attack
}

public class PlayerMovement1 : MonoBehaviour {

    [SerializeField] private float _moveSpeed = 5f;
    private PlayerState _currentState;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private Vector3 _movement;

    void Start() {
        _currentState = PlayerState.walk;
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    void Update() {
        // Input
        if (Input.GetMouseButtonDown(0) && _currentState != PlayerState.attack) {
            Debug.Log("click");
            StartCoroutine(AttackCo());
        }
        else if (_currentState == PlayerState.walk) {
            UpdateAnimationAndMove();
        }
    }

    // Como el framerate puede cambiar, se manejan las físicas aquí
    // Ejecutado en un tiempo no encapsulado por el framerate (50 veces por segundo)
    void FixedUpdate() {
        // Input del movimiento
        _movement = Vector3.zero;
        _movement.x = Input.GetAxisRaw("Horizontal") * _moveSpeed * Time.fixedDeltaTime;
        _movement.y = Input.GetAxisRaw("Vertical") * _moveSpeed * Time.fixedDeltaTime;
        _movement.Normalize();
    }

    void UpdateAnimationAndMove() {
        // Movimiento
        if (_movement != Vector3.zero) {
            MoveCharacter();

            _animator.SetFloat("Horizontal", _movement.x);
            _animator.SetFloat("Vertical", _movement.y);
            _animator.SetBool("Moving", true);
        }
        else {
            _animator.SetBool("Moving", false);
        }
    }

    void MoveCharacter() {
        _rigidBody.MovePosition(transform.position + _movement * _moveSpeed * Time.deltaTime);
    }

    private IEnumerator AttackCo() {
        _animator.SetBool("Attacking", true);
        _currentState = PlayerState.attack;

        yield return null; // Introduce un pequeño delay en la acción
        
        _animator.SetBool("Attacking", false);

        yield return new WaitForSeconds(0.3f); // Esperar mientras termina la animación
        _currentState = PlayerState.walk;
    }
}