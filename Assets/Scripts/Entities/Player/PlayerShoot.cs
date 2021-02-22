using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletSpawnPoint;
    public float waitTime;
    private float wT;
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
        // Shooting - G
        if (Input.GetMouseButtonDown(0) /*&& PlayerInfo.ammo > 0*/ && wT <= 0 /*&& !PlayerInfo.reloadAffirm*/)
        {
            Shoot();
            //PlayerInfo.ammo = PlayerInfo.ammo - 2;
            wT = waitTime;
        }

        wT -= 1 * Time.deltaTime;
    }

    // Shooting function - G
    public void Shoot()
    {
        Quaternion rotation = Quaternion.Euler(m_playerInfo.dir.x, m_playerInfo.dir.y, m_playerInfo.dir.z);
        GameObject goBullet = Instantiate(bullet, bulletSpawnPoint.transform.position, rotation);
        goBullet.transform.forward = m_playerInfo.dir;
    }
}
