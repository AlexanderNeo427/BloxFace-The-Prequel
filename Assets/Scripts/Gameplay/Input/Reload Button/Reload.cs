using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Reload : MonoBehaviour
{
#if UNITY_ANDROID

    private GraphicRaycaster m_Raycaster;
    private PointerEventData m_PointerEventData;
    private EventSystem m_EventSystem;

    AudioSource m_AudioSource;

    private WeaponGUIScript weaponGUIScript;

    // Start is called before the first frame update
    void Start()
    {
        weaponGUIScript = GameObject.Find("UICanvas").GetComponent<WeaponGUIScript>();

        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();

        WeaponGUIScript.instance.TptI.SetActive(true);
        WeaponGUIScript.instance.TspI.SetActive(false);
        WeaponGUIScript.instance.TsgI.SetActive(false);
        WeaponGUIScript.instance.TmgI.SetActive(false);
        WeaponGUIScript.instance.RI.SetActive(false);

        WeaponGUIScript.instance.time = 3f;
        WeaponGUIScript.instance.t = false;

        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponGUIScript.instance.pistolGO.activeSelf)
        {
            WeaponGUIScript.instance.TptI.SetActive(true);
            WeaponGUIScript.instance.TspI.SetActive(false);
            WeaponGUIScript.instance.TsgI.SetActive(false);
            WeaponGUIScript.instance.TmgI.SetActive(false);
        }
        else if (WeaponGUIScript.instance.shotgunGO.activeSelf)
        {
            WeaponGUIScript.instance.TptI.SetActive(false);
            WeaponGUIScript.instance.TspI.SetActive(true);
            WeaponGUIScript.instance.TsgI.SetActive(false);
            WeaponGUIScript.instance.TmgI.SetActive(false);
        }
        else if (WeaponGUIScript.instance.sniperGO.activeSelf)
        {
            WeaponGUIScript.instance.TptI.SetActive(false);
            WeaponGUIScript.instance.TspI.SetActive(false);
            WeaponGUIScript.instance.TsgI.SetActive(true);
            WeaponGUIScript.instance.TmgI.SetActive(false);
        }
        else if (WeaponGUIScript.instance.MGGO.activeSelf)
        {
            WeaponGUIScript.instance.TptI.SetActive(false);
            WeaponGUIScript.instance.TsgI.SetActive(false);
            WeaponGUIScript.instance.TspI.SetActive(false);
            WeaponGUIScript.instance.TmgI.SetActive(true);
        }
        if (WeaponInfo.ammo <= 0)
        {
            WeaponInfo.ammo = 0;
        }
        if (WeaponInfo.reloadAffirm)
        {
            if (WeaponGUIScript.instance.wT <= 0)
            {
                WeaponInfo.ammo++;
                WeaponInfo.MaxAmmo--;
                WeaponGUIScript.instance.wT = WeaponGUIScript.instance.waitTime;
            }
            WeaponGUIScript.instance.wT -= 1 * Time.deltaTime;
            WeaponGUIScript.instance.RI.SetActive(true);
            if (WeaponInfo.ammo >= 100)
            {
                WeaponInfo.ammo = 100;
                WeaponGUIScript.instance.RI.SetActive(false);
                WeaponInfo.reloadAffirm = false;
                m_AudioSource.Stop();
            }
            else if (WeaponInfo.MaxAmmo <= 0)
            {
                WeaponGUIScript.instance.RI.SetActive(false);
                WeaponInfo.reloadAffirm = false;
                m_AudioSource.Stop();
            }
        }

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                //Set up the new Pointer Event
                m_PointerEventData = new PointerEventData(m_EventSystem);
                //Set the Pointer Event Position to that of the mouse position
                m_PointerEventData.position = Input.GetTouch(i).position;

                //Create a list of Raycast Results
                List<RaycastResult> results = new List<RaycastResult>();

                //Raycast using the Graphics Raycaster and mouse click position
                m_Raycaster.Raycast(m_PointerEventData, results);

                foreach (RaycastResult result in results)
                {
                    if (result.gameObject.tag == "Reload Button")
                    {
                        if (WeaponInfo.ammo < 100 && WeaponInfo.MaxAmmo > 0)
                        {
                            m_AudioSource.loop = true;
                            m_AudioSource.Play();
                            WeaponGUIScript.Reload();
                        }
                    }
                }
            }
        }
    }
#endif

    /*
    *   If this isn't on Android, then 
    *   just remove the reload on play
    */
#if UNITY_STANDALONE_WIN
    private void Awake()
    {
        Destroy(gameObject);
        Destroy(this);
    }
#endif

}