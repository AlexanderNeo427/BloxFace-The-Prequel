using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    // Wave State
    enum WaveState
    {
        Spawning,
        Updating,
        Waiting
    };

    private WaveState State = WaveState.Waiting;

    // Bonus Score event
    public delegate void BonusEvent();
    public static event BonusEvent BonusScore;

    // Enemy List
    [SerializeField] private List<GameObject> enemies;

    // Spawn Position
    public static int xPos;
    public static int zPos;
    private int oldSpawn;
    private int chosenSpawn = 0;

    // Wave Count
    public int waveCount = 0;
    
    // Enemy Counts
    private int enemySpawnedMax = 0;
    public static int enemySpawned;
    public int enemyAlive;

    // Spawn Rate
    private float spawnInterval = 0.05f;

    // Timer
    public float TimeCount = 5f;

    // Bonus Threshold (extra points for clearing enemies fast)
    private bool ThresholdAchieved = false;

    // Update is called once per frame
    void Update()
    {
        enemyAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        TimeCount -= Time.deltaTime;

        // State
        // Spawning of enemies
        if (State == WaveState.Spawning)
        {
            if (enemySpawnedMax >= 55)
                enemySpawnedMax = 70;
            else
                enemySpawnedMax = GetNthFibonacci_Ite(waveCount + 1);

            enemySpawned = 0;

            StartCoroutine(SpawnEnemy());

            State = WaveState.Updating;
        }

        // Delay while waiting for all enemies to spawn in
        else if (State == WaveState.Updating)
        {
            // Wait for all enemies to spawn in
            if (enemySpawned == enemySpawnedMax)
            {
                State = WaveState.Waiting;
            }
        }

        // Check whether its time to spawn or not
        else if (State == WaveState.Waiting)
        {
            // If player kills all the enemy within the time threshold
            if (TimeCount >= 15f)
            {
                if (enemyAlive == 0)
                {
                    ThresholdAchieved = true;
                    BonusScore?.Invoke();
                    TimeCount = 5f;
                }
            }

            // If player kills all the enemy before 5 seconds
            if (TimeCount > 5f && TimeCount < 15f)
            {
                if (enemyAlive == 0)
                {
                    ThresholdAchieved = false;
                    TimeCount = 5f;
                }
            }

            // Once timer runs out, change state, add wave counter accordingly
            if (TimeCount <= 0f)
            {
                if (ThresholdAchieved)
                {
                    waveCount += 2;
                }
                else
                {
                    waveCount += 1;
                }

                TimeCount = 30f;
                State = WaveState.Spawning;
            }
        }
    }

    // Fibonacci Sequence to spawn enemies
    public static int GetNthFibonacci_Ite(int n)
    {
        // Need to decrement by 1 to follow the actual start of fibonacci sequence 
        int number = n - 1; 
        int[] Fib = new int[number + 1];
        Fib[0] = 0;
        Fib[1] = 1;
        for (int i = 2; i <= number; i++)
        {
            Fib[i] = Fib[i - 2] + Fib[i - 1];
        }
        return Fib[number];
    }

    // Spawn Enemies
    IEnumerator SpawnEnemy()
    {
        // Spawn enemies while count less than max
        while (enemySpawned < enemySpawnedMax)
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
            enemySpawned += 1;
        }
    }
}
