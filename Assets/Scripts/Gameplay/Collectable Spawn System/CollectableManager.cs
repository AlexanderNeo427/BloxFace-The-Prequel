using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : Singleton<CollectableManager>
{
    [Header("Customisations")]
    [SerializeField][Range (0.0f, 100f)][Tooltip("Chance of spawning pickups (after every zombie death)")]
    private float spawnChance = 20f;

    [Header ("References")]
    [SerializeField] private GameObject collectablePrefab;

    private void OnEnable()
    {
        // Subscribe to events here
        RegularZombie.OnDeath += SpawnCollectable;
        SuicideBomberZombie.OnDeath += SpawnCollectable;
        RegularZombie.OnDeath += SpawnCollectable;
    }

    private void OnDisable()
    {
        // Unsubscribe from events here
        RegularZombie.OnDeath -= SpawnCollectable;
        SuicideBomberZombie.OnDeath -= SpawnCollectable;
        RegularZombie.OnDeath -= SpawnCollectable;
    }

    private void SpawnCollectable(Vector3 pos)
    {
        Vector3 spawnPos = new Vector3(pos.x, 0.4f, pos.z);
        float randNum = Random.Range(0f, 100f);

        if (randNum <= spawnChance)
        {
            GameObject collectable = Instantiate( collectablePrefab, spawnPos, Quaternion.identity );
        }
    }
}
