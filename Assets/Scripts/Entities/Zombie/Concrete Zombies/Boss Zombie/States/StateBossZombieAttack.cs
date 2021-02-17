using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBossZombieAttack : State      
{
    private BossZombie m_zombieController;
    private PlayerInfo m_playerInfo;
    private float      m_attackRange;
    private float      m_attackSpeed;
    private float      m_attackTimer;

    public StateBossZombieAttack(BossZombie zombieController,
                                 PlayerInfo playerInfo, 
                                 float      attackRange, 
                                 float      attackSpeed)
    {
        m_zombieController = zombieController;
        m_playerInfo       = playerInfo;
        m_attackRange      = attackRange;
        m_attackSpeed      = attackSpeed;
        m_attackTimer      = 0f;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateUpdate()
    {
        m_attackTimer += Time.deltaTime;
        if (m_attackTimer >= m_attackSpeed)
        {
            m_zombieController.Attack();
            m_attackTimer = 0f;
        }

        bool playerOutOfRange = DistFromPlayer() > m_attackRange;
        if (playerOutOfRange)
            m_zombieController.stateMachine.ChangeState("BossZombieChase");

        Debug.Log( "Boss attack" );
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
