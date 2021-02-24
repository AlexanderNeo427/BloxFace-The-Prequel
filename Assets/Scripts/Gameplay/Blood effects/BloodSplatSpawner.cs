using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bloodSplatPrefab;

    private void OnEnable()
    {
        RegularZombie.OnDeath += SpawnBlood;
        SuicideBomberZombie.OnDeath += SpawnBlood;
        RunnerZombie.OnDeath += SpawnBlood;
        BossZombie.OnDeath += SpawnBlood;
    }

    private void OnDisable()
    {
        RegularZombie.OnDeath -= SpawnBlood;
        SuicideBomberZombie.OnDeath -= SpawnBlood;
        RunnerZombie.OnDeath -= SpawnBlood;
        BossZombie.OnDeath -= SpawnBlood;
    }

    private void SpawnBlood(Vector3 pos)
    {
        pos.y = 0.01f;

        GameObject go = Instantiate( bloodSplatPrefab, pos, Quaternion.Euler(90f, 0f, 0f) );

        float randScale = Random.Range(2.5f, 6f);
        go.transform.localScale = new Vector3(randScale, randScale, 0.125f);

        float randRotation = Random.Range(0.1f, 359.9f);
        go.transform.rotation = Quaternion.Euler(0f, randRotation, 0f) * go.transform.rotation;

        Color currColor = go.GetComponent<Renderer>().material.color;
        float randAlpha = Random.Range(100f, 230f);
        currColor.a = randAlpha / 255f;
        go.GetComponent<Renderer>().material.color = currColor;
    }
}
