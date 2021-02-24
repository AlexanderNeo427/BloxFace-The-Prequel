using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Weapon controller script for teammateController
 *  
 *  All the guns are to be a child of whatever
 *  gameObject this script is attached to
 * 
 *  The teammateController can then grab a reference to this gameObject 
 *  and use the weapons through the exposed methods:
 *  
 *  - UseWeapon()
 *  - SwitchNextWeapon()
 *  - SwitchPrevWeapon()
 */
public class WeaponController : MonoBehaviour, Item
{
    [Header ("Insert all weapons here (from worst to best)")]
    [SerializeField] [Tooltip ("The first gun will be the default gun")]
    private List<GameObject> gunPrefabs;

    private IShootable  m_currGun;
    private int         m_currGunIndex;

    private void Start()
    {
        foreach (GameObject gun in gunPrefabs)
        {
            gun.SetActive( false );
            gun.GetComponent<IUnlockable>().UnlockWeapon();
        }

        m_currGunIndex = 0;
        m_currGun = gunPrefabs[m_currGunIndex].GetComponent<IShootable>();
        gunPrefabs[m_currGunIndex].GetComponent<IUnlockable>().UnlockWeapon();
        gunPrefabs[m_currGunIndex].SetActive( true );
    }

    public void UseWeapon()
    {
        m_currGun.Shoot();
    }

    public bool HasAmmo()
    {
        return gunPrefabs[m_currGunIndex].GetComponent<GunController>().HasAmmo();
    }

    public void UpgradeWeapon()
    {
        int nextIdx = m_currGunIndex + 1;
        if (nextIdx < gunPrefabs.Count)
        {
            m_currGun = gunPrefabs[nextIdx].GetComponent<IShootable>();
            gunPrefabs[m_currGunIndex].SetActive( false );
            gunPrefabs[nextIdx].SetActive( true );
            m_currGunIndex = nextIdx;
        }
    }

    public bool SwitchNextWeapon()
    {
        int nextIdx = m_currGunIndex + 1;
        if (nextIdx >= gunPrefabs.Count)
            nextIdx = 0;

        IUnlockable nextWeapon = gunPrefabs[nextIdx].GetComponent<IUnlockable>();
        if (!nextWeapon.IsUnlocked())
            return false;

        m_currGun = gunPrefabs[nextIdx].GetComponent<IShootable>();
        gunPrefabs[m_currGunIndex].SetActive( false );
        gunPrefabs[nextIdx].SetActive( true );
        m_currGunIndex = nextIdx;
        return true;
    }

    public bool SwitchPrevWeapon()
    {
        int prevIdx = m_currGunIndex - 1;
        if (prevIdx < 0)
            prevIdx = m_currGunIndex - 1;

        IUnlockable prevWeapon = gunPrefabs[prevIdx].GetComponent<IUnlockable>();
        if (!prevWeapon.IsUnlocked())
            return false;

        m_currGun = gunPrefabs[prevIdx].GetComponent<IShootable>();
        gunPrefabs[m_currGunIndex].SetActive( false );
        gunPrefabs[prevIdx].SetActive( true );
        m_currGunIndex = prevIdx;
        return true;
    }
}
