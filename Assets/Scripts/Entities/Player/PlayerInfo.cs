using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Make sure this script is attached to the player
 */
public class PlayerInfo : MonoBehaviour
{
    [SerializeField] [Range (50f, 250f)]
    private float playerHealth = 100f;

    private float m_health;

    // Getters
    public float   MaxHP { get { return playerHealth; } }
    public float   HP    { get { return m_health; } }
    public Vector3 pos   { get { return transform.position; } }
    public Vector3 dir   { get { return transform.forward; } }

    private void Awake()
    {
        // Set player starting position
        transform.position = new Vector3(0f, transform.localScale.y, 0f);

        m_health = playerHealth;
    }

    private void Update()
    {
        
    }

    public void TakeDamage(float dmg)
    {
        m_health -= dmg;
    }

    public void GainHP(float hp)
    {
        m_health += hp;
    }
}
