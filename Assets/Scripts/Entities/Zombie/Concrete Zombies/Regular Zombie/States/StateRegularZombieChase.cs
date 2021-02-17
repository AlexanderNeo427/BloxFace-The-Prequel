using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateRegularZombieChase : State
{
    private RegularZombie m_zombieController;
    private NavMeshAgent  m_navMeshAgent;
    private PlayerInfo    m_playerInfo;

    public StateRegularZombieChase(RegularZombie zombieController,
                                   NavMeshAgent  navMeshAgent,
                                   PlayerInfo    playerInfo)
    {
        m_zombieController = zombieController;
        m_navMeshAgent     = navMeshAgent;
        m_playerInfo       = playerInfo;
    }
    public override void OnStateEnter()
    {
        if (m_navMeshAgent == null)
            Debug.LogError("StateRegularZombieChase() NavMeshAgent NULL");

        m_navMeshAgent.isStopped = false;
    }

    public override void OnStateUpdate()
    {
        bool PlayerWithinRange = (DistFromPlayer() <= m_navMeshAgent.stoppingDistance);
        if (PlayerWithinRange)
            m_zombieController.stateMachine.ChangeState("RegularZombieAttack");

        m_navMeshAgent.SetDestination( m_playerInfo.pos );
    }

    public override void OnStateExit()
    {
        m_navMeshAgent.isStopped = true;
    }

    public override string GetStateID()
    {
        return "RegularZombieChase";
    }

    private float DistFromPlayer()
    {
        Vector3 playerPos = m_playerInfo.pos;
        Vector3 myPos = m_zombieController.transform.position;

        float distanceFromPlayer = (playerPos - myPos).magnitude;
        return distanceFromPlayer;
    }
}
