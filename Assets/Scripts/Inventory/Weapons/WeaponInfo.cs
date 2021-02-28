using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Make sure this script is attached to the player
 */

public class WeaponInfo : MonoBehaviour
{
    public static WeaponInfo instance;

    public GameObject pistol;
    public GameObject shotgun;
    public GameObject sniper;
    public GameObject machineGun;

    public static bool SGAccess = false;
    public static bool SPAccess = false;
    public static bool MGAccess = false;

    public static int ammo = 150; // ammo starts at 150
    public static int MaxAmmo = 20; // maxAmmo starts at 20
    public float shotgunDist = 0.062f;
    public static int grenadeAmount = 3;
    public static float reloadTime = 1.5f;
    public static bool reloadAffirm = false;
    public static float wT = 0f;
    public bool In = false;

    private PlayerMove m_playerMove;

    private void OnEnable()
    {
        Pickup.OnPickupAmmo += IncreaseMaxAmmo;
    }
    private void OnDisable()
    {
        Pickup.OnPickupAmmo -= IncreaseMaxAmmo;
    }

    void IncreaseMaxAmmo()
    {
        MaxAmmo += 15; // Ammo increases by 15
    }

    // Start is called before the first frame update
    void Start()
    {
        pistol.SetActive(true);
        shotgun.SetActive(false);
        sniper.SetActive(false);
        machineGun.SetActive(false);

        m_playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) // Sets the pistol as active weapon
        {
            m_playerMove.ResetMoveSpeed();
            //GetComponent<AudioSource>().Play();
            pistol.SetActive(true);
            shotgun.SetActive(false);
            sniper.SetActive(false);
            machineGun.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            m_playerMove.ResetMoveSpeed();
            if (SGAccess) // Sets the shotgun as active weapon if shotgun was picked up
            {
                //GetComponent<AudioSource>().Play();
                pistol.SetActive(false);
                shotgun.SetActive(true);
            }
            else // else the pistol is set active
            {
                pistol.SetActive(true);
                shotgun.SetActive(false);
            }
            sniper.SetActive(false);
            machineGun.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            m_playerMove.ResetMoveSpeed();
            shotgun.SetActive(false);
            if (SPAccess) // Sets the sniper as active weapon if sniper was picked up
            {
                //GetComponent<AudioSource>().Play();
                pistol.SetActive(false);
                sniper.SetActive(true);
            }
            else // else the pistol is set active
            {
                pistol.SetActive(true);
                sniper.SetActive(false);
            }
            machineGun.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            shotgun.SetActive(false);
            sniper.SetActive(false);
            if (MGAccess) // Sets the minigun as active weapon if minigun was picked up
            {
                //GetComponent<AudioSource>().Play();
                pistol.SetActive(false);
                machineGun.SetActive(true);
                m_playerMove.SetMoveSpeed(9.75f);
            }
            else // else the pistol is set active
            {
                pistol.SetActive(true);
                machineGun.SetActive(false);
                m_playerMove.ResetMoveSpeed();
            }
        }
        // This function puts knockback when shooting shotgun
        if (shotgun.activeSelf && Input.GetMouseButton(0) && ammo >= 16 && wT <= 0 && !WeaponInfo.reloadAffirm)
        {
            shotgunDist -= Time.deltaTime;
            if (shotgunDist > 0)
            {
                transform.Translate(Vector3.back * 12.5f * Time.deltaTime);
                if (In) // if player is colliding with a box, no knockback is applied
                {
                    transform.Translate(Vector3.forward * 12.5f * Time.deltaTime);
                }
            }
        }
        else
        {
            shotgunDist = 0.062f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall")) // Checks whether or not if colliding with box
        {
            In = true;
        }
        else
        {
            In = false;
        }
    }
}
