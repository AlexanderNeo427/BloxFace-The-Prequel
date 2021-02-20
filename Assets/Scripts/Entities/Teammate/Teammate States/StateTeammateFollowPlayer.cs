using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateTeammateFollowPlayer : State
{
    private const float        SET_DEST_BUFFER = 0.6f;

    private TeammateController m_controller;
    private NavMeshAgent       m_navMeshAgent;
    private PlayerInfo         m_playerInfo;
    private float              m_setDestBuffer;

    public StateTeammateFollowPlayer(TeammateController teammateController,
                                     PlayerInfo         playerInfo)
    {
        m_controller   = teammateController;
        m_navMeshAgent = teammateController.GetComponent<NavMeshAgent>();
        m_playerInfo   = playerInfo;
    }
    public override void OnStateEnter()
    {
        m_navMeshAgent.isStopped = false;
        m_navMeshAgent.autoBraking = true;
        m_setDestBuffer = 0f;
    }

    public override void OnStateUpdate()
    {
        // State transition(s)
        Vector3 myPos = m_controller.transform.position;
        Vector3 playerPos = m_playerInfo.pos;

        float distFromPlayer = (myPos - playerPos).magnitude;
        if (distFromPlayer <= m_navMeshAgent.stoppingDistance)
        {
            m_controller.stateMachine.ChangeState("TeammatePatrol");
        }

        // Set destination only after every set interval
        m_setDestBuffer += Time.deltaTime;
        if (m_setDestBuffer >= SET_DEST_BUFFER)
        {
            m_setDestBuffer = 0f;
            m_navMeshAgent.SetDestination( m_playerInfo.pos );
        }
    }

    public override void OnStateExit()
    {

    }

    public override string GetStateID()
    {
        return "TeammateFollowPlayer";
    }
}
