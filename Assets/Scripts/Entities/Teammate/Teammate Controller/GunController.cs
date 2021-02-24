﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Generic gun controller script
 *  using scriptableObjects
 *  
 *  Drag this onto the
 *  teammates' guns
 */
public class GunController : MonoBehaviour, IShootable, IUnlockable
{
    [SerializeField] private Gun        gun;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform  bulletSpawnPoint;

    private int   m_ammo;
    private bool  m_reloadActive;
    private float m_shootBuffer;
    private bool  m_isUnlocked;

    private AudioSource m_audioSource;

    private void Start()
    {
        m_ammo         = gun.MaxAmmo;
        m_reloadActive = false;
        m_shootBuffer  = 0;
        m_isUnlocked   = false;
        m_audioSource  = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (m_shootBuffer > 0f)
            m_shootBuffer -= Time.deltaTime;

        if (m_reloadActive)
            StartCoroutine(this.ReloadCoroutine());
    }

    public void Shoot()
    {
        // Early exit cond
        if (!ReadyToShoot()) return;

        // Shooting
        m_shootBuffer = gun.FireRate;
        int numSpawnBullets = gun.BulletsPerShot;

        if (m_ammo < gun.BulletsPerShot)
        {
            numSpawnBullets = gun.BulletsPerShot - m_ammo;
            // m_reloadActive = true;
        }

        this.PlayWeaponSound();
        for (int i = 0; i < numSpawnBullets; ++i)
        {
            float randAngle = Random.Range(-gun.Spread * 0.5f, gun.Spread * 0.5f);
            Quaternion randRotate = Quaternion.Euler(0f, randAngle, 0f);
            Quaternion rootRotate = transform.root.rotation;
            Quaternion bulletRotation = randRotate * rootRotate;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletRotation);
        }
    }

    public void PlayWeaponSound()
    {
        m_audioSource.Play();
    }

    public bool HasAmmo()
    {
        return m_ammo <= 0;
    }

    public void Reload()
    {
        StartCoroutine( this.ReloadCoroutine() );
    }

    public int GetMaxAmmo()
    {
        return gun.MaxAmmo;
    }

    public bool IsUnlocked()
    {
        return m_isUnlocked;
    }

    public void UnlockWeapon()
    {
        m_isUnlocked = true;
    }

    private IEnumerator ReloadCoroutine()
    {
        m_reloadActive = true;
        yield return new WaitForSeconds(gun.ReloadTime);

        m_ammo = gun.MaxAmmo;
        m_reloadActive = false;
        yield return null;
    }

    private bool ReadyToShoot()
    {
        return !m_reloadActive && (m_shootBuffer <= 0f);
    }
}
