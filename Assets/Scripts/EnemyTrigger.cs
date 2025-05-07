using System;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
     // Reference to the enemy prefab
    public GameObject enemyPrefab;

    // Array of spawn points
    public Transform[] spawnPoints;

    // Whether the trigger should only work once
    public bool oneTimeTrigger = true;

    // Internal flag to track if the trigger has been activated
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player") && (!oneTimeTrigger || !hasTriggered))
        {
            SpawnEnemies();
            hasTriggered = true; // Mark the trigger as activated
        }
    }

    private void SpawnEnemies()
    {
        // Loop through all spawn points and instantiate an enemy at each
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint != null && enemyPrefab != null)
            {
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }
}
