using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateRegularZombieAttack : State
{
    private RegularZombie m_zombieController;
    private PlayerInfo    m_playerInfo;
    private float         m_chaseDistance;
    private float         m_timePerHit;
    private float         m_attackTimer;

    public StateRegularZombieAttack(RegularZombie zombieController,
                                   PlayerInfo     playerInfo,
                                   float          chaseDistance,
                                   float          attackspeed)
    {
        m_zombieController = zombieController;
        m_playerInfo       = playerInfo;
        m_chaseDistance    = chaseDistance;
        m_timePerHit       = attackspeed;
        m_attackTimer      = 0f;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateUpdate()
    {
        m_attackTimer -= Time.deltaTime;
        if (m_attackTimer <= 0f)
        {
            m_attackTimer = m_timePerHit;
            m_zombieController.Attack();
        }

        bool PlayerOutOfRange = DistFromPlayer() > m_chaseDistance;
        if (PlayerOutOfRange)
            m_zombieController.stateMachine.ChangeState("RegularZombieChase");
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
