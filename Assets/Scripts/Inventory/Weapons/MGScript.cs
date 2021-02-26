using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGScript : MonoBehaviour
{
    public GameObject bulletSpawnPoint;
    public float waitTime;
    private float wT;
    public GameObject bullet;
    private PlayerMove m_playerMove;

    // Start is called before the first frame update
    void Start()
    {
        wT = waitTime;
        m_playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        // Shooting - G
        if (Input.GetMouseButtonDown(0) && WeaponInfo.ammo > 0 && wT <= 0 && !WeaponInfo.reloadAffirm)
        {
            Shoot();
            GetComponent<AudioSource>().Play();
            WeaponInfo.ammo--;
            wT = waitTime;
        }
        wT -= 1 * Time.deltaTime;
    }

    // Shooting function - G
    public void Shoot()
    {
        Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
    }
}
