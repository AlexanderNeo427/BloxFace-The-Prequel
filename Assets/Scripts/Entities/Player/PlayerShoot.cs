using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletSpawnPoint;
    public float waitTime = 0.5f;
    public float wT = 0.5f;
    public GameObject bullet;

    private PlayerInfo m_playerInfo;

    // Start is called before the first frame update
    void Start()
    {
        m_playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();

        wT = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE
        // Shooting - G
        if (Input.GetMouseButtonDown(0) /*&& PlayerInfo.ammo > 0*/ && wT <= 0 && !WeaponInfo.reloadAffirm)
        {
            Shoot();
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
                    //When a touch has first been detected, change the message and record the starting position
                    case TouchPhase.Began:
                        if (wT <= 0)
                        {
                            Quaternion rotation = Quaternion.Euler(m_playerInfo.dir.x, m_playerInfo.dir.y, m_playerInfo.dir.z);
                            GameObject goBullet = Instantiate(bullet, bulletSpawnPoint.transform.position, rotation);
                            goBullet.transform.forward = m_playerInfo.dir;
                            
                            AudioManager.instance.Play("Pistol");
                            wT = waitTime;
                        }
                        break;
                }
            }
        }
#endif

#if UNITY_STANDALONE
        Quaternion rotation = Quaternion.Euler(m_playerInfo.dir.x, m_playerInfo.dir.y, m_playerInfo.dir.z);
        GameObject goBullet = Instantiate(bullet, bulletSpawnPoint.transform.position, rotation);
        goBullet.transform.forward = m_playerInfo.dir;
        AudioManager.instance.Play("Pistol");
#endif
    }
}
