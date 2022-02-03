using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyState {
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
    EnemyState _currentState;

    void Start() {
        
    }

    void Update() {
        
    }
}