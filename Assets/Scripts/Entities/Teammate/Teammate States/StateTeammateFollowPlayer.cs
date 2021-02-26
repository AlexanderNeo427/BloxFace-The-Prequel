using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateTeammateFollowPlayer : State
{
    // How often to set the destination
    private const float        SET_DEST_BUFFER = 0.75f;

    // How often to perform enemy check (cause doing it every frame is probably bad)
    private const float        RAYCAST_BUFFER = 0.25f;

    // How many rays (around the teammate) to cast (to check for enemies)
    private const int          NUM_RAYS = 28;

    private TeammateController m_controller;
    private NavMeshAgent       m_navMeshAgent;
    private PlayerInfo         m_playerInfo;
    private float              m_setDestBuffer;
    private float              m_raycastBuffer;

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

        m_setDestBuffer = SET_DEST_BUFFER;
        m_raycastBuffer = RAYCAST_BUFFER;

        m_controller.SetEnemy( null );
    }

    public override void OnStateUpdate()
    {
        // State transition(s)
       /* if (m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance + 1f)
        {
            m_controller.m_stateMachine.ChangeState("TeammatePatrol");
            return;
        }*/

        // Check for enemies
        m_raycastBuffer -= Time.deltaTime;
        if (m_raycastBuffer <= 0f)
        {
            m_raycastBuffer = RAYCAST_BUFFER;

            // Finds closest enemy in detection range
            float FOV = 225f;
            Vector3 pos = m_controller.transform.position;
            Vector3 forward = m_controller.transform.forward;
            Vector3 dir = new Vector3(forward.x, forward.y, forward.z);
            pos.y = 1f;
            dir = Quaternion.Euler(0f, -FOV * 0.5f, 0) * dir;

            RaycastHit hitInfo;
            Zombie closestZombie = null;
            float minDist = 999f;

            float dTheta = FOV / NUM_RAYS;
            for (int i = 0; i < NUM_RAYS; ++i)
            {
                Debug.DrawRay(pos, dir * m_controller.DetectionRange, Color.blue, RAYCAST_BUFFER, true);
                bool foundHit = Physics.Raycast(pos, dir, out hitInfo, m_controller.DetectionRange);
                dir = Quaternion.Euler(0f, dTheta, 0f) * dir;
                if (!foundHit) continue;

                GameObject other = hitInfo.collider.gameObject;

                if (other.CompareTag("Player"))
                {
                    m_controller.m_stateMachine.ChangeState("TeammatePatrol");
                    return;
                }

                Zombie zombieComponent = other.GetComponent<Zombie>();
                if (zombieComponent is Zombie && zombieComponent != closestZombie)
                {
                    Vector3 otherPos = other.transform.position;
                    otherPos.y = 0f;

                    float distFromEnemy = Vector3.Distance( pos, otherPos );
                    if (distFromEnemy < minDist)
                    {
                        minDist = distFromEnemy;
                        closestZombie = zombieComponent;
                    }
                }
            }

            // Found enemy!!
            if ((Zombie)closestZombie != null)
            {
                m_controller.SetEnemy( closestZombie );
                m_controller.m_stateMachine.ChangeState("TeammateShoot");
            }
        }

        // Set destination only after every set interval
        m_setDestBuffer -= Time.deltaTime;
        if (m_setDestBuffer <= 0f)
        {
            m_setDestBuffer = SET_DEST_BUFFER;
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
