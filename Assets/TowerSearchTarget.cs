using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSearchTarget : MonoBehaviour {

    public GameObject currentTarget;

    private List<GameObject> _posibleTargets = new List<GameObject>();

    private void Update() {
        if (_posibleTargets.Count > 0) {
            SearchClosestTarget();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag($"Enemy")) {
            _posibleTargets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform.CompareTag($"Enemy")) {
            _posibleTargets.Remove(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.transform.CompareTag($"Enemy")) {
            
        }
    }

    private void SearchClosestTarget() {

        Transform closestEnemy = _posibleTargets[0].transform;
        foreach (GameObject enemy in _posibleTargets) {
            closestEnemy = GetClosest(closestEnemy, enemy.transform);
        }
        currentTarget = closestEnemy.gameObject;
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
}
