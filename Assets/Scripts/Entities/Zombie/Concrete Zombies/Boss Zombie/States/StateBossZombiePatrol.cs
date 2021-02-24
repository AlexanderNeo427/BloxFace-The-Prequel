using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateBossZombiePatrol : State
{
    // How often to check whether the player is within line of sight
    private const float     SPHERECAST_BUFFER = 0.3f;

    // How many rays to cast to check player in line-of-sight
    private const int       NUM_RAYS = 21;

    // How often to recalculate the NavMesh path
    private const float     SET_DEST_BUFFER = 0.75f;

    private BossZombie      m_zombieController;
    private NavMeshAgent    m_navMeshAgent;
    private WaypointManager m_waypointManager;
    private PlayerInfo      m_playerInfo;
    private Vector3         m_currWaypoint;
    private float           m_setDestBuffer;
    private float           m_spherecastBuffer;

    public StateBossZombiePatrol(BossZombie zombieController,
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
        m_currWaypoint = initialWaypoint;
        m_navMeshAgent.isStopped = false;
        m_navMeshAgent.autoBraking = true;
        m_navMeshAgent.SetDestination( initialWaypoint );

        float speed = m_zombieController.MoveSpeed + UnityEngine.Random.Range(-5, 5f);
        speed = Mathf.Max(1.25f, speed);
        m_navMeshAgent.speed = speed;

        m_setDestBuffer = SET_DEST_BUFFER;
        m_spherecastBuffer = SPHERECAST_BUFFER;
    }

    public override void OnStateUpdate()
    {
        // State transition(s)
        // Check if player can be seen
        m_spherecastBuffer -= Time.deltaTime;
        if (m_spherecastBuffer <= 0f)
        {
            m_spherecastBuffer = SPHERECAST_BUFFER;

            float FOV = 225f;
            float dTheta = FOV / NUM_RAYS;
            Vector3 pos = m_zombieController.transform.position;
            Vector3 dir = m_zombieController.transform.forward;
            dir = Quaternion.Euler(0f, -FOV * 0.5f, 0) * dir;
            pos.y = 1f;
            RaycastHit hitInfo;

            for (int i = 0; i < NUM_RAYS; ++i)
            {
                Debug.DrawRay(pos, dir * m_zombieController.DetectionRange, Color.green, SPHERECAST_BUFFER, true);

                bool foundHit = Physics.Raycast(pos, dir, out hitInfo, m_zombieController.DetectionRange);
                dir = Quaternion.Euler(0f, dTheta, 0f) * dir;
                if (!foundHit) continue;

                GameObject other = hitInfo.collider.gameObject;
                if (other.CompareTag("Player"))
                {
                    m_zombieController.stateMachine.ChangeState("BossZombieAttack");
                    return;
                }
            }
        }

        // Behavior
        m_setDestBuffer -= Time.deltaTime;
        if (m_setDestBuffer < 0f)
        {
            m_setDestBuffer = SET_DEST_BUFFER;
            m_navMeshAgent.SetDestination( m_currWaypoint );
        }

        if (m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
        {
            Vector3 nextWaypoint = m_waypointManager.GetRandomWaypoint();
            m_navMeshAgent.SetDestination( nextWaypoint );
            m_currWaypoint = nextWaypoint;
        }
    }

    public override void OnStateExit()
    {
    }

    public override string GetStateID()
    {
        return "BossZombiePatrol";
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