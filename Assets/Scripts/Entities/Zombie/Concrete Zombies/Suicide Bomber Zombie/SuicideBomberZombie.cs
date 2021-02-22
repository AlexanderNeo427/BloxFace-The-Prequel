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
public class SuicideBomberZombie : MonoBehaviour, Zombie, Entity
{
    [Header("Stats")]

    [SerializeField] [Range(50f, 150f)]
    private float health;

    [SerializeField] [Range(1f, 8f)]
    private float moveSpeed;

    [Header("Attack")]

    [SerializeField] [Range(5f, 50f)]
    private float explosionDamage;

    [SerializeField] [Range(5f, 20f)]
    private float blastRadius;

    public StateMachine  stateMachine { get; private set; }
    private NavMeshAgent m_navMeshAgent;
    private PlayerInfo   m_playerInfo;
    private float        m_health;

    public float HP          { get { return m_health; } }
    public float MoveSpeed   { get { return moveSpeed; } }

    /*
     * Spawns explosion at zombies' position with 
     * specified position, blast radius, and damage
     * 
     * @param Vector3 - Spawn position 
     * @param float   - Blast radius 
     * @param float   - Damage 
     */
    public static event Action<Vector3, float, float> OnSuicideZombieExplode;

    // Broadcast the Entity's position at time of death
    public static event Action<Vector3> OnDeath;

    /*
     * Broadcasts the info about the zombie
     * when he gets damaged
     * 
     * @param Vector3 - Spawn position 
     * @param float   - Damage 
     */
    public static event Action<Vector3, float> OnDamage;

    private void Start()
    {
        m_health = health;

        m_playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
        if (m_playerInfo == null)
            Debug.LogError("SuicideBomberZombie Start() : m_playerInfo is NULL");

        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.stoppingDistance = blastRadius * 0.75f;

        // Nav mesh agent speed
        float speed = moveSpeed + UnityEngine.Random.Range(-6, 6f);
        speed = Mathf.Max(0.75f, speed);
        m_navMeshAgent.speed = speed;

        // Add states here
        stateMachine = new StateMachine();
        stateMachine.AddState(new StateSuicideZombieChase(this, m_playerInfo));
        stateMachine.ChangeState("SuicideZombieChase");
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void Attack()
    {
        OnSuicideZombieExplode?.Invoke( transform.position, blastRadius, explosionDamage );
        Destroy( this.gameObject );
    }

    public void TakeDamage(float dmg)
    {
        m_health -= dmg;

        if (m_health <= 0f)
        {
            OnDeath?.Invoke( transform.position );
            Destroy( this.gameObject );
        }
    }

    public float GetMaxHP()
    {
        return health;
    }

    public float GetCurrentHP()
    {
        return m_health;
    }

    public GameObject GetGameObject()
    {
        if (this.gameObject != null && !this.gameObject.Equals(null))
            return this.gameObject;

        return null;
    }
}

