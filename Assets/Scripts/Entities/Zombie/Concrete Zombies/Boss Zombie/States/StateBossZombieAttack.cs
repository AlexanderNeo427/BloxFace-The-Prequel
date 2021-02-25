using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateBossZombieAttack : State      
{
    private const float  RAYCAST_BUFFER = 0.25f;

    private BossZombie   m_zombieController;
    private NavMeshAgent m_navMeshAgent;
    private PlayerInfo   m_playerInfo;
    private float        m_attackRange;
    private float        m_attackSpeed;
    private float        m_attackTimer;
    private float        m_rotationSpeed;
    private float        m_raycastBuffer;

    public StateBossZombieAttack(BossZombie zombieController,
                                 PlayerInfo playerInfo)
    {
        m_zombieController = zombieController;
        m_navMeshAgent     = zombieController.GetComponent<NavMeshAgent>();
        m_playerInfo       = playerInfo;
    }

    public override void OnStateEnter()
    {
        m_attackRange = m_zombieController.AttackRange;
        m_attackSpeed = m_zombieController.AttackSpeed;
        m_attackTimer = 0f;

        m_rotationSpeed = m_navMeshAgent.angularSpeed;
        m_navMeshAgent.isStopped = true;

        // Nav mesh agent speed
        // m_navMeshAgent.speed *= 0.3f;
        m_raycastBuffer = RAYCAST_BUFFER;
    }

    public override void OnStateUpdate()
    {
        // Attack
        m_attackTimer -= Time.deltaTime;
        if (m_attackTimer <= 0f)
        {
            m_zombieController.Attack();
            m_attackTimer = m_attackSpeed;
        }

        // Rotate towards player
        Vector3 targetDir = m_playerInfo.pos - m_zombieController.transform.position;
        targetDir.y = 0;
        targetDir.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
        Quaternion currentRotation = m_zombieController.transform.rotation;
        float rotationStep = m_rotationSpeed * Time.deltaTime;

        Quaternion newRotation = Quaternion.RotateTowards(currentRotation, 
                                                          targetRotation,
                                                          rotationStep);

        m_zombieController.transform.rotation = newRotation;

        // Check if line-of-sight to player being blocked
        m_raycastBuffer -= Time.deltaTime;
        if (m_raycastBuffer <= 0f)
        {
            m_raycastBuffer = RAYCAST_BUFFER;

            Vector3 pos = m_zombieController.transform.position;
            pos.y = 1f;

            Vector3 playerPos = m_playerInfo.pos;
            playerPos.y = 1f;

            Vector3 dir = (playerPos - pos).normalized;
            RaycastHit hitInfo;

            Debug.DrawRay(pos, dir * m_zombieController.AttackRange, Color.red, RAYCAST_BUFFER, true);
            bool hitFound = Physics.SphereCast(pos, 0.8f, dir, out hitInfo, m_zombieController.AttackRange);

            if (hitFound)
            {
                GameObject other = hitInfo.collider.gameObject;
                bool canSeePlayer = other.CompareTag("Player");

                if (!canSeePlayer)
                {
                    m_zombieController.stateMachine.ChangeState("BossZombieChase");
                    return;
                }
            }
        }
    }

    public override void OnStateExit()
    {
    }

    public override string GetStateID()
    {
        return "BossZombieAttack";
    }

    private float DistFromPlayer()
    {
        Vector3 playerPos = m_playerInfo.pos;
        Vector3 myPos = m_zombieController.transform.position;

        return (playerPos - myPos).magnitude;
    }
}
