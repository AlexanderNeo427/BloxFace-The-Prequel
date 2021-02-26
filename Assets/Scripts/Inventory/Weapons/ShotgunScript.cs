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
#if UNITY_STANDALONE
        // Shooting - G
        if (Input.GetMouseButtonDown(0) && WeaponInfo.ammo >= 16 && WeaponInfo.wT <= 0 && !WeaponInfo.reloadAffirm)
        {
            Shoot();
            AudioManager.instance.Play("Shotgun");
            WeaponInfo.ammo -= 8;
            WeaponInfo.wT = waitTime;
        }
#endif  
        WeaponInfo.wT -= 1 * Time.deltaTime;
    }

    // Shooting function - G
    public void Shoot()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                switch (Input.GetTouch(i).phase)
                {
                    case TouchPhase.Began:
                        {
                            if (WeaponInfo.ammo >= 8 && WeaponInfo.wT <= 0 && !WeaponInfo.reloadAffirm)
                            {
                                for (int j = 0; j < 25; ++j)
                                {
                                    Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
                                }

                                AudioManager.instance.Play("Shotgun");
                                WeaponInfo.ammo -= 8;
                                WeaponInfo.wT = waitTime;
                            }
                            break;
                        }
                }
            }
        }
#endif

#if UNITY_STANDALONE
        for (int i = 0; i < 25; ++i)
        {
            Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        }
#endif

    }
}
