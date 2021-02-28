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

    [SerializeField] [Range(50f, 300f)]
    private float health;

    [SerializeField] [Range(1f, 8f)]
    private float moveSpeed;

    [Header("Attack")]

    [SerializeField] [Range(3f, 30f)]
    private float explosionDamage;

    [SerializeField] [Range(3f, 30f)]
    private float blastRadius;

    public StateMachine  stateMachine { get; private set; }
    private NavMeshAgent m_navMeshAgent;
    private PlayerInfo   m_playerInfo;
    private float        m_health;

    public float HP             { get { return m_health; } }
    public float MoveSpeed      { get { return moveSpeed; } }

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
    public static event Action<Vector3, float> OnDamaged;

    private void Start()
    {
        m_health = health;

        m_playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
        if (m_playerInfo == null)
            Debug.LogError("SuicideBomberZombie Start() : m_playerInfo is NULL");

        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.stoppingDistance = blastRadius * 0.75f;

        // Nav mesh agent speed
        float speed = moveSpeed + UnityEngine.Random.Range(-5, 5f);
        speed = Mathf.Max(3f, speed);
        m_navMeshAgent.speed = speed;

        // Add states here
        stateMachine = new StateMachine();
        // stateMachine.AddState(new StateSuicideZombiePatrol(this, m_playerInfo));
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
        if (m_health <= 0f)
            return;

        if (m_health - dmg <= 0f)
        {
            m_health = -Mathf.Epsilon;
            OnDeath?.Invoke(transform.position);
            Destroy(this.gameObject);
        }
        else
        {
            m_health -= dmg;
            OnDamaged?.Invoke(transform.position, dmg);
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
        if (this != null)
            return this.gameObject;

        return null;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        Vector3 pos = transform.position;
        pos += new Vector3(0f, 3.8f, 0);

        if (stateMachine.GetCurrentState() == "SuicideZombiePatrol")
        {
            Gizmos.color = Color.green;
        }
        else if (stateMachine.GetCurrentState() == "SuicideZombieChase")
        {
            Gizmos.color = Color.yellow;
        }
        Gizmos.DrawSphere(pos, 0.4f);
    }
}

