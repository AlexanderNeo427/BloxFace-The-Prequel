using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject shotgun;
    [SerializeField] private GameObject sniper;
    [SerializeField] private GameObject machineGun;

    private List<GameObject> m_weaponList;

    private void Start()
    {
        m_weaponList = new List<GameObject>();
        m_weaponList.Add( pistol );
        m_weaponList.Add( shotgun );
        m_weaponList.Add( sniper );
        m_weaponList.Add( machineGun );

        foreach (GameObject gun in m_weaponList)
            gun.SetActive( false );

        pistol.SetActive( true );
    }

    private void Update()
    {
        
    }

    public void SwitchNextWeapon()
    {

    }

    public void SwitchPrevWeapon()
    {

    }

    public void Reload()
    {

    }

    public void Fire()
    {

    }
}
