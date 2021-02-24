using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 *  Controller script for the regular zombie
 *  Attach this script to the enemy model
 */
[RequireComponent (typeof(NavMeshAgent))]
public class RegularZombie : MonoBehaviour, Zombie, Entity
{
    [Header("Stats")]

    [SerializeField] [Range(50f, 150f)]
    private float health;

    [SerializeField] [Range(1f, 8f)]
    private float moveSpeed;

    [SerializeField] [Range(5f, 20f)] 
    [Tooltip ("Distance at which they can detect the player")]
    private float detectionRange = 10f;

    [Header ("Attack")]

    [SerializeField] [Range(5f, 20f)] 
    private float dmgPerHit;

    [SerializeField] [Range(2.5f, 5f)]
    private float attackRange;

    [SerializeField] [Range(0.2f, 1.5f)]
    [Tooltip ("Time between hits (in seconds)")]
    private float attackSpeed;

    public StateMachine  stateMachine { get; private set; }
    private NavMeshAgent m_navMeshAgent;
    private PlayerInfo   m_playerInfo;
    private float        m_health;

    // Getterss
    public float HP             { get { return m_health; } }
    public float MoveSpeed      { get { return moveSpeed; } }
    public float Damage         { get { return dmgPerHit; } }
    public float AttackRange    { get { return attackRange; } }
    public float AttackSpeed    { get { return attackSpeed; } }
    public float DetectionRange { get { return detectionRange; } }

    // ----- Events ------

    // param - Damage done to player
    public static event Action<float> OnAttackPlayer;

    // param - Position at time of death
    public static event Action<Vector3> OnDeath;

    /*
     * Broadcasts the info about the zombie
     * when he gets damaged
     * 
     * @param Vector3 - Position 
     * @param float   - Damage 
     */
    public static event Action<Vector3, float> OnDamaged;

    private void Start()
    {
        m_health = health;

        m_playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
        if (m_playerInfo == null)
            Debug.LogError("RegularZombie Start() : m_playerInfo is NULL");

        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.stoppingDistance = (attackRange * 0.85f);

        // Nav mesh agent speed
        float speed = moveSpeed + UnityEngine.Random.Range(-5, 5f);
        speed = Mathf.Max(1.25f, speed);
        m_navMeshAgent.speed = speed;

        // Add states here
        stateMachine = new StateMachine();
        stateMachine.AddState(new StateRegularZombiePatrol(this, m_playerInfo));
        stateMachine.AddState(new StateRegularZombieChase(this, m_playerInfo));
        stateMachine.AddState(new StateRegularZombieAttack(this, m_playerInfo));
        stateMachine.ChangeState("RegularZombiePatrol");
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void Attack()
    {
        OnAttackPlayer?.Invoke( dmgPerHit );
    }

    public void TakeDamage(float dmg)
    {
        m_health -= dmg;
        OnDamaged?.Invoke( transform.position, dmg );

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
        if (this != null)
            return this.gameObject;

        return null;
    }
}
