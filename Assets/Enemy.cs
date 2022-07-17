using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health = 10.0f;

    private void OnParticleCollision(GameObject other) {
        if (other.GetComponent<BulletProperties>()) {
            Debug.Log("hola");
        }
        health--;
        if (health <= 0) {
            Die();    
        }
    }

    private void Die() {
        Destroy(this.gameObject);
    }
}
