using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health = 10.0f;
    
    public void TakeDamage(float incomingDamage) {
        health -= incomingDamage;
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        Destroy(this.gameObject);
    }
}
