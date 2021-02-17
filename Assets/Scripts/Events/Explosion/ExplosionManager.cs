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

    private void OnEnable()
    {
        // Subscribe to events 
        SuicideBomberZombie.OnSuicideZombieDeath += SpawnExplosion;
    }

    private void SpawnExplosion(Vector3 pos, float blastRadius, float damage)
    {
        GameObject explosion = Instantiate(explosionPrefab, pos, Quaternion.identity);
        explosion.transform.localScale = new Vector3( blastRadius * 2, blastRadius * 2, blastRadius * 2 );

        // Handle explosion damage
        Collider[] colliders = Physics.OverlapSphere(pos, blastRadius);
        this.HandleExplosion(damage, colliders);
    }

    private void HandleExplosion(float damage, Collider[] colliders)
    {
        /*foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                PlayerInfo playerInfo = collider.gameObject.GetComponent<PlayerInfo>();
                playerInfo.TakeDamage( damage );
            }
            else if (collider.gameObject.CompareTag("Enemy"))
            {
                RegularZombie regularZombie = collider.gameObject.GetComponent<RegularZombie>();
                if (regularZombie != null)
                    regularZombie.TakeDamage( damage );

                SuicideBomberZombie suicideBomberZombie = collider.gameObject.GetComponent<SuicideBomberZombie>();
                if (suicideBomberZombie != null)
                    suicideBomberZombie.TakeDamage( damage );
            }
        }*/
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        SuicideBomberZombie.OnSuicideZombieDeath -= SpawnExplosion;
    }
}
