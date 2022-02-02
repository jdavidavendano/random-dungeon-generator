using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {

    [SerializeField] private float _thrust;

    void Start() {
        
    }

    void Update() {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        // CircleCollider2D enemy_circle = ;
        //  && other.GetType() == CircleCollider2D
        Debug.Log("other: " + (other.GetType()).GetType());
        if(other.gameObject.CompareTag("Enemy")) {
            Rigidbody2D enemy_rb = other.GetComponent<Rigidbody2D>();
            if(enemy_rb != null) {
                enemy_rb.isKinematic = false;
                Vector2 difference = enemy_rb.transform.position - transform.position;
                difference = difference.normalized * _thrust;
                enemy_rb.AddForce(difference, ForceMode2D.Impulse);
                enemy_rb.isKinematic = true;
            }
        }
    }
}