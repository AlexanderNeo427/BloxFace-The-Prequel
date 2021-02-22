using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateTeammateShoot : State
{
    private const float        SET_DEST_BUFFER = 0.8f;

    private TeammateController m_controller;
    private NavMeshAgent       m_navMeshAgent;
    private Zombie             m_enemy;
    private WeaponController   m_weaponController;
    private float              m_originalMoveSpeed;
    private float              m_atkMoveSpeed;
    private float              m_setDestBuffer;

    public StateTeammateShoot(TeammateController teammateController)
    {
        m_controller = teammateController;
        m_navMeshAgent = m_controller.GetComponent<NavMeshAgent>();
        m_weaponController = m_controller.m_weaponController;
    }

    public override void OnStateEnter()
    {
        if (m_controller.m_enemy == null)
        {
            m_controller.m_stateMachine.ChangeState("TeammatePatrol");
            return;
        }
        else
            m_enemy = m_controller.m_enemy;

        // Slower moveSpeed while shooting
        m_navMeshAgent.isStopped = false;
        m_navMeshAgent.autoBraking = true;
        m_navMeshAgent.speed = m_controller.MoveSpeed * 0.333333f;

        m_setDestBuffer = SET_DEST_BUFFER;
    }

    public override void OnStateUpdate()
    {
        // State transition(s)
        if (m_enemy.Equals( null ))
        {
            m_controller.m_stateMachine.ChangeState("TeammatePatrol");
            return;
        }

        GameObject enemyGO = m_enemy.GetGameObject();
        Entity enemy = enemyGO.GetComponent<Entity>();

        // Rotate towards enemy
        Vector3 enemyPos = enemyGO.transform.position;
        Vector3 myPos = m_controller.transform.position;
        Vector3 targetDir = (enemyPos - myPos).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
        Quaternion currentRotation = m_controller.transform.rotation;
        float rotationStep = m_navMeshAgent.angularSpeed * Time.deltaTime;

        Quaternion newRotation = Quaternion.RotateTowards(currentRotation,
                                                          targetRotation,
                                                          rotationStep);

        m_controller.transform.rotation = newRotation;

        // Fire gun
        m_weaponController.UseWeapon();

        // Set destination
        m_setDestBuffer -= Time.deltaTime;
        if (m_setDestBuffer <= 0f)
        {
            m_setDestBuffer = SET_DEST_BUFFER;
            m_navMeshAgent.SetDestination( enemyPos );
        }
    }

    public override void OnStateExit()
    {
        m_enemy = null;
    }

    public override string GetStateID()
    {
        return "TeammateShoot";
    }
}
