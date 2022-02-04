using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Diferenciaremos los estados en los que puede estar el jugador
public enum PlayerState
{
    idle,
    walk,
    attack,
    stagger
}

public class PlayerMovement : MonoBehaviour
{

    public PlayerState _currentState;
    [SerializeField] private float _moveSpeed = 12f;
    private Rigidbody2D _rigidBody;
    private Vector2 _movement;
    private Animator _animator;
    private bool _isAttacking;
    public FloatValue _currentHealth;
    public Signal _playerHealthSignal;

    void Start()
    {
        _currentState = PlayerState.walk;
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();

        // Para que al empezar el juego se inicialicen valores de acuerdo a la dirección del jugador
        // y no se activen los cuatro colliders al atacar sin haberse movido.
        _animator.SetFloat("Horizontal", 0);
        _animator.SetFloat("Vertical", -1);

        _isAttacking = false;
    }

    void Update()
    {
        // Input del movimiento
        _movement = Vector3.zero;
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        // Input del ataque
        if (Input.GetMouseButtonDown(0) && _currentState != PlayerState.attack && _currentState != PlayerState.stagger)
        {
            _isAttacking = true;
            StartCoroutine(AttackCo());
        }
    }

    // Como el framerate puede cambiar, se manejan las físicas aquí
    // Ejecutado en un tiempo no encapsulado por el framerate (50 veces por segundo)
    void FixedUpdate()
    {
        if ((_currentState == PlayerState.walk || _currentState == PlayerState.idle) && _isAttacking == false)
        {
            UpdateAnimationAndMove();
        }
    }

    void UpdateAnimationAndMove()
    {
        // Movimiento
        if (_movement != Vector2.zero)
        {
            MoveCharacter();
            // Ejecutar las animaciones
            _animator.SetFloat("Horizontal", _movement.x);
            _animator.SetFloat("Vertical", _movement.y);
            _animator.SetBool("Moving", true);
        }
        else
        {
            _animator.SetBool("Moving", false);
        }
    }

    void MoveCharacter()
    {
        _movement.Normalize();
        _rigidBody.velocity = Vector2.zero;
        _rigidBody.MovePosition(_rigidBody.position + _movement * _moveSpeed * Time.fixedDeltaTime);
    }

    // Empujar
    public void Knock(float knockbackTime, float damage)
    {
        _currentHealth._runTimeValue -= damage; // Hacer daño
        _playerHealthSignal.Raise();
        // Solo se ejecuta si el jugador no ha muerto
        if (_currentHealth._runTimeValue > 0)
        {
            StartCoroutine(KnockCo(knockbackTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator AttackCo()
    {
        _animator.SetBool("Attacking", true); // Ejecutar la animación
        _currentState = PlayerState.attack;

        yield return null; // Introduce un pequeño delay en la acción

        _animator.SetBool("Attacking", false); // Dejar de ejecutar a animación

        yield return new WaitForSeconds(0.3f); // Esperar mientras termina la animación
        _currentState = PlayerState.walk;
        _isAttacking = false;
    }

    // Ejecutar el empuje
    IEnumerator KnockCo(float knockbackTime)
    {
        if (_rigidBody != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            _currentState = PlayerState.idle;
            _rigidBody.velocity = Vector2.zero;
        }
    }
}