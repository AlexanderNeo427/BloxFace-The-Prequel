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
 *  and use the weapons through the exposed methods
 *  
 *  - SwitchPrevWeapon()
 *  - SwitchNextWeapon()
 *  - GetCurrentWeapon()
 *  - ReloadWeapon()
 *  - UseWeapon()
 */
public class WeaponController : MonoBehaviour
{
    [Header ("Insert all weapons here")]
    [SerializeField] [Tooltip ("The first gun will be the default gun")]
    private List<GameObject> weaponList;

    public class WeaponData
    {
        public GameObject weapon;
        public int        ammo;
        public int        maxAmmo;
        public bool       isUnlocked;

        public WeaponData(GameObject _weapon)
        {
            weapon     = _weapon;
            ammo       = 100;
            maxAmmo    = 100;
            isUnlocked = false;
        }
    }

    private List<WeaponData> m_listWeaponData;
    private int              m_currWeaponIndex;
    private float            m_reloadTime;
    private bool             m_reloadActive;

    private void Start()
    {
        m_listWeaponData = new List<WeaponData>();
        m_currWeaponIndex = 0;

        foreach (GameObject weapon in weaponList)
        {
            WeaponData weaponData = new WeaponData( weapon );
            weaponData.ammo = 0;
            weaponData.maxAmmo = 0;
            weapon.SetActive( false );
        }

        m_listWeaponData[m_currWeaponIndex].weapon.SetActive( true );
        m_listWeaponData[m_currWeaponIndex].isUnlocked = true;

        m_reloadTime = 1.5f;
        m_reloadActive = false;
    }

    private void Update()
    {
        if (m_reloadActive)
        {
            StartCoroutine(ReloadWeaponCoroutine(m_reloadTime));
        }
    }

    private IEnumerator ReloadWeaponCoroutine(float reloadTime)
    {
        yield return new WaitForSeconds( reloadTime );
        m_reloadActive = false;
        m_listWeaponData[m_currWeaponIndex].ammo = m_listWeaponData[m_currWeaponIndex].maxAmmo;

        yield return null;
    }

    public void UseWeapon()
    {
        if (!m_reloadActive)
        {
            PlayerShoot pistol = GetCurrentWeapon().GetComponent<PlayerShoot>();
            if (pistol != null)
                pistol.Shoot();

            MGScript machineGun = GetCurrentWeapon().GetComponent<MGScript>();
            if (machineGun != null)
                machineGun.Shoot();

            ShotgunScript shotgun = GetCurrentWeapon().GetComponent<ShotgunScript>();
            if (shotgun != null)
                shotgun.Shoot();

            SniperScript sniper = GetCurrentWeapon().GetComponent<SniperScript>();
            if (sniper != null)
                sniper.Shoot();
        }
    }

    public void ReloadWeapon()
    {
        m_reloadActive = true;
    }

    public bool SwitchNextWeapon()
    {
        int nextIdx = m_currWeaponIndex + 1;
        if (nextIdx >= m_listWeaponData.Count)
            nextIdx = 0;

        if (!m_listWeaponData[nextIdx].isUnlocked)
            return false;

        m_listWeaponData[m_currWeaponIndex].weapon.SetActive( false );
        m_listWeaponData[nextIdx].weapon.SetActive( true );
        m_currWeaponIndex = nextIdx;
        return true;
    }

    public bool SwitchPrevWeapon()
    {
        int nextIdx = m_currWeaponIndex - 1;
        if (nextIdx < 0)
            nextIdx = m_listWeaponData.Count - 1;

        if (!m_listWeaponData[nextIdx].isUnlocked)
            return false;

        m_listWeaponData[m_currWeaponIndex].weapon.SetActive( false );
        m_listWeaponData[nextIdx].weapon.SetActive( true );
        m_currWeaponIndex = nextIdx;
        return true;
    }

    public GameObject GetCurrentWeapon()
    {
        return m_listWeaponData[m_currWeaponIndex].weapon;
    }
}
