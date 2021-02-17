﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGScript : MonoBehaviour
{
    public GameObject bulletSpawnPoint;
    public float waitTime;
    private float wT;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        wT = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Shooting - G
        if (Input.GetMouseButton(0) && PlayerInfo.ammo > 0 && wT <= 0)
        {
            Shoot();
            PlayerInfo.ammo--;
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