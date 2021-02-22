/*
 *  No longer being used
 *  
 *  If you see this during the merge, _delete dis_
 */

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateTeammateIdle : State
{
    private TeammateController m_controller;
    private PlayerInfo         m_playerInfo;

    // If get too far from player, Teammate will start to follow
    private const float        m_followThreshold = 15f; 

    public StateTeammateIdle(TeammateController teammateController,
                             PlayerInfo         playerInfo)
    {
        m_controller   = teammateController;
        m_playerInfo   = playerInfo;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateUpdate()
    {
        Vector3 myPos = m_controller.transform.position;
        Vector3 playerPos = m_playerInfo.pos;

        float distFromPlayer = (myPos - playerPos).magnitude;

        if (distFromPlayer >= m_followThreshold)
            m_controller.stateMachine.ChangeState("TeammateFollowPlayer");
    }

    public override void OnStateExit()
    {
    }

    public override string GetStateID()
    {
        return "TeammateIdle";
    }
}
*/