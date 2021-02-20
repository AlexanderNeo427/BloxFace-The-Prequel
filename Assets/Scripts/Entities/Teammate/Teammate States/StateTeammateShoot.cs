using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateTeammateShoot : State
{
    private TeammateController m_controller;
    private NavMeshAgent       m_navMeshAgent;
    private Zombie             m_enemy;
    private WeaponController   m_weaponController;
    private float              m_originalMoveSpeed;
    private float              m_atkMoveSpeed;

    public StateTeammateShoot(TeammateController teammateController)
    {
        m_controller = teammateController;
        m_navMeshAgent = m_controller.GetComponent<NavMeshAgent>();
        m_weaponController = m_controller.m_weaponController;
    }

    public override void OnStateEnter()
    {   
    }

    public override void OnStateUpdate()
    {
    }

    public override void OnStateExit()
    {
    }

    public override string GetStateID()
    {
        return "TeammateShoot";
    }
}
