using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateBossZombieChase : State
{
    // How often to check if player within line-of-sight
    private const float  RAYCAST_BUFFER = 0.15f;

    // How many rays to cast within field of view
    private const int    NUM_RAYS = 22;

    // How often to recalculate the NavMesh path to target
    private const float  SET_DEST_BUFFER = 0.6f;

    // How long will try chasing the player before giving up
    private const float  CHASE_TIME = 6f;

    private BossZombie   m_zombieController;
    private NavMeshAgent m_navMeshAgent;
    private PlayerInfo   m_playerInfo;

    private float        m_raycastBuffer;
    private float        m_setDestBuffer;
    private float        m_chaseTime;
    private float        m_setSpeedBuffer;

    public StateBossZombieChase(BossZombie   zombieController,
                                PlayerInfo   playerInfo)
    {
        m_zombieController = zombieController;
        m_navMeshAgent     = zombieController.GetComponent<NavMeshAgent>();
        m_playerInfo       = playerInfo;
    }

    public override void OnStateEnter()
    {
        m_navMeshAgent.ResetPath();
        m_navMeshAgent.isStopped      = false;
        m_navMeshAgent.updatePosition = true;
        m_navMeshAgent.updateRotation = true;

        // Nav mesh agent speed
        float speed = m_zombieController.MoveSpeed + UnityEngine.Random.Range(-5, 5f);
        speed = Mathf.Max(1.25f, speed);
        m_navMeshAgent.speed = speed;

        m_raycastBuffer  = RAYCAST_BUFFER;
        m_setSpeedBuffer = 0f;
        m_chaseTime      = 0f;
        m_setDestBuffer  = SET_DEST_BUFFER;
    }

    public override void OnStateUpdate()
    {
        // If within range of attack, check line of sight to the player
        m_raycastBuffer -= Time.deltaTime;

        if (DistFromPlayer() <= m_zombieController.AttackRange)
        {
            if (m_raycastBuffer <= 0f)
            {
                m_raycastBuffer = RAYCAST_BUFFER;

                Vector3 pos = m_zombieController.transform.position;
                pos.y = 1f;

                Vector3 playerPos = m_playerInfo.pos;
                playerPos.y = 1f;

                Vector3 dir = (playerPos - pos).normalized;
                RaycastHit hitInfo;

                Debug.DrawRay(pos, dir * m_zombieController.AttackRange, Color.yellow, RAYCAST_BUFFER, true);
                bool foundHit = Physics.SphereCast(pos, 0.8f, dir, out hitInfo, m_zombieController.AttackRange);
                if (foundHit)
                {
                    GameObject other = hitInfo.collider.gameObject;
                    if (other.CompareTag("Player"))
                    {
                        m_zombieController.stateMachine.ChangeState("BossZombieAttack");
                        return;
                    }
                }
            }
        }

        // Set destination buffer
        m_setDestBuffer -= Time.deltaTime;
        if (m_setDestBuffer <= 0f)
        {
            m_setDestBuffer = SET_DEST_BUFFER;
            m_navMeshAgent.SetDestination( m_playerInfo.pos );
        }

        // If it's been too long since seeing the player,
        // give up and go back to patrol
        /*m_chaseTime += Time.deltaTime;
        if (m_chaseTime > CHASE_TIME)
        {
            m_navMeshAgent.ResetPath();
            m_zombieController.stateMachine.ChangeState("BossZombiePatrol");
        }*/
    }

    public override void OnStateExit()
    {
    }

    public override string GetStateID()
    {
        return "BossZombieChase";
    }

    private float DistFromPlayer()
    {
        Vector3 playerPos = m_playerInfo.pos;
        Vector3 myPos = m_zombieController.transform.position;

        return (playerPos - myPos).magnitude;
    }
}
