using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour {

    public EnemyState _currentState;
    [SerializeField] protected FloatValue _maxHealth;
    protected float _health;
    protected string _enemyName;
    [SerializeField] protected int _baseAttack;
    [SerializeField] protected float _moveSpeed;

    void Awake() {
        _health = _maxHealth._initialValue; // setear la vida inicial
    }

    void TakeDamage(float damage) {
        _health -= damage; // quitarle vida al enemigo

        // Verificar si el enemigo murió
        if(_health <= 0) {
            if(this.gameObject.name == "OgreBoss") { // Reproducir el sonido de muerte del boss
                SoundController.PlaySound("BossDeath");
            }
            this.gameObject.SetActive(false); // desactivar al enemigo
        }
    }

    // Empujar
    public void Knock(Rigidbody2D myRigidBody, float knockbackTime, float damage) {
        StartCoroutine(KnockCo(myRigidBody, knockbackTime));
        TakeDamage(damage);
    }

    // Ejecutar el empuje
    IEnumerator KnockCo(Rigidbody2D myRigidBody, float knockbackTime) {
        if(myRigidBody != null) {
            yield return new WaitForSeconds(knockbackTime);
            _currentState = EnemyState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }
}