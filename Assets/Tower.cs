using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    
    [SerializeField] private float attackRange = 30.0f;
    [SerializeField] private float attackRate = 2.0f;
    
    [SerializeField] private ParticleSystem projectileParticle;
    [SerializeField] private Transform towerRotation;
    
    public Transform currentTarget;

    private ParticleSystem.EmissionModule _emissionModule;

    private void Start() {
        _emissionModule = projectileParticle.emission;
        _emissionModule.rateOverTime = attackRate;
    }

    private void Update() {
        SearchTargetEnemy();
        ShotAtEnemy();
    }
    
    private void ShotAtEnemy() {
        if (currentTarget != null) {
            float distanceToEnemy = Vector3.Distance(
                currentTarget.transform.position,
                gameObject.transform.position);
            if (distanceToEnemy <= attackRange) {
                towerRotation.LookAt(currentTarget);
                _emissionModule.enabled = true;
            } else {
                _emissionModule.enabled = false;
            }
        } else {
            _emissionModule.enabled = false;
        }
    }

    private void SearchTargetEnemy() {
        Enemy[] _sceneEnemies = FindObjectsOfType<Enemy>();
        if (_sceneEnemies.Length == 0) {return;}
        
        Transform closestEnemy = _sceneEnemies[0].transform;
        foreach (Enemy enemy in _sceneEnemies) {
            closestEnemy = GetClosest(closestEnemy, enemy.transform);
        }
        currentTarget = closestEnemy;
    }

    private Transform GetClosest(Transform enemy1, Transform enemy2) {
        float distanceToEnemy1 = Vector3.Distance(
            enemy1.transform.position,
            gameObject.transform.position);
        float distanceToEenemy2 = Vector3.Distance(
            enemy2.transform.position,
            gameObject.transform.position);
        if (distanceToEnemy1 > distanceToEenemy2) {
            return enemy2;
        } else {
            return enemy1;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
