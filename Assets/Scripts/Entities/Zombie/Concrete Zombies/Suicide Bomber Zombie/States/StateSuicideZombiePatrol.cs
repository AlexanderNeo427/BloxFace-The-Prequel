﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateSuicideZombiePatrol : State
{
    private SuicideBomberZombie m_zombieController;
    private NavMeshAgent        m_navMeshAgent;
    private WaypointManager     m_waypointManager;
    private PlayerInfo          m_playerInfo;

    public StateSuicideZombiePatrol(SuicideBomberZombie  zombieController,
                                    PlayerInfo playerInfo)
    {
        m_zombieController = zombieController;
        m_navMeshAgent     = zombieController.GetComponent<NavMeshAgent>();
        m_waypointManager  = WaypointManager.Instance;
        m_playerInfo       = playerInfo;
    }

    public override void OnStateEnter()
    {
        Vector3 initialWaypoint = m_waypointManager.GetRandomWaypoint();

        m_navMeshAgent.isStopped = false;
        m_navMeshAgent.autoBraking = true;
        m_navMeshAgent.speed = m_zombieController.MoveSpeed;
        m_navMeshAgent.SetDestination( initialWaypoint );
    }

    public override void OnStateUpdate()
    {
        // State transition(s)
        if (DistFromPlayer() <= m_zombieController.DetectionRange)
        {
            m_navMeshAgent.SetDestination(m_playerInfo.pos);
            m_zombieController.stateMachine.ChangeState("SuicideZombieChase");
        }

        // Behavior
        if (m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
        {
            Vector3 nextWaypoint = m_waypointManager.GetRandomWaypoint();
            m_navMeshAgent.SetDestination( nextWaypoint );
        }
    }

    public override void OnStateExit()
    {
    }

    public override string GetStateID()
    {
        return "SuicideZombiePatrol";
    }

    // Helper
    private float DistFromPlayer()
    {
        Vector3 myPos = m_zombieController.transform.position;
        Vector3 playerPos = m_playerInfo.pos;
        myPos.y = playerPos.y = 0f;

        return Vector3.Distance(myPos, playerPos);
    }
}