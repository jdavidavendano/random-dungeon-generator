using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreBoss : Enemy {

    private Rigidbody2D _rigidBody;
    private Rigidbody2D _target;
    [SerializeField] private float _chaseRadius;
    [SerializeField] private float _attackRadius;
    private Animator _animator;

    void Start() {
        _currentState = EnemyState.idle;
        _rigidBody = GetComponent<Rigidbody2D>();
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();

        // Para que al empezar el juego se inicialicen valores de acuerdo a la dirección del boss
        // y no se activen los cuatro colliders al atacar sin haberse movido.
        _animator.SetFloat("Horizontal", 0);
        _animator.SetFloat("Vertical", -1);
    }

    // Se usa para las físicas
    void FixedUpdate() {
        CheckDistance(); // Como CheckDistance trabaja con el rigidbody (físicas) se pone aquí en vez de en Update
    }

    void CheckDistance() {
        if(_currentState == EnemyState.stagger) {
            return;
        }

        Vector3 vectorToTarget = _target.position - _rigidBody.position; // vector de dirección al player
        float distanceToTarget = vectorToTarget.magnitude; // distancia al player

        if(distanceToTarget > _chaseRadius) {
            // Idle
            ChangeState(EnemyState.idle);
            _animator.SetBool("Moving", false);
            _rigidBody.velocity = Vector2.zero; // se detiene cuando está idle
        }
        else if(distanceToTarget <= _attackRadius) {
            // Atacar
            ChangeState(EnemyState.attack);
            _rigidBody.velocity = Vector2.zero; // se detiene cuando está atacando
            StartCoroutine(AttackCo());
        }
        else {
            // Perseguir al player
            ChangeState(EnemyState.walk);
            Vector2 temp = Vector2.MoveTowards(_rigidBody.position, _target.position, _moveSpeed * Time.deltaTime);
            _rigidBody.MovePosition(temp); // mover al enemigo
            ChangeAnim(temp - _rigidBody.position); // cambiar la dirección de las animaciones
        }
    }

    // Cambia la dirección de las animaciones del enemigo dependiendo de hacia qué dirección se mueve
    void ChangeAnim(Vector2 direction) {
        direction = direction.normalized;
        _animator.SetBool("Moving", true);
        _animator.SetFloat("Horizontal", direction.x);
        _animator.SetFloat("Vertical", direction.y);
    }

    // Cambia el estado del enemigo
    void ChangeState(EnemyState newState) {
        if(_currentState != newState) {
            _currentState = newState;
        }
    }

    private IEnumerator AttackCo()
    {
        _animator.SetBool("Attacking", true); // Ejecutar la animación
        _currentState = EnemyState.attack;

        yield return null; // Introduce un pequeño delay en la acción

        _animator.SetBool("Attacking", false); // Dejar de ejecutar a animación

        yield return new WaitForSeconds(0.3f); // Esperar mientras termina la animación
        _currentState = EnemyState.walk;
    }

    void OnDrawGizmos() {
        if(_rigidBody == null) {
            return;
        }
        Gizmos.DrawWireSphere(_rigidBody.position, _attackRadius);
    }
}