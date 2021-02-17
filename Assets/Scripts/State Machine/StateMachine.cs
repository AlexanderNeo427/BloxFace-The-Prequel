using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private State m_currentState;
    private State m_nextState;

    private Dictionary<string, State> m_stateMap;

    public StateMachine()
    {
        m_currentState = null;
        m_nextState = null;

        m_stateMap = new Dictionary<string, State>();
    }

    public bool AddState(State newState)
    {
        if (newState == null)
            return false;

        m_stateMap.Add(newState.GetStateID(), newState);

        if (m_currentState == null)
            m_nextState = m_currentState = newState;

        return true;
    }

    public string GetCurrentState()
    {
        if (m_currentState != null)
            return m_currentState.GetStateID();

        return "Current state is null";
    }

    public bool ChangeState(string nextState)
    {
        if (m_stateMap.ContainsKey(nextState))
        {
            m_nextState = m_stateMap[nextState];
            return true;
        }
        return false;
    }

    public void Update()
    {
        if (m_currentState != m_nextState)
        {
            m_currentState.OnStateExit();
            m_currentState = m_nextState;
            m_currentState.OnStateEnter();
        }
        m_nextState.OnStateUpdate();
    }
}
