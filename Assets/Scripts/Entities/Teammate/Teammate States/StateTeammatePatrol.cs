using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateTeammatePatrol : State
{
    // At what distance does the teammate start chasing the player
    private const float        PLAYER_FOLLOW_THRESHOLD = 22.5f;
    private const float        SPHERECAST_BUFFER = 0.25f;

    private TeammateController m_controller;
    private NavMeshAgent       m_navMeshAgent;
    private WeaponController   m_weaponController;
    private WaypointManager    m_waypointManager;
    private PlayerInfo         m_playerInfo;
    private float              m_spherecastBuffer;

    public StateTeammatePatrol(TeammateController teammateController, 
                               PlayerInfo         playerInfo)
    {
        m_controller       = teammateController;
        m_navMeshAgent     = teammateController.GetComponent<NavMeshAgent>();
        m_weaponController = teammateController.m_weaponController;
        m_waypointManager  = WaypointManager.Instance;
        m_playerInfo       = playerInfo;
    }

    public override void OnStateEnter()
    {
        m_navMeshAgent.isStopped = false;
        m_navMeshAgent.autoBraking = true;
        m_navMeshAgent.speed = m_controller.MoveSpeed;
        m_navMeshAgent.SetDestination( m_waypointManager.GetRandomWaypoint() );

        m_spherecastBuffer = SPHERECAST_BUFFER;
    }

    public override void OnStateUpdate()
    {
        // Check for enemies
        m_spherecastBuffer -= Time.deltaTime;
        if (m_spherecastBuffer <= 0f)
        {
            m_spherecastBuffer = SPHERECAST_BUFFER;

            // It goes for the first enemy within it's detection range
            Vector3 pos = m_controller.transform.position;
            pos.y = 0f;
            float detectionRange = 12f;

            Collider[] colliders = Physics.OverlapSphere(pos, detectionRange);
            foreach (Collider collider in colliders)
            {
                Zombie zombie = collider.gameObject.GetComponent<Zombie>();
                if (zombie is Zombie)
                {
                    m_controller.SetEnemy( zombie );
                    m_controller.m_stateMachine.ChangeState("TeammateShoot");
                    break;
                }
            }
        }

        // Patrol behavior
        if (m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance + 0.5f)
        {
            Vector3 nextWaypoint = m_waypointManager.GetRandomWaypoint();
            m_navMeshAgent.SetDestination( nextWaypoint );
        }

        // State transition(s)
        if (DistFromPlayer() > PLAYER_FOLLOW_THRESHOLD)
            m_controller.m_stateMachine.ChangeState("TeammateFollowPlayer");
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