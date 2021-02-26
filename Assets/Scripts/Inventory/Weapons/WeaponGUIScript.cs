using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGUIScript : MonoBehaviour
{
    public static WeaponGUIScript instance;

    public GameObject pistolGO;
    public GameObject shotgunGO;
    public GameObject sniperGO;
    public GameObject MGGO;

    //public GameObject pistolUI;
    //public GameObject shotgunUI;
    //public GameObject sniperUI;
    //public GameObject MGUI;

    public GameObject TptI; // Translucent Pistol Image
    public GameObject TsgI; // Translucent Shotgun Image
    public GameObject TspI; // Translucent Sniper Image
    public GameObject TmgI; // Translucent MiniGun Image
    public GameObject RI; // Reloading Image
    public GameObject Ifin;  // Infinity Image

    public float time;
    public bool t;
    public float waitTime = 0.05f;
    public float wT = 0.05f;

    AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        //pistolUI.SetActive(true);
        //shotgunUI.SetActive(false);
        //sniperUI.SetActive(false);
        //MGUI.SetActive(false);

        TptI.SetActive(true);
        TspI.SetActive(false);
        TsgI.SetActive(false);
        TmgI.SetActive(false);
        RI.SetActive(false);
        Ifin.SetActive(true);

        time = 3f;
        t = false;

        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pistolGO.activeSelf)
        {
            //pistolUI.SetActive(true);
            //shotgunUI.SetActive(false);
            //sniperUI.SetActive(false);
            //MGUI.SetActive(false);

            TptI.SetActive(true);
            TsgI.SetActive(false);
            TspI.SetActive(false);
            TmgI.SetActive(false);
            Ifin.SetActive(true);
        }
        else if (shotgunGO.activeSelf)
        {
            //pistolUI.SetActive(false);
            //shotgunUI.SetActive(true);
            //sniperUI.SetActive(false);
            //MGUI.SetActive(false);

            TptI.SetActive(false);
            TsgI.SetActive(true);
            TspI.SetActive(false);
            TmgI.SetActive(false);
            Ifin.SetActive(false);
        }
        else if (sniperGO.activeSelf)
        {
            //pistolUI.SetActive(false);
            //shotgunUI.SetActive(false);
            //sniperUI.SetActive(true);
            //MGUI.SetActive(false);

            TptI.SetActive(false);
            TsgI.SetActive(false);
            TspI.SetActive(true);
            TmgI.SetActive(false);
            Ifin.SetActive(false);
        }
        else if (MGGO.activeSelf)
        {
            //pistolUI.SetActive(false);
            //shotgunUI.SetActive(false);
            //sniperUI.SetActive(false);
            //MGUI.SetActive(true);

            TptI.SetActive(false);
            TsgI.SetActive(false);
            TspI.SetActive(false);
            TmgI.SetActive(true);
            Ifin.SetActive(false);
        }
        if (Input.GetKey(KeyCode.R) && WeaponInfo.ammo < 100 && WeaponInfo.MaxAmmo > 0)
        {
            m_AudioSource.loop = true;
            m_AudioSource.Play();
            Reload();
        }
        if (WeaponInfo.ammo <= 0)
        {
            WeaponInfo.ammo = 0;
        }
        if (WeaponInfo.reloadAffirm) // Reload Function
        {
            if (wT <= 0)
            {
                WeaponInfo.ammo++;
                WeaponInfo.MaxAmmo--;
                wT = waitTime;
            }
            wT -= 1 * Time.deltaTime;
            RI.SetActive(true);
            if (WeaponInfo.ammo >= 100)
            {
                WeaponInfo.ammo = 100;
                RI.SetActive(false);
                WeaponInfo.reloadAffirm = false;
                m_AudioSource.Stop();
            }
            else if (WeaponInfo.MaxAmmo <= 0)
            {
                RI.SetActive(false);
                WeaponInfo.reloadAffirm = false;
                m_AudioSource.Stop();
            }
        }
    }

    public void Reload()
    {
        WeaponInfo.reloadAffirm = true;
    }
}
