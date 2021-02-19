using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateBossZombieAttack : State      
{
    private BossZombie   m_zombieController;
    private NavMeshAgent m_navMeshAgent;
    private PlayerInfo   m_playerInfo;
    private float        m_attackRange;
    private float        m_attackSpeed;
    private float        m_attackTimer;

    public StateBossZombieAttack(BossZombie zombieController,
                                 PlayerInfo playerInfo)
    {
        m_zombieController = zombieController;
        m_navMeshAgent     = zombieController.GetComponent<NavMeshAgent>();
        m_playerInfo       = playerInfo;
        m_attackRange      = m_zombieController.AttackRange;
        m_attackSpeed      = m_zombieController.AttackSpeed;
        m_attackTimer      = 0f;
    }

    public override void OnStateEnter()
    {
        m_navMeshAgent.updatePosition = true;
    }

    public override void OnStateUpdate()
    {
        m_attackTimer += Time.deltaTime;
        if (m_attackTimer >= m_attackSpeed)
        {
            m_zombieController.Attack();
            m_attackTimer = 0f;
        }

        bool playerOutOfRange = DistFromPlayer() > m_attackRange;
        if (playerOutOfRange)
            m_zombieController.stateMachine.ChangeState("BossZombieChase");

        m_navMeshAgent.SetDestination( m_playerInfo.pos );
    }

    public override void OnStateExit()
    {
    }

    public override string GetStateID()
    {
        return "BossZombieAttack";
    }

    private float DistFromPlayer()
    {
        Vector3 playerPos = m_playerInfo.pos;
        Vector3 myPos = m_zombieController.transform.position;

        return (playerPos - myPos).magnitude;
    }
}
