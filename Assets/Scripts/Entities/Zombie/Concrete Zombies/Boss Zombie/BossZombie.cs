using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 *  Controller script for the Boss zombie
 *  Attach this script to the enemy model
 */
[RequireComponent(typeof(NavMeshAgent))]
public class BossZombie : MonoBehaviour, Zombie, Entity
{
    [Header("References")]
    [SerializeField] private GameObject fireball;

    [Header("Stats")]

    [SerializeField] [Range(50f, 150f)]
    private float health;

    [SerializeField] [Range(1f, 8f)]
    private float moveSpeed;

    [Header("Attack")]

    [SerializeField] [Range(5f, 50f)]
    private float attackDamage;

    [SerializeField] [Range(12f, 30f)]
    private float attackRange;

    [SerializeField] [Range(0.1f, 1.5f)] 
    [Tooltip ("Time between attacks (in seconds)")]
    private float attackSpeed;

    public StateMachine  stateMachine { get; private set; }
    private NavMeshAgent m_navMeshAgent;
    private PlayerInfo   m_playerInfo;
    private float        m_health;

    public float HP          { get { return m_health; } }
    public float MoveSpeed   { get { return moveSpeed; } }
    public float Damage      { get { return attackDamage; } }
    public float AttackRange { get { return attackRange; } }
    public float AttackSpeed { get { return attackSpeed; } }

    // Broadcast this entity's death
    public static event Action<Vector3> OnDeath;

    private void Start()
    {
        m_playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
        if (m_playerInfo == null)
            Debug.LogError("BossZombie Start() : m_playerInfo is NULL");

        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.stoppingDistance = attackRange * 0.6f;

        // Nav mesh agent speed
        float speed = moveSpeed + UnityEngine.Random.Range(-6, 6f);
        speed = Mathf.Max(0.75f, speed);
        m_navMeshAgent.speed = speed;

        // Add states here
        stateMachine = new StateMachine();
        stateMachine.AddState(new StateBossZombieChase(this, m_playerInfo));
        stateMachine.AddState(new StateBossZombieAttack(this, m_playerInfo));

        m_health = health;
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void Attack()
    {
        Vector3 pos = transform.position + transform.forward * 0.8f;
        pos += new Vector3(0f, 1.5f, 0f);
        Instantiate(fireball, pos, transform.rotation);
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
