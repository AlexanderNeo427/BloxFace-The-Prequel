using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateRunnerZombieChase : State
{
    // Don't wanna keep recalculating pathfinding every frame
    private const float SET_DEST_BUFFER = 0.8f;

    private RunnerZombie m_zombieController;
    private NavMeshAgent m_navMeshAgent;
    private PlayerInfo m_playerInfo;
    private float m_setDestBuffer;
    private float m_setSpeedBuffer;

    public StateRunnerZombieChase(RunnerZombie zombieController,
                                   PlayerInfo playerInfo)
    {
        m_zombieController = zombieController;
        m_navMeshAgent = zombieController.GetComponent<NavMeshAgent>();
        m_playerInfo = playerInfo;
    }
    public override void OnStateEnter()
    {
        m_navMeshAgent.ResetPath();
        m_navMeshAgent.isStopped = false;
        m_navMeshAgent.updatePosition = true;
        m_navMeshAgent.updateRotation = true;

        m_setSpeedBuffer = 0f;
        m_setDestBuffer = SET_DEST_BUFFER;
    }

    public override void OnStateUpdate()
    {
        // State transition(s)
        bool PlayerWithinRange = (DistFromPlayer() <= m_navMeshAgent.stoppingDistance + 0.1f);
        if (PlayerWithinRange)
            m_zombieController.stateMachine.ChangeState("RunnerZombieAttack");

        // Set destination buffer
        m_setDestBuffer -= Time.deltaTime;
        if (m_setDestBuffer <= 0f)
        {
            m_setDestBuffer = SET_DEST_BUFFER;
            m_navMeshAgent.SetDestination(m_playerInfo.pos);
        }

        // Set random speed every few seconds
/*        m_setSpeedBuffer -= Time.deltaTime;
        if (m_setSpeedBuffer <= 0f)
        {
            m_setSpeedBuffer = UnityEngine.Random.Range(2f, 6f);
            float newSpeed = m_zombieController.MoveSpeed + UnityEngine.Random.Range(-3f, 3f);
            m_navMeshAgent.speed = Mathf.Max(newSpeed, m_navMeshAgent.speed);
        }*/
    }

    public override void OnStateExit()
    {
    }

    public override string GetStateID()
    {
        return "RunnerZombieChase";
    }

    private float DistFromPlayer()
    {
        Vector3 playerPos = m_playerInfo.pos;
        Vector3 myPos = m_zombieController.transform.position;

        float distanceFromPlayer = (playerPos - myPos).magnitude;
        return distanceFromPlayer;
    }
}
