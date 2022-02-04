using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEnemy : Enemy {

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
    }

    // Se usa para las físicas
    void FixedUpdate() {
        CheckDistance(); // Como CheckDistance trabaja con el rigidbody (físicas) se pone aquí en vez de en Update
    }

    void CheckDistance() {
        if(_currentState == EnemyState.stagger) {
            return;
        }

        // if(Vector3.Distance(_target.position, _rigidBody.position) <= _chaseRadius 
        //     && Vector3.Distance(_target.position, _rigidBody.position) > _attackRadius
        //     && (_currentState == EnemyState.idle || _currentState == EnemyState.walk && _currentState != EnemyState.stagger)
        //     && _isCollidingWithEnemy == false) {
        //     // Vector3 temp = Vector3.MoveTowards(transform.position, _target.position, _moveSpeed * Time.deltaTime);
        //     Vector2 temp = Vector2.MoveTowards(_rigidBody.position, _target.position, _moveSpeed * Time.deltaTime);

        //     ChangeAnim(temp - _rigidBody.position); // cambiar la dirección de las animaciones
        //     _rigidBody.MovePosition(temp); // mover al enemigo

        //     ChangeState(EnemyState.walk);
        //     _animator.SetBool("WakeUp", true); // ejecutar la animación de despertar
        // }
        // // Solo se debe dormir cuando está lejos del jugador
        // else if(Vector3.Distance(_target.position, _rigidBody.position) > _chaseRadius) {
        //     _animator.SetBool("WakeUp", false); // ejecutar la animación de volver a dormir
        //     _rigidBody.velocity = Vector2.zero;
        //     ChangeState(EnemyState.idle);
        // }

        Vector3 vectorToTarget = _target.position - _rigidBody.position; // vector de dirección al player
        float distanceToTarget = vectorToTarget.magnitude; // distancia al player

        if(distanceToTarget > _chaseRadius) {
            // Idle
            ChangeState(EnemyState.idle);
            _animator.SetBool("WakeUp", false); // ejecutar la animación de volver a dormir
            _rigidBody.velocity = Vector2.zero; // se detiene cuando está idle
        }
        else if(distanceToTarget <= _attackRadius) {
            // Atacar
            ChangeState(EnemyState.attack);
            _rigidBody.velocity = Vector2.zero; // se detiene cuando está atacando
        }
        else {
            // Perseguir al player
            ChangeState(EnemyState.walk);
            Vector2 temp = Vector2.MoveTowards(_rigidBody.position, _target.position, _moveSpeed * Time.deltaTime);
            _rigidBody.MovePosition(temp); // mover al enemigo
            ChangeAnim(temp - _rigidBody.position); // cambiar la dirección de las animaciones
            
            _animator.SetBool("WakeUp", true); // ejecutar la animación de despertar
        }
    }

    // Cambia la dirección de las animaciones del enemigo dependiendo de hacia qué dirección se mueve
    void ChangeAnim(Vector2 direction) {
        direction = direction.normalized;
        _animator.SetFloat("Horizontal", direction.x);
        _animator.SetFloat("Vertical", direction.y);
    }

    // Cambia el estado del enemigo
    void ChangeState(EnemyState newState) {
        if(_currentState != newState) {
            _currentState = newState;
        }
    }

    void OnDrawGizmos() {
        if(_rigidBody == null) {
            return;
        }
        Gizmos.DrawWireSphere(_rigidBody.position, _attackRadius);
    }
}