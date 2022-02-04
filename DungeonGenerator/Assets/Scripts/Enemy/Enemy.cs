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

    [SerializeField] protected int _health;
    protected string _enemyName;
    [SerializeField] protected int _baseAttack;
    [SerializeField] protected float _moveSpeed;
    public EnemyState _currentState;

    // Empujar
    public void Knock(Rigidbody2D myRigidBody, float knockbackTime) {
        StartCoroutine(KnockCo(myRigidBody, knockbackTime));
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