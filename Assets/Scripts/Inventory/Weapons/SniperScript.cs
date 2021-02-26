using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperScript : MonoBehaviour
{
    public GameObject bulletSpawnPoint;
    public float waitTime;
    public float wT;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        wT = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE
        // Shooting - G
        if (Input.GetMouseButtonDown(0) && WeaponInfo.ammo >= 4 && wT <= 0 && !WeaponInfo.reloadAffirm)
        {
            Shoot();
            GetComponent<AudioSource>().Play();
            WeaponInfo.ammo = WeaponInfo.ammo - 4;
            wT = waitTime;
        }
#endif
        wT -= 1 * Time.deltaTime;
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
                            if (wT <= 0 && WeaponInfo.ammo >= 4 && !WeaponInfo.reloadAffirm)
                            {            
                                Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);                                
                                WeaponInfo.ammo -= 4;
                                wT = waitTime;
                                GetComponent<AudioSource>().Play();
                            }
                            break;
                        }
                }
            }
        }
#endif

#if UNITY_STANDALONE
        Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
#endif
    }
}
