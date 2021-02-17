using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateTeammateFollowPlayer : State
{
    private TeammateController m_controller;
    private NavMeshAgent       m_navMeshAgent;
    private PlayerInfo         m_playerInfo;

    public StateTeammateFollowPlayer(TeammateController teammateController,
                                     NavMeshAgent       navMeshAgent,
                                     PlayerInfo         playerInfo)
    {
        m_controller   = teammateController;
        m_navMeshAgent = navMeshAgent;
        m_playerInfo   = playerInfo;
    }
    public override void OnStateEnter()
    {
        if (m_navMeshAgent == null)
        {
            Debug.LogError("StateTeammateIdle() NavMeshAgent NULL");
            m_controller.stateMachine.ChangeState("TeammateIdle");
        }

        m_navMeshAgent.isStopped = false;
    }

    public override void OnStateUpdate()
    {
        Vector3 myPos = m_controller.transform.position;
        Vector3 playerPos = m_playerInfo.pos;

        float distFromPlayer = (myPos - playerPos).magnitude;

        if (distFromPlayer <= m_navMeshAgent.stoppingDistance)
        {
            m_navMeshAgent.isStopped = true;
            m_controller.stateMachine.ChangeState("TeammateIdle");
        }

        m_navMeshAgent.SetDestination( m_playerInfo.pos );
    }

    public override void OnStateExit()
    {

    }

    public override string GetStateID()
    {
        return "TeammateFollowPlayer";
    }
}
