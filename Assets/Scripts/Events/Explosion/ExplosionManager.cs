using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Responsible for spawning explosions in response to events
 *  Attach this script to an empty GameObject
 */
public class ExplosionManager : Singleton<ExplosionManager>
{
    [SerializeField] private GameObject explosionPrefab;

    // Pooling for better performance
    private List<GameObject> m_explosionPrefabs;

    /*
     * Broadcasts the explosion parameters
     * e.g. So objects that can be damaged by the 
     *      explosion will be notified when one happens
     * 
     * @param Vector3 - Spawn position 
     * @param float   - Blast radius 
     * @param float   - Damage 
     */
    // public static event Action<Vector3, float, float> OnExplosion;

    private void Awake()
    {
        m_explosionPrefabs = new List<GameObject>();

        for (int i = 0; i < 10; ++i)
        {
            GameObject goExplosion = Instantiate( explosionPrefab, new Vector3(0, 0, 0), Quaternion.identity );
            goExplosion.SetActive( false );
            m_explosionPrefabs.Add( goExplosion );
        }
    }

    private void OnEnable()
    {
        // Subscribe to events 
        SuicideBomberZombie.OnSuicideZombieExplode += SpawnExplosion;
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        SuicideBomberZombie.OnSuicideZombieExplode -= SpawnExplosion;
    }

    private void SpawnExplosion(Vector3 pos, float blastRadius, float damage)
    {
        GameObject explosion = FetchExplosion( pos, blastRadius );
        explosion.SetActive( true );
        explosion.transform.position = pos;
        explosion.transform.localScale = new Vector3(blastRadius * 2, blastRadius * 2, blastRadius * 2);

        // Handle explosion damage
        Collider[] colliders = Physics.OverlapSphere(pos, blastRadius);
        this.HandleExplosion(damage, colliders);
    }

    private void HandleExplosion(float damage, Collider[] colliders)
    {
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == null)
                continue;

            if (collider.gameObject.CompareTag("Player"))
            {
                PlayerInfo playerInfo = collider.gameObject.GetComponent<PlayerInfo>();

                if (playerInfo != null)
                {
                    playerInfo.TakeDamage( damage );
                    continue;
                }

                Debug.LogError("ExplosionManager, PlayerInfo is null");
            }
            else if (collider.gameObject.CompareTag("Teammate"))
            {
                TeammateController teammateController = collider.gameObject.GetComponent<TeammateController>();
                if (teammateController != null)
                {
                    teammateController.TakeDamage(damage);
                    continue;
                }

                Debug.LogError("ExplosionManager, TeammateController is null");
            }
            else if (collider.gameObject.CompareTag("Enemy"))
            {
                RegularZombie regularZombie = collider.gameObject.GetComponent<RegularZombie>();
                if (regularZombie != null)
                {
                    regularZombie.TakeDamage( damage );
                    continue;
                }

                BossZombie bossZombie = collider.gameObject.GetComponent<BossZombie>();
                if (bossZombie != null)
                {
                    bossZombie.TakeDamage( damage );
                    continue;
                }

                SuicideBomberZombie suicideBomberZombie = collider.gameObject.GetComponent<SuicideBomberZombie>();
                if (suicideBomberZombie != null)
                {
                    suicideBomberZombie.TakeDamage( damage );
                    continue;
                }
            }
        }
    }

    private GameObject FetchExplosion(Vector3 pos, float blastRadius)
    {
        for (int i = 0; i < m_explosionPrefabs.Count; ++i)
        {
            GameObject explosion = m_explosionPrefabs[i];

            if (!explosion.activeSelf)
            {
                explosion.transform.position = pos;
                explosion.transform.localScale = new Vector3(blastRadius * 2, blastRadius * 2, blastRadius * 2);
                explosion.SetActive( true );
                return explosion;
            }
        }

        // Should really try not to hit here
        Debug.Log( "Probably should increase the explosion object pool size" );

        for (int i = 0; i < 4; ++i)
        {
            GameObject goExplosion = Instantiate( explosionPrefab, new Vector3(0, 0, 0), Quaternion.identity );
            goExplosion.SetActive( false );
            m_explosionPrefabs.Add( goExplosion );
        }

        int lastElem = m_explosionPrefabs.Count - 1;
        m_explosionPrefabs[lastElem].transform.position = pos;
        m_explosionPrefabs[lastElem].transform.localScale = new Vector3(blastRadius * 2, blastRadius * 2, blastRadius * 2);
        m_explosionPrefabs[lastElem].SetActive( true );
        return m_explosionPrefabs[lastElem];
    }
}
