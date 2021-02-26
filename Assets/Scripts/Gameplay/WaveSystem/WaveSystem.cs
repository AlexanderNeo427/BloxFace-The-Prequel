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

    public GridManager myGrid;

    private WaveState State = WaveState.Waiting;

    // Bonus Score event
    public delegate void BonusEvent();
    public static event BonusEvent BonusScore;

    // Enemy List
    [SerializeField] private List<GameObject> enemies;

    // Spawn Position
    public static float xPos;
    public static float zPos;
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
    private float ResetTimer = 30f;

    // Bonus Threshold (extra points for clearing enemies fast)
    private bool ThresholdAchieved = false;

    // Different Guns that spawn per phase
    [SerializeField] private GameObject Shotgun;
    [SerializeField] private GameObject Sniper;
    [SerializeField] private GameObject MiniGun;

    // Milestones phases
    // Refers to the different waves that can spawn different enemies and stuff. 
    // refers to wave 1 - 2, wave 3 - 5, wave 6 - 9, wave 10 - infinity
    private bool Phase1 = true;
    private bool Phase2 = false;
    private bool Phase3 = false;
    private bool Phase4 = false;

    // Counter to spawn new teammates in the map
    [SerializeField] private GameObject Teammate;
    private int TurnToSpawn = 3;

    // Update is called once per frame
    void Update()
    {
        enemyAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        TimeCount -= Time.deltaTime;

        // State
        // Spawning of enemies
        if (State == WaveState.Spawning)
        {
            if (enemySpawnedMax >= 50)
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

                TimeCount = ResetTimer;
                State = WaveState.Spawning;
            }
        }

        if (waveCount >= TurnToSpawn)
        {
            Instantiate(Teammate, WaypointManager.Instance.GetRandomWaypoint(), Quaternion.identity);
            TurnToSpawn += 3;
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

        // Add arbitrary value of 5 just so that it doesnt start at 1
        return Fib[number] + 5;
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
                xPos = myGrid.m_grid.m_botLeft.x - 20;
                zPos = Random.Range(-13, 12);
            }
            // Right
            else if (chosenSpawn == 2)
            {
                xPos = myGrid.m_grid.m_topRight.x + 20;
                zPos = Random.Range(-13, 12);
            }
            // Up
            else if (chosenSpawn == 3)
            {
                zPos = myGrid.m_grid.m_topRight.z + 20;
                xPos = Random.Range(-13, 12);
            }
            // Down
            else
            {
                zPos = myGrid.m_grid.m_botLeft.z - 20; 
                xPos = Random.Range(-13, 12);
            }

            int randIdx = DetermineEnemy();
            Instantiate(enemies[randIdx], new Vector3(xPos, 0.92f, zPos), Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
            enemySpawned += 1;
        }
    }

    // Determine what enemies can spawn depending on which round it is
    private int DetermineEnemy()
    {
        Vector3 spawnPos = WaypointManager.Instance.GetRandomWaypoint();
        spawnPos += new Vector3(0f, 1f, 0f);

        if (waveCount < 3)
        {
            return 0;
        }
        else if (waveCount < 6)
        {
            if (Phase2 == false)
            {
                Instantiate(Shotgun, spawnPos, Quaternion.identity);
                ResetTimer = 60f;
                spawnInterval = 0.15f;
                Phase2 = true;
            }

            return Random.Range(0, enemies.Count - 2);
        }
        else if (waveCount < 10)
        {
            if (Phase3 == false)
            {
                Instantiate(Sniper, spawnPos, Quaternion.identity);
                ResetTimer = 120f;
                spawnInterval = 0.30f;
                Phase3 = true;
            }

            return Random.Range(0, enemies.Count - 1);
        }
        else
        {
            if (Phase4 == false)
            {
                Instantiate(MiniGun, spawnPos, Quaternion.identity);
                ResetTimer = 240f;
                spawnInterval = 0.45f;
                Phase4 = true;
            }

            return Random.Range(0, enemies.Count);
        }
    }
}
