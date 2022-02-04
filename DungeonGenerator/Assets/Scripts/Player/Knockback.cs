using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {

    [SerializeField] private float _thrust;
    [SerializeField] private float _knockbackTime;

    void OnTriggerEnter2D(Collider2D other) {
        // CircleCollider2D enemy_circle = ;
        //  && other.GetType() == CircleCollider2D
        if(other.gameObject.CompareTag("Enemy")) {
            Rigidbody2D enemy_rb = other.GetComponent<Rigidbody2D>();

            if(enemy_rb != null) {
                enemy_rb.GetComponent<Enemy>()._currentState = EnemyState.stagger;
                Vector2 difference = enemy_rb.transform.position - transform.position;
                difference = difference.normalized * _thrust;
                enemy_rb.velocity = Vector2.zero;
                enemy_rb.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo(enemy_rb));
            }
        }
    }

    IEnumerator KnockCo(Rigidbody2D enemy_rb) {
        if(enemy_rb != null) {
            yield return new WaitForSeconds(_knockbackTime);
            enemy_rb.velocity = Vector2.zero;
            enemy_rb.GetComponent<Enemy>()._currentState = EnemyState.idle;
        }
    }
}