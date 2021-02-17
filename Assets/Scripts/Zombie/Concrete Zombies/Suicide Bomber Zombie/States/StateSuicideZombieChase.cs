using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateSuicideZombieChase : State
{
    private SuicideBomberZombie m_zombieController;
    private NavMeshAgent        m_navMeshAgent;
    private PlayerInfo          m_playerInfo;

    public StateSuicideZombieChase(SuicideBomberZombie zombieController,
                                   NavMeshAgent        navMeshAgent,
                                   PlayerInfo          playerInfo)
    {
        m_zombieController = zombieController;
        m_navMeshAgent     = navMeshAgent;
        m_playerInfo       = playerInfo;
    }

    public override void OnStateEnter()
    {
        m_navMeshAgent.isStopped = false;
    }

    public override void OnStateUpdate()
    {
        bool playerInBlastRadius = DistFromPlayer() <= m_navMeshAgent.stoppingDistance;
        if (playerInBlastRadius)
        {
            m_zombieController.Attack();
            return;
        }

        m_navMeshAgent.SetDestination( m_playerInfo.pos );
    }

    public override void OnStateExit()
    {
        m_navMeshAgent.isStopped = true;
    }

    public override string GetStateID()
    {
        return "SuicideZombieChase";
    }

    private float DistFromPlayer()
    {
        Vector3 playerPos = m_playerInfo.pos;
        Vector3 myPos = m_zombieController.transform.position;

        return (playerPos - myPos).magnitude;
    }
}
