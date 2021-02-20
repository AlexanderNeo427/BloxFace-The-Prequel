using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateTeammatePatrol : State
{
    // At what distance does the teammate start chasing the player
    private float              PLAYER_FOLLOW_THRESHOLD = 10f;

    private TeammateController m_controller;
    private NavMeshAgent       m_navMeshAgent;
    private WaypointManager    m_waypointManager;
    private PlayerInfo         m_playerInfo;

    public StateTeammatePatrol(TeammateController teammateController, 
                               PlayerInfo         playerInfo)
    {
        m_controller      = teammateController;
        m_navMeshAgent    = teammateController.GetComponent<NavMeshAgent>();
        m_waypointManager = WaypointManager.Instance;
        m_playerInfo      = playerInfo;
    }

    public override void OnStateEnter()
    {
        m_navMeshAgent.isStopped = false;
        m_navMeshAgent.autoBraking = true;
        m_navMeshAgent.SetDestination( m_waypointManager.GetRandomWaypoint() );
    }

    public override void OnStateUpdate()
    {
        // Patrol behavior
        if (m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance + 1f)
        {
            Vector3 nextWaypoint = m_waypointManager.GetRandomWaypoint();
            m_navMeshAgent.SetDestination( nextWaypoint );
        }

        // State transition(s)
        if (DistFromPlayer() > PLAYER_FOLLOW_THRESHOLD)
            m_controller.stateMachine.ChangeState("TeammateFollowPlayer");
    }

    public override void OnStateExit()
    {
    }

    public override string GetStateID()
    {
        return "TeammatePatrol";
    }

    // Helper
    private float DistFromPlayer()
    {
        Vector3 myPos = m_controller.transform.position;
        Vector3 playerPos = m_playerInfo.pos;

        return Vector3.Distance( myPos, playerPos );
    }
}