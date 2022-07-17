using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProperties : MonoBehaviour {

    [HideInInspector] public float bulletDamage;
    [SerializeField] private bool isAoEDamage = false;
    [SerializeField] private float aoeRange = 10.0f;
    [SerializeField] private ParticleSystem hitParticle;
    
    private void OnParticleCollision(GameObject other) {
        if (other.GetComponent<Enemy>()) {
            var tmphitParticle = Instantiate(hitParticle, other.transform);
            tmphitParticle.transform.parent = null;
            Destroy(tmphitParticle.gameObject, 5.0f);    
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(bulletDamage);
            if (isAoEDamage) {
                Enemy[] enemies = FindObjectsOfType<Enemy>();
                foreach (Enemy enemyInScene in enemies) {
                    if (enemyInScene != null && other != null) {
                        float distanceToEnemy = Vector3.Distance(
                            enemyInScene.transform.position
                            ,other.transform.position);
                        if (distanceToEnemy <= aoeRange) {
                            enemyInScene.TakeDamage(bulletDamage/2);
                        }                        
                    }
                }
            }
        }
    }
}
