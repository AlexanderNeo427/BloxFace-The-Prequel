using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateTeammateShoot : State
{
    // How often to set NavMesh destination, because setting every frame is probably bad
    private const float        SET_DEST_BUFFER = 0.8f;

    // How often to perform enemy check (cause doing it every frame is probably bad)
    private const float        RAYCAST_BUFFER = 0.333f;

    // How many rays (around the teammate) to cast (to check for enemies)
    private const int          NUM_RAYS = 20;

    private TeammateController m_controller;
    private NavMeshAgent       m_navMeshAgent;
    private Zombie             m_enemy;
    private WeaponController   m_weaponController;
    private float              m_originalMoveSpeed;
    private float              m_atkMoveSpeed;
    private float              m_setDestBuffer;
    private float              m_raycastBuffer;

    public StateTeammateShoot(TeammateController teammateController)
    {
        m_controller = teammateController;
        m_navMeshAgent = m_controller.GetComponent<NavMeshAgent>();
        m_weaponController = m_controller.m_weaponController;
    }

    public override void OnStateEnter()
    {
        if ((Zombie)m_controller.m_enemy == null)
        {
            m_controller.m_stateMachine.ChangeState("TeammatePatrol");
            return;
        }
        else
            m_enemy = m_controller.m_enemy;

        // Slower moveSpeed while shooting
        m_navMeshAgent.isStopped = false;
        m_navMeshAgent.autoBraking = true;
        m_navMeshAgent.speed = m_controller.MoveSpeed * 0.333f;

        m_setDestBuffer = SET_DEST_BUFFER;
        m_raycastBuffer = RAYCAST_BUFFER;
    }

    public override void OnStateUpdate()
    {
        // State transition(s)
        if ((Zombie)m_enemy == null)
        {
            m_controller.m_stateMachine.ChangeState("TeammatePatrol");
            return;
        }
        if ((GameObject)m_enemy.GetGameObject() == null)
        {
            m_controller.m_stateMachine.ChangeState("TeammatePatrol");
            return;
        }

        if (DistFromEnemy() > m_controller.DetectionRange)
        {
            m_controller.m_stateMachine.ChangeState("TeammateChaseEnemy");
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

        // Check if objects blocking line-of-sight
        // If so, change into "chase" state
        m_raycastBuffer -= Time.deltaTime;
        if (m_raycastBuffer <= 0f)
        {
            Vector3 rayOrigin = m_controller.transform.position;
            Vector3 rayDir = targetDir;
            Ray ray = new Ray(rayOrigin, rayDir);
            float dist = m_controller.DetectionRange;
            RaycastHit hitInfo;

            Debug.DrawRay(rayOrigin, rayDir * dist, Color.magenta, RAYCAST_BUFFER, true);

            bool hit = Physics.Raycast(ray, out hitInfo, dist);
            if ( hit )
            {
                GameObject other = hitInfo.collider.gameObject;
                bool canSeeEnemy = other.gameObject.CompareTag("Enemy");

                if (!canSeeEnemy)
                    m_controller.m_stateMachine.ChangeState("TeammateChaseEnemy");
            }
            /*RaycastHit hitInfo;
            bool foundEnemy = false;
            float FOV = 70f;
            float dTheta = FOV / NUM_RAYS;
            Vector3 rayOrigin = m_controller.transform.position;
            Vector3 rayDir = m_controller.transform.forward;
            rayDir = Quaternion.Euler(0f, -FOV * 0.5f, 0f) * rayDir;

            for (int i = 0; i < NUM_RAYS; ++i)
            {
                Debug.DrawRay(rayOrigin, rayDir * m_controller.DetectionRange, Color.magenta, RAYCAST_BUFFER, true);
                bool foundHit = Physics.Raycast(rayOrigin, rayDir, out hitInfo, m_controller.DetectionRange);
                if (foundHit)
                {
                    GameObject other = hitInfo.collider.gameObject;
                    if (other.gameObject.CompareTag("Enemy"))
                    {
                        Zombie zombieComp = other.GetComponent<Zombie>();
                        if (zombieComp == m_enemy)
                        {
                            foundEnemy = true;
                            break;
                        }
                    }
                }
                rayDir = Quaternion.Euler(0f, dTheta, 0f) * rayDir;
            }

            if (!foundEnemy)
                m_controller.m_stateMachine.ChangeState("TeammateChaseEnemy");*/
        }

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
    }

    public override string GetStateID()
    {
        return "TeammateShoot";
    }

    // Helper
    private float DistFromEnemy()
    {
        GameObject enemyGO = m_enemy.GetGameObject();
        Vector3 enemyPos = enemyGO.transform.position;
        Vector3 myPos = m_controller.transform.position;
        enemyPos.y = myPos.y = 0f;

        return Vector3.Distance( enemyPos, myPos );
    }
}
