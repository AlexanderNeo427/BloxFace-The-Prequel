using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateRegularZombieChase : State
{
    private const float   SET_DEST_BUFFER = 0.8f;

    private RegularZombie m_zombieController;
    private NavMeshAgent  m_navMeshAgent;
    private PlayerInfo    m_playerInfo;

    private float         m_setDestBuffer;

    public StateRegularZombieChase(RegularZombie zombieController,
                                   PlayerInfo    playerInfo)
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
        bool PlayerWithinRange = (DistFromPlayer() <= m_navMeshAgent.stoppingDistance);
        if (PlayerWithinRange)
            m_zombieController.stateMachine.ChangeState("RegularZombieAttack");

        m_navMeshAgent.SetDestination( m_playerInfo.pos );
    }

    public override void OnStateExit()
    {
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
