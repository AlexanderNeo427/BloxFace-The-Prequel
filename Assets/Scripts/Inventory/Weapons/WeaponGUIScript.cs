using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGUIScript : MonoBehaviour
{
    public GameObject pistolGO;
    public GameObject shotgunGO;
    public GameObject sniperGO;
    public GameObject MGGO;

    //public GameObject pistolUI;
    //public GameObject shotgunUI;
    //public GameObject sniperUI;
    //public GameObject MGUI;

    public GameObject TptI;
    public GameObject TsgI;
    public GameObject TspI;
    public GameObject TmgI;
    public GameObject RI;

    private float time;
    private bool t;
    private float waitTime = 0.05f;
    private float wT = 0.05f;

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
        if (WeaponInfo.reloadAffirm)
        {
            if (wT <= 0)
            {
                WeaponInfo.ammo++;
                WeaponInfo.MaxAmmo--;
                //GetComponent<AudioSource>().Play();
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

    public static void Reload()
    {
        WeaponInfo.reloadAffirm = true;
    }
}
