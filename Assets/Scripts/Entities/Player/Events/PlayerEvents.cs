using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Script to dispatch player-related events
 *  
 *  Attach to empty gameObject, 
 *  preferably a child of the player
 */
public class PlayerEvents : MonoBehaviour
{
    public static event Action OnPlayerDeath;

    private PlayerInfo m_playerInfo;

    private void OnEnable()
    {
        // Subscribe to events 
    }

    private void Start()
    {
        GameObject player = transform.root.gameObject;
        m_playerInfo = player.GetComponent<PlayerInfo>();
    }

    private void Update()
    {
        CheckDeath();
    }

    private void OnDisable()
    {
        // Unsubscribe from events 

    }

    public void DispatchDamagedEvent()
    {
        // 
    }

    private void CheckDeath()
    {
        if (m_playerInfo.HP <= 0.0f)
            OnPlayerDeath?.Invoke();   
    }
}
