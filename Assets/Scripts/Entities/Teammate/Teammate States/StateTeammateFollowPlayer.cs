using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateTeammateFollowPlayer : State
{
    private const float        SET_DEST_BUFFER = 0.8f;
    private const float        SPHERECAST_BUFFER = 0.25f;

    private TeammateController m_controller;
    private NavMeshAgent       m_navMeshAgent;
    private PlayerInfo         m_playerInfo;
    private float              m_setDestBuffer;
    private float              m_spherecastBuffer;

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
        m_navMeshAgent.speed = m_controller.MoveSpeed;
        m_setDestBuffer = 0f;
        m_spherecastBuffer = 0f;
    }

    public override void OnStateUpdate()
    {
        // State transition(s)
        Vector3 myPos = m_controller.transform.position;
        Vector3 playerPos = m_playerInfo.pos;

        float distFromPlayer = (myPos - playerPos).magnitude;
        if (distFromPlayer <= m_navMeshAgent.stoppingDistance + 0.5f)
        {
            m_controller.m_stateMachine.ChangeState("TeammatePatrol");
        }

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
