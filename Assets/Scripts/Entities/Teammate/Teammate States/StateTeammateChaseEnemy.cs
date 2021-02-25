using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateTeammateChaseEnemy : State
{
    // How often to set the destination
    private const float        SET_DEST_BUFFER = 0.4f;

    // How often to perform enemy check (cause doing it every frame is probably bad)
    private const float        RAYCAST_BUFFER = 0.25f;

    // How many rays (around the teammate) to cast (to check for enemies)
    private const int          NUM_RAYS = 28;

    // How long the teammate will chase the enemy before giving up
    private const float        CHASE_TIME = 7.85f;

    private TeammateController m_controller;
    private NavMeshAgent       m_navMeshAgent;
    private WeaponController   m_weaponController;
    private WaypointManager    m_waypointManager;
    private Zombie             m_enemy;

    private float              m_setDestBuffer;
    private float              m_raycastBuffer;
    private float              m_timeSinceSeenEnemy;

    public StateTeammateChaseEnemy(TeammateController teammateController)
    {
        m_controller       = teammateController;
        m_navMeshAgent     = teammateController.GetComponent<NavMeshAgent>();
        m_weaponController = teammateController.m_weaponController;
        m_waypointManager  = WaypointManager.Instance;
    }

    public override void OnStateEnter()
    {
        m_navMeshAgent.isStopped = false;
        m_navMeshAgent.autoBraking = true;
        m_navMeshAgent.speed = m_controller.MoveSpeed;
        m_navMeshAgent.SetDestination(m_waypointManager.GetRandomWaypoint());

        m_enemy              = m_controller.m_enemy; 
        m_setDestBuffer      = SET_DEST_BUFFER;
        m_raycastBuffer      = RAYCAST_BUFFER;
        m_timeSinceSeenEnemy = 0f;
    }

    public override void OnStateUpdate()
    {
        // State transition(s)
        if ((Zombie)m_enemy == null)
        {
            m_controller.m_stateMachine.ChangeState("TeammatePatrol");
            return;
        }
        if ((GameObject)m_enemy.GetGameObject() == null)
        {
            m_controller.m_stateMachine.ChangeState("TeammatePatrol");
            return;
        }

        // Check for enemies
        m_raycastBuffer -= Time.deltaTime;
        if (m_raycastBuffer <= 0f)
        {
            m_raycastBuffer = RAYCAST_BUFFER;

            // Start chasing after the enemy
            float FOV = 225f;
            Vector3 pos = m_controller.transform.position;
            Vector3 forward = m_controller.transform.forward;
            Vector3 dir = new Vector3(forward.x, forward.y, forward.z);
            pos.y = 1f;
            dir = Quaternion.Euler(0f, -FOV * 0.5f, 0) * dir;

            RaycastHit hitInfo;
            Zombie closestZombie = null;

            float dTheta = FOV / NUM_RAYS;
            for (int i = 0; i < NUM_RAYS; ++i)
            {
                Debug.DrawRay(pos, dir * m_controller.DetectionRange, Color.red, RAYCAST_BUFFER, true);
                bool foundHit = Physics.Raycast(pos, dir, out hitInfo, m_controller.DetectionRange);
                dir = Quaternion.Euler(0f, dTheta, 0f) * dir;
                if (!foundHit) continue;

                GameObject other = hitInfo.collider.gameObject;
                Zombie zombieComponent = other.GetComponent<Zombie>();
                if (zombieComponent is Zombie && zombieComponent != null)
                {
                    // If spot enemy, reset chase time
                    m_controller.SetEnemy( zombieComponent );
                    m_navMeshAgent.SetDestination( other.gameObject.transform.position );
                    m_controller.m_stateMachine.ChangeState("TeammateShoot");
                    return;
                }
            }
        }

        // If it's been too long since seeing the enemy, 
        // give up and go into patrol state
        m_timeSinceSeenEnemy += Time.deltaTime;
        if (m_timeSinceSeenEnemy > CHASE_TIME)
        {
            m_controller.SetEnemy( null );
            m_controller.m_stateMachine.ChangeState("TeammatePatrol");
            return;
        }

        // Set destination
        m_setDestBuffer -= Time.deltaTime;
        if (m_setDestBuffer <= 0f)
        {
            m_setDestBuffer = SET_DEST_BUFFER;

            GameObject enemyGO = m_enemy.GetGameObject();
            Vector3 enemyPos = enemyGO.transform.position;
            m_navMeshAgent.SetDestination( enemyPos );
        }
    }

    public override void OnStateExit()
    {
    }

    public override string GetStateID()
    {
        return "TeammateChaseEnemy";
    }
}
