using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Make sure this script is attached to the player
 */
public class PlayerInfo : MonoBehaviour, Entity
{
    [SerializeField] [Range (50f, 250f)]
    private float playerHealth = 100f;

    private float m_maxHP;
    private float m_health;

    private PlayerMove m_playerMove;

    public GameObject pistol;
    public GameObject shotgun;
    public GameObject sniper;
    public GameObject machineGun;
    public static int ammo = 100;
    private float shotgunDist = 0.25f;
    public static int grenadeAmount = 3;

    // Getters
    public float   MaxHP { get { return m_maxHP; } }
    public float   HP    { get { return m_health; } }
    public Vector3 pos   { get { return transform.position; } }
    public Vector3 dir   { get { return transform.forward; } }
    public float   Speed { get { return m_playerMove.GetMoveSpeed(); } }

    // Broadcast this entity's death
    // (Caleb's SceneManager needs to subscribe to this)
    // And bring us back to the main menu
    public static event Action OnDeath;

    private void Awake()
    {
        // Set player starting position
        transform.position = new Vector3(0f, transform.localScale.y, 0f);

        m_maxHP  = playerHealth;
        m_health = playerHealth;

        m_playerMove = this.gameObject.GetComponent<PlayerMove>();

        pistol.SetActive(true);
        shotgun.SetActive(false);
        sniper.SetActive(false);
        machineGun.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            pistol.SetActive(true);
            shotgun.SetActive(false);
            sniper.SetActive(false);
            machineGun.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            pistol.SetActive(false);
            shotgun.SetActive(true);
            sniper.SetActive(false);
            machineGun.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            pistol.SetActive(false);
            shotgun.SetActive(false);
            sniper.SetActive(true);
            machineGun.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            pistol.SetActive(false);
            shotgun.SetActive(false);
            sniper.SetActive(false);
            machineGun.SetActive(true);
        }
        if (Input.GetKey(KeyCode.R))
        {
            ammo = 100;
        }
        if (shotgun.activeSelf && Input.GetMouseButton(0) && ammo > 0)
        {
            shotgunDist -= Time.deltaTime;
            if (shotgunDist > 0)
            {
                transform.Translate(Vector3.back * 25f * Time.deltaTime);
            }
        }
        else
        {
            shotgunDist = 0.25f;
        }
        //if (machineGun.activeSelf && Input.GetMouseButton(0))
        //{
        //    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || 
        //        Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        //    {
        //        transform.Translate(Vector3.back * 5f * Time.deltaTime);
        //    }
        //}
    }

    public float GetMaxHP()
    {
        return MaxHP;
    }

    public float GetCurrentHP()
    {
        return m_health;
    }

    public void TakeDamage(float dmg)
    {
        m_health -= dmg;

        if (m_health <= 0f)
        {
            OnDeath?.Invoke();
        }
    }

    public void GainHP(float hp)
    {
        m_health += hp;
        m_health = Mathf.Max( m_health, m_maxHP );
    }
}
