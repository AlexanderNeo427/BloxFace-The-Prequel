using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 *  Attach this script to the root GameObject
 *  Hopefully that GameObject is your Teammate model/prefab
 */
public class TeammateController : MonoBehaviour, Entity
{
    [Header ("Customisations")]
    [SerializeField] [Range(50f, 150f)] private float health;
    [SerializeField] [Range(1f, 8f)]    private float moveSpeed;

    [Header ("References")]
    [SerializeField] private GameObject weapons;

    public StateMachine      stateMachine { get; private set; }
    private NavMeshAgent     m_navMeshAgent;
    private PlayerInfo       m_playerInfo;
    private WeaponController m_weaponController;
    private float            m_health;

    private void Start()
    {
        m_health = health;
        m_playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();

        m_weaponController = weapons.GetComponent<WeaponController>();

        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.speed = moveSpeed;

        stateMachine = new StateMachine();
        stateMachine.AddState(new StateTeammateIdle(this, m_playerInfo));
        stateMachine.AddState(new StateTeammateFollowPlayer(this, m_navMeshAgent, m_playerInfo));
        stateMachine.ChangeState("TeammateIdle");
    }

    private void OnTriggerEnter(Collider other)
    {
        Fireball fireball = other.gameObject.GetComponent<Fireball>();
        if (fireball != null)
        {
            this.TakeDamage( fireball.Damage );
        }
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void TakeDamage(float dmg)
    {
        m_health -= dmg;

        if (m_health <= 0f)
        {
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
}
