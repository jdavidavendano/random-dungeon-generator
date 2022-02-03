using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Diferenciaremos los estados en los que puede estar el jugador
enum PlayerState {
    walk,
    attack
}

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float _moveSpeed = 12f;
    private PlayerState _currentState;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private Vector2 _movement;
    private bool _isAttacking;

    void Start() {
        _currentState = PlayerState.walk;
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();

        // Para que al empezar el juego se inicialicen valores de acuerdo a la dirección del jugador
        // y no se activen los cuatro colliders al atacar sin haberse movido.
        _animator.SetFloat("Horizontal", 0);
        _animator.SetFloat("Vertical", -1);

        _isAttacking = false;
    }
    
    void Update() {
        // Input del movimiento
        _movement = Vector3.zero;
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        // Input del ataque
        if (Input.GetMouseButtonDown(0) && _currentState != PlayerState.attack) {
            _isAttacking = true;
            StartCoroutine(AttackCo());
        }
    }

    // Como el framerate puede cambiar, se manejan las físicas aquí
    // Ejecutado en un tiempo no encapsulado por el framerate (50 veces por segundo)
    void FixedUpdate() {
        if (_currentState == PlayerState.walk && _isAttacking == false) {
            UpdateAnimationAndMove();
        }
    }

    void UpdateAnimationAndMove() {
        // Movimiento
        if (_movement != Vector2.zero) {
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
        _movement.Normalize();
        _rigidBody.MovePosition(_rigidBody.position + _movement * _moveSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator AttackCo() {
        _animator.SetBool("Attacking", true);
        _currentState = PlayerState.attack;

        yield return null; // Introduce un pequeño delay en la acción
        
        _animator.SetBool("Attacking", false);

        yield return new WaitForSeconds(0.3f); // Esperar mientras termina la animación
        _currentState = PlayerState.walk;
        _isAttacking = false;
    }
}