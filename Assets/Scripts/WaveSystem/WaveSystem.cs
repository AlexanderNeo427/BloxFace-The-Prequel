using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    // Events
    // TODO when everyone compile
    public delegate void SpawnAction();
    public static event SpawnAction OnSpawn;

    public GameObject enemy;
    public int xPos;
    public int zPos;
    public int enemyCount;

    // Where the enemy will want to spawn
    private int oldSpawn;
    private int chosenSpawn = 0;

    // Max Enemy Count. Will increase after a few rounds
    private int enemyCountMax = 10;
    // Rate of enemies spawning in. Will decrease after a few rounds
    private float spawnInterval = 1f;

    private void OnEnable()
    {
        StartCoroutine(SpawnEnemy());
    }
    private void OnDisable()
    {
        if (spawnInterval > 0.1f)
            spawnInterval -= 0.05f;

        if (enemyCountMax > 50)
            enemyCountMax = 50;
        else
            enemyCountMax += (enemyCountMax / 5);

        Debug.Log("Max Enemies: " + enemyCountMax);
        Debug.Log("SpawnInterval: " + spawnInterval);

        enemyCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SpawnEnemy()
    {
        Debug.Log(enemyCountMax);
        
        // Spawn enemies while count less than max
        while (enemyCount < enemyCountMax)
        {
            // Spawn in diff places
            while (chosenSpawn == oldSpawn)
            {
                chosenSpawn = Random.Range(1, 5);
            }
            oldSpawn = chosenSpawn;

            // Left
            if (chosenSpawn == 1)
            {
                xPos = -22;
                zPos = Random.Range(-13, 12);
            }
            // Right
            else if (chosenSpawn == 2)
            {
                xPos = 22;
                zPos = Random.Range(-13, 12);
            }
            // Up
            else if (chosenSpawn == 3)
            {
                zPos = -24;
                xPos = Random.Range(-13, 12);
            }
            // Down
            else
            {
                zPos = 24;
                xPos = Random.Range(-13, 12);
            }

            Instantiate(enemy, new Vector3(xPos, 0.92f, zPos), Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
            enemyCount += 1;
        }
    }
}
