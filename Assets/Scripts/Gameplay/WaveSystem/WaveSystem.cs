using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    // Events
    // TODO when everyone compile
    public delegate void SpawnAction();
    public static event SpawnAction OnSpawn;

    [SerializeField] private List<GameObject> enemies;
    // public GameObject enemy;
    public static int xPos;
    public static int zPos;
    public static int enemyCount;

    // Where the enemy will want to spawn
    private int oldSpawn;
    private int chosenSpawn = 0;

    public int waveCount = 0;

    // Max Enemy Count. Will increase after a few rounds
    private int enemyCountMax = 0;
    // Rate of enemies spawning in. Will decrease after a few rounds
    private float spawnInterval = 1f;

    private void OnEnable()
    {
        waveCount += 1;

        if (spawnInterval > 0.1f)
            spawnInterval -= 0.05f;

        if (enemyCountMax > 70)
            enemyCountMax = 70;
        else
            enemyCountMax = GetNthFibonacci_Ite(waveCount + 1);

        Debug.Log("Max Enemies: " + enemyCountMax);
        Debug.Log("SpawnInterval: " + spawnInterval);

        enemyCount = 0;

        StartCoroutine(SpawnEnemy());

        Debug.Log("Enemies Remaining: " + GameObject.FindGameObjectsWithTag("Enemy").Length);
    }

    private void OnDisable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount == enemyCountMax)
            gameObject.SetActive(false);
    }

    // Fibonacci Sequence to spawn enemies
    public static int GetNthFibonacci_Ite(int n)
    {
        int number = n - 1; //Need to decrement by 1 since we are starting from 0  
        int[] Fib = new int[number + 1];
        Fib[0] = 0;
        Fib[1] = 1;
        for (int i = 2; i <= number; i++)
        {
            Fib[i] = Fib[i - 2] + Fib[i - 1];
        }
        return Fib[number];
    }

    IEnumerator SpawnEnemy()
    {
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

            int randIdx = Random.Range( 0, enemies.Count );
            Instantiate(enemies[randIdx], new Vector3(xPos, 0.92f, zPos), Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
            enemyCount += 1;
        }
    }
}
