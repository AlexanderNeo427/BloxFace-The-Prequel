using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Script to handle player-related events
 *  
 *  Attach to empty gameObject, 
 *  preferably a child of the player
 */
public class PlayerEventHandler : MonoBehaviour
{
    private PlayerInfo m_playerInfo;

    private void Awake()
    {
        m_playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
    }

    private void OnEnable()
    {
        // Subscribe to events
        Pickup.OnPickupHP += m_playerInfo.GainHP;
        RegularZombie.OnAttackPlayer += m_playerInfo.TakeDamage;
        RunnerZombie.OnAttackPlayer += m_playerInfo.TakeDamage;
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        Pickup.OnPickupHP -= m_playerInfo.GainHP;
        RegularZombie.OnAttackPlayer -= m_playerInfo.TakeDamage;
        RunnerZombie.OnAttackPlayer -= m_playerInfo.TakeDamage;
    }
}
