using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : MonoBehaviour
{
    public GameObject bulletSpawnPoint;
    public float waitTime;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        WeaponInfo.wT = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Shooting - G
        if (Input.GetMouseButtonDown(0) && WeaponInfo.ammo >= 16 && WeaponInfo.wT <= 0 && !WeaponInfo.reloadAffirm)
        {
            Shoot();
            GetComponent<AudioSource>().Play();
            WeaponInfo.ammo = WeaponInfo.ammo - 16;
            WeaponInfo.wT = waitTime;
        }
        WeaponInfo.wT -= 1 * Time.deltaTime;
    }

    // Shooting function - G
    public void Shoot()
    {
        for (int i = 0; i < 25; ++i)
        {
            Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        }
    }
}
