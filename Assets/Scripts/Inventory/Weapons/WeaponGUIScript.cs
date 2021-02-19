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
    }
}
