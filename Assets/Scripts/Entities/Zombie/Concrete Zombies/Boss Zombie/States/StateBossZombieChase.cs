using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateBossZombieChase : State
{
    private BossZombie   m_zombieController;
    private NavMeshAgent m_navMeshAgent;
    private PlayerInfo   m_playerInfo;

    public StateBossZombieChase(BossZombie   zombieController,
                                PlayerInfo   playerInfo)
    {
        m_zombieController = zombieController;
        m_navMeshAgent     = zombieController.GetComponent<NavMeshAgent>();
        m_playerInfo       = playerInfo;
    }

    public override void OnStateEnter()
    {
        m_navMeshAgent.isStopped      = false;
        m_navMeshAgent.updatePosition = true;
    }

    public override void OnStateUpdate()
    {
        bool playerWithinRange = DistFromPlayer() <= m_navMeshAgent.stoppingDistance;
        if (playerWithinRange)
            m_zombieController.stateMachine.ChangeState("BossZombieAttack");

        m_navMeshAgent.SetDestination( m_playerInfo.pos );
    }

    public override void OnStateExit()
    {
    }

    public override string GetStateID()
    {
        return "BossZombieChase";
    }

    private float DistFromPlayer()
    {
        Vector3 playerPos = m_playerInfo.pos;
        Vector3 myPos = m_zombieController.transform.position;

        return (playerPos - myPos).magnitude;
    }
}
