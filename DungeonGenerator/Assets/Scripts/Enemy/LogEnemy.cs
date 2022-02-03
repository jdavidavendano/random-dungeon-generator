using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEnemy : Enemy {

    private Rigidbody2D _rigidBody;
    private Transform target;
    [SerializeField] private float chaseRadius;
    [SerializeField] private float attackRadius;
    private Transform _homePosition;
    // private BoxCollider2D _boxCollider;
    private Animator _animator;

    void Start() {
        _rigidBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        _animator = GetComponent<Animator>();
        // _boxCollider = GetComponent<BoxCollider2D>();
    }


    void Update() {
        
    }

    // Se usa para las físicas
    void FixedUpdate() {
        // Como CheckDistance trabaja con el rigidbody (físicas) se pone aquí en vez de en Update
        CheckDistance();
    }

    void CheckDistance() {
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius) {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);
            _rigidBody.MovePosition(temp);
        }
        else {
            // _boxCollider.size = new Vector2();
            Debug.Log("Salió");
        }
    }
}