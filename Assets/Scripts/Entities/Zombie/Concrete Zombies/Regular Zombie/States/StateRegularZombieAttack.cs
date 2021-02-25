using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateRegularZombieAttack : State
{
    private RegularZombie m_zombieController;
    private NavMeshAgent  m_navMeshAgent;
    private PlayerInfo    m_playerInfo;
    private float         m_chaseDistance;
    private float         m_timePerHit;
    private float         m_attackTimer;
    private float         m_rotationSpeed;

    public StateRegularZombieAttack(RegularZombie zombieController,
                                    PlayerInfo    playerInfo)
    {
        m_zombieController = zombieController;
        m_navMeshAgent     = zombieController.GetComponent<NavMeshAgent>();
        m_playerInfo       = playerInfo;
    }

    public override void OnStateEnter()
    {
        m_chaseDistance = m_zombieController.AttackRange;
        m_timePerHit = m_zombieController.AttackSpeed;
        m_attackTimer = 0f;

        m_rotationSpeed = m_navMeshAgent.angularSpeed;
        m_navMeshAgent.isStopped = true;
        m_navMeshAgent.autoBraking = true;
    }

    public override void OnStateUpdate()
    {
        // State transition
        bool PlayerOutOfRange = DistFromPlayer() > m_chaseDistance;
        if (PlayerOutOfRange)
            m_zombieController.stateMachine.ChangeState("RegularZombieChase");

        // Attack
        m_attackTimer -= Time.deltaTime;
        if (m_attackTimer <= 0f)
        {
            m_attackTimer = m_timePerHit;
            m_zombieController.Attack();
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
    }

    public override void OnStateExit()
    {
    }

    public override string GetStateID()
    {
        return "RegularZombieAttack";
    }

    private float DistFromPlayer()
    {
        Vector3 playerPos = m_playerInfo.pos;
        Vector3 myPos = m_zombieController.transform.position;

        return (playerPos - myPos).magnitude;
    }
}
