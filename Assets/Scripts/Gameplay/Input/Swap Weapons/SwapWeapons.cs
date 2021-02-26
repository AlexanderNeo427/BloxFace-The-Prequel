using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwapWeapons : MonoBehaviour
{
#if UNITY_ANDROID

    private GraphicRaycaster m_Raycaster;
    private PointerEventData m_PointerEventData;
    private EventSystem m_EventSystem;

    private PlayerMove m_playerMove;

    public GameObject[] Weapons;

    // Start is called before the first frame update
    void Start()
    {
        WeaponInfo.instance.pistol.SetActive(true);
        WeaponInfo.instance.shotgun.SetActive(false);
        WeaponInfo.instance.sniper.SetActive(false);
        WeaponInfo.instance.machineGun.SetActive(false);

        m_playerMove = GetComponent<PlayerMove>();

        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
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
                    if (result.gameObject.tag == "Swap Weapons Button")
                    {
                        ActivateWeapon(0);
                    }
                }
            }
        }
    }

    public void ActivateWeapon(int index)
    {
        foreach (GameObject Weapon in Weapons)
        {
            Weapon.SetActive(false);

            if (index == 0)
            {
                m_playerMove.ResetMoveSpeed();
                Weapons[0].SetActive(true);     // Pistol
                Weapons[1].SetActive(false);    // Shotgun
                Weapons[2].SetActive(false);    // Sniper
                Weapons[3].SetActive(false);    // Machine Gun
            }
            else if (index == 1)
            {
                m_playerMove.ResetMoveSpeed();

                if (WeaponInfo.SGAccess)
                {
                    Weapons[0].SetActive(false);
                    Weapons[1].SetActive(true);
                }
                else
                {
                    Weapons[0].SetActive(true);
                    Weapons[1].SetActive(false);
                }
                Weapons[2].SetActive(false);
                Weapons[3].SetActive(false);
            }
            else if (index == 2)
            {
                m_playerMove.ResetMoveSpeed();
                Weapons[1].SetActive(false);

                if (WeaponInfo.SPAccess)
                {
                    Weapons[0].SetActive(false);
                    Weapons[2].SetActive(true);
                }
                else
                {
                    Weapons[0].SetActive(true);
                    Weapons[2].SetActive(false);
                }
                Weapons[3].SetActive(false);
            }
            else if (index == 3)
            {
                Weapons[1].SetActive(false);
                Weapons[2].SetActive(false);

                if (WeaponInfo.MGAccess)
                {
                    Weapons[0].SetActive(false);
                    Weapons[3].SetActive(true);
                    m_playerMove.SetMoveSpeed(5f);
                }
                else
                {
                    Weapons[0].SetActive(true);
                    Weapons[3].SetActive(false);
                    m_playerMove.ResetMoveSpeed();
                }
            }

            if (Weapons[1].activeSelf && WeaponInfo.ammo >= 8 && WeaponInfo.wT <= 0 && !WeaponInfo.reloadAffirm)
            {
                WeaponInfo.instance.shotgunDist -= Time.deltaTime;

                if (WeaponInfo.instance.shotgunDist > 0)
                {
                    transform.Translate(Vector3.back * 12.5f * Time.deltaTime);
                    if (WeaponInfo.instance.In)
                    {
                        transform.Translate(Vector3.forward * 12.5f * Time.deltaTime);
                    }
                }
            }
            else
            {
                WeaponInfo.instance.shotgunDist = 0.062f;
            }
        }
        Weapons[index].SetActive(true);
    }
#endif

    /*
    *   If this isn't on Android, then 
    *   just remove the swapweapon on play
    */
#if UNITY_STANDALONE_WIN
    private void Awake()
    {
        Destroy(gameObject);
        Destroy(this);
    }
#endif

}