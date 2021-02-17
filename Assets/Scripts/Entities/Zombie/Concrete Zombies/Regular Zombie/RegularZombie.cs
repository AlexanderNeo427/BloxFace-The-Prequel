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

    public static event Action<float> OnPlayerDamage;
    public static event Action OnRegularZombieDeath;

    private void Start()
    {
        m_playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
        if (m_playerInfo == null)
            Debug.LogError("RegularZombie Start() : m_playerInfo is NULL");

        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.speed = moveSpeed;
        m_navMeshAgent.stoppingDistance = (attackRange * 0.5f);

        // Add states here
        stateMachine = new StateMachine();
        stateMachine.AddState(new StateRegularZombieChase(this, m_navMeshAgent, m_playerInfo));
        stateMachine.AddState(new StateRegularZombieAttack(this, m_playerInfo, 
                                                           attackRange, attackSpeed));
        stateMachine.ChangeState("RegularZombieChase");

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
        OnPlayerDamage?.Invoke( dmgPerHit );
    }

    public void TakeDamage(float dmg)
    {
        m_health -= dmg;

        if (m_health <= 0f)
        {
            OnRegularZombieDeath?.Invoke();
            m_navMeshAgent.isStopped = true;
            Destroy( this.gameObject, 1.5f );
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
}
