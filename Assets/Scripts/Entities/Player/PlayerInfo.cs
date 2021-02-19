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

    public GameObject pistol;
    public GameObject shotgun;
    public GameObject sniper;
    public GameObject machineGun;
    public static int ammo = 100;
    private float shotgunDist = 0.25f;
    public static int grenadeAmount = 3;

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

    public void TakeDamage(float dmg)
    {
        m_health -= dmg;
    }

    public void GainHP(float hp)
    {
        m_health += hp;
    }
}
