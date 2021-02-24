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
        m_navMeshAgent.speed *= 0.3f;

        m_raycastBuffer = RAYCAST_BUFFER;
    }

    public override void OnStateUpdate()
    {
        // State transition
        bool playerOutOfRange = DistFromPlayer() > m_attackRange;
        if (playerOutOfRange)
            m_zombieController.stateMachine.ChangeState("BossZombieChase");

        // Attack
        m_attackTimer += Time.deltaTime;
        if (m_attackTimer >= m_attackSpeed)
        {
            m_zombieController.Attack();
            m_attackTimer = 0f;
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
            Vector3 dir = (pos - m_playerInfo.pos).normalized;
            float dist = m_zombieController.DetectionRange;
            RaycastHit hitInfo;

            Debug.DrawRay(pos, dir * dist, Color.red, RAYCAST_BUFFER);
            bool hitFound = Physics.Raycast(pos, dir, out hitInfo, dist);
            if (hitFound)
            {
                GameObject other = hitInfo.collider.gameObject;
                bool canSeePlayer = other.CompareTag("Player");

                // If player not spotted, have 50/50 chance of
                // either chasing, or going back to patrol
                if (!canSeePlayer)
                {
                    float rand = Random.Range(0f, 100f);

                    if (rand <= 50f)
                        m_zombieController.stateMachine.ChangeState("BossZombieChase");
                    else
                        m_zombieController.stateMachine.ChangeState("BossZombiePatrol");
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
