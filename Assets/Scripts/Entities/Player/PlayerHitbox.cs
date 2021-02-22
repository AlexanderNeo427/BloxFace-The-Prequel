using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    private PlayerInfo m_playerInfo;

    private void Start()
    {
        m_playerInfo = transform.root.gameObject.GetComponent<PlayerInfo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Fireball fireball = other.GetComponent<Fireball>();
        if (fireball != null)
        {
            m_playerInfo.TakeDamage( fireball.Damage );
        }
    }
}
