using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEnemy : Enemy
{

    private Rigidbody2D _rigidBody;
    private Rigidbody2D _target;
    [SerializeField] private float _chaseRadius;
    [SerializeField] private float _attackRadius;
    private Animator _animator;

    private Vector3 lastPosition;

    private int lastDirection;

    private float timeSleeping = 0f;

    private float timeAwakeMandatory = 10f;

    private float goToBed()
    {
        if (timeAwakeMandatory < 0)
        {
            int isSleepingRandom = Random.Range(0, 9);

            if (isSleepingRandom < 1)
            {
                return Random.Range(10, 30);
            }
        }

        return 0;
    }

    private List<Vector2> directions = new List<Vector2> {
        new Vector2(0, 1f),
        new Vector2(0, -1f),
        new Vector2(-1f, 0),
        new Vector2(1f, 0),
        new Vector2(1f, 1f),
        new Vector2(-1f, 1f),
        new Vector2(1f, -1f),
        new Vector2(-1f, -1f)
    };

    private int getRandomDirection()
    {
        int newDirection = Random.Range(0, directions.Count);
        while (newDirection == lastDirection)
        {
            newDirection = Random.Range(0, directions.Count);
        }
        return newDirection;
    }

    void Start()
    {
        _currentState = EnemyState.idle;
        _rigidBody = GetComponent<Rigidbody2D>();
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Se usa para las físicas
    void FixedUpdate()
    {
        CheckDistance(); // Como CheckDistance trabaja con el rigidbody (físicas) se pone aquí en vez de en Update
    }

    void CheckDistance()
    {
        if (_currentState == EnemyState.stagger)
        {
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

        if (distanceToTarget > _chaseRadius)
        {
            // Idle
            if (timeSleeping > 0)
            {
                timeSleeping -= Time.deltaTime;
                // Idle stay
                ChangeState(EnemyState.idle);
                _animator.SetBool("WakeUp", false); // ejecutar la animación de volver a dormir
                _rigidBody.velocity = Vector2.zero; // se detiene cuando está idle
            }
            else
            {
                // Idle Walk 

                Vector2 temp = Vector2.MoveTowards(_rigidBody.position, _rigidBody.position + directions[lastDirection], _moveSpeed * Time.deltaTime);
                ChangeState(EnemyState.walk);
                _animator.SetBool("WakeUp", true); // ejecutar la animación de dejar de dormir

                _rigidBody.MovePosition(temp); // mover al enemigo
                ChangeAnim(temp - _rigidBody.position); // cambiar la dirección de las animaciones

                if (lastDirection < 4)
                {
                    if (lastPosition == transform.position)
                    {
                        lastDirection = getRandomDirection();
                        timeSleeping = goToBed();
                    }
                }
                else if (lastPosition.x == transform.position.x || lastPosition.y == transform.position.y)
                {

                    lastDirection = getRandomDirection();
                    timeSleeping = goToBed();
                }
            }

            if (timeAwakeMandatory > 0)
            {
                timeAwakeMandatory -= Time.deltaTime;
            }
            if (timeSleeping < 0)
            {
                timeAwakeMandatory = 10f;
            }
            lastPosition = _rigidBody.position;
            //transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 1), enemyVelocity * Time.deltaTime);



        }
        else if (distanceToTarget <= _attackRadius)
        {
            // Atacar
            ChangeState(EnemyState.attack);
            _rigidBody.velocity = Vector2.zero; // se detiene cuando está atacando
        }
        else
        {
            // Perseguir al player
            ChangeState(EnemyState.walk);
            Vector2 temp = Vector2.MoveTowards(_rigidBody.position, _target.position, _moveSpeed * Time.deltaTime);
            _rigidBody.MovePosition(temp); // mover al enemigo
            ChangeAnim(temp - _rigidBody.position); // cambiar la dirección de las animaciones

            _animator.SetBool("WakeUp", true); // ejecutar la animación de despertar
        }

    }

    // Cambia la dirección de las animaciones del enemigo dependiendo de hacia qué dirección se mueve
    void ChangeAnim(Vector2 direction)
    {
        direction = direction.normalized;
        _animator.SetFloat("Horizontal", direction.x);
        _animator.SetFloat("Vertical", direction.y);
    }

    // Cambia el estado del enemigo
    void ChangeState(EnemyState newState)
    {
        if (_currentState != newState)
        {
            _currentState = newState;
        }
    }

    void OnDrawGizmos()
    {
        if (_rigidBody == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(_rigidBody.position, _attackRadius);
    }
}