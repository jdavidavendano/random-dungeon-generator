using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour {

    public int hitPoint;
    public int maxHitPoint;
    public float pushRecoverySpeed = 0.2f;

    // Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    // Push
    protected Vector3 pushDirection;

    // All fighters can ReceiveDamage and Die
    protected virtual void ReceiveDamage(DamageController dmg) {
        if (Time.time - lastImmune > immuneTime) {
            lastImmune = Time.time;
            hitPoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            // GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector2.zero, 0.5f);
            
            if (hitPoint <= 0) {
                hitPoint = 0;
                Death();
            }
        }
    }

    protected virtual void Death() {

    }
}