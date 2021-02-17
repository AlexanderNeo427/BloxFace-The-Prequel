using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 *  Controller script for the suicide bomber zombie
 *  Attach this script to the enemy model
 */
[RequireComponent(typeof(NavMeshAgent))]
public class SuicideBomberZombie : MonoBehaviour, Zombie
{
    [Header("Stats")]

    [SerializeField] [Range(50f, 150f)]
    private float health;

    [SerializeField] [Range(1f, 8f)]
    private float moveSpeed;

    [Header("Attack")]

    [SerializeField] [Range(5f, 50f)]
    private float explosionDamage;

    [SerializeField] [Range(2.5f, 8f)]
    private float blastRadius;

    public StateMachine  stateMachine { get; private set; }
    private NavMeshAgent m_navMeshAgent;
    private PlayerInfo   m_playerInfo;
    private float        m_health;
   
   /*
    * Spawns explosion at zombies' position with 
    * specified position, blast radius, and damage
    * 
    * @param Vector3 - Spawn position 
    * @param float   - Blast radius 
    * @param float   - Damage 
    */
    public static event Action<Vector3, float, float> OnSuicideZombieDeath;

    private void Start()
    {
        m_playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
        if (m_playerInfo == null)
            Debug.LogError("SuicideBomberZombie Start() : m_playerInfo is NULL");

        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.speed = moveSpeed;
        m_navMeshAgent.stoppingDistance = blastRadius * 0.75f;

        // Add states here
        stateMachine = new StateMachine();
        stateMachine.AddState(new StateSuicideZombieChase(this, m_navMeshAgent, m_playerInfo));
        stateMachine.ChangeState("SuicideZombieChase");

        m_health = health;
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        // TODO : work with Gabriel on this
       /* if (other.gameObject.CompareTag("Bullet"))
            this.TakeDamage(  );*/
    }

    public void Attack()
    {
        m_health = 0f;
        OnSuicideZombieDeath?.Invoke( transform.position, blastRadius, explosionDamage );
        m_navMeshAgent.isStopped = true;
        Destroy( this.gameObject );
    }

    public void TakeDamage(float dmg)
    {
        m_health -= dmg;

        if (m_health <= 0f)
        {
            OnSuicideZombieDeath?.Invoke( transform.position, blastRadius, explosionDamage );
            m_navMeshAgent.isStopped = true;
            Destroy( this.gameObject );
        }
    }
}

