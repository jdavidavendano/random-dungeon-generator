using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {

    [SerializeField] private float _thrust;
    [SerializeField] private float _knockbackTime;
    [SerializeField] private float _damage;

    void OnTriggerEnter2D(Collider2D other) {
        // Objetos que se pueden romper
        // if(other.gameObject.CompareTag("Breakable") && this.gameObject.CompareTag("Player")) {
        //     other.GetComponent<>();
        // }

        // Verificar que o que golpea la hitbox es un enemigo o player
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player")) {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();

            if(hit != null) {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * _thrust;
                hit.velocity = Vector2.zero;
                hit.AddForce(difference, ForceMode2D.Impulse);

                // Ejecución para jugador golpeando a enemigo
                if(other.gameObject.CompareTag("Enemy") && other.isTrigger) {
                    hit.GetComponent<Enemy>()._currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, _knockbackTime, _damage);

                    // Reproducir sonido para los tipos de enemigos
                    if(other.gameObject.name == "LogEnemy(Clone)") {
                        SoundController.PlaySound("LogHit");
                    }
                    else {
                        SoundController.PlaySound("BossHit");
                    }
                }
                // Ejecución para enemigo golpeando a jugador
                if(other.gameObject.CompareTag("Player")) {
                    if(other.GetComponent<PlayerController>()._currentState != PlayerState.stagger) {
                        hit.GetComponent<PlayerController>()._currentState = PlayerState.stagger;
                        other.GetComponent<PlayerController>().Knock(_knockbackTime, _damage);
                        // Debug.Log("acá?");

                        SoundController.PlaySound("PlayerHit"); // reproducir sonido de jugador siendo golpeado
                    }
                }
            }
        }
    }
}