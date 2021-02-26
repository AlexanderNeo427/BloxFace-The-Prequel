using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FireButton : MonoBehaviour
{
#if UNITY_ANDROID

    private GraphicRaycaster m_Raycaster;
    private PointerEventData m_PointerEventData;
    private EventSystem m_EventSystem;

    [SerializeField]
    private PlayerShoot m_PistolShoot;
    [SerializeField]
    private MGScript m_MachineGunShoot;
    [SerializeField]
    private ShotgunScript m_ShotgunGunShoot;
    [SerializeField]
    private SniperScript m_SniperShoot;

    // Start is called before the first frame update
    void Start()
    {
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
                    if (result.gameObject.tag == "Fire Button")
                    {
                        if (m_PistolShoot.isActiveAndEnabled)
                            m_PistolShoot.Shoot();
                        else if (m_ShotgunGunShoot.isActiveAndEnabled)
                            m_ShotgunGunShoot.Shoot();
                        else if (m_SniperShoot.isActiveAndEnabled)
                            m_SniperShoot.Shoot();
                        else if (m_MachineGunShoot.isActiveAndEnabled)
                            m_MachineGunShoot.Shoot();
                    }
                }
            }
        }

        //m_PistolShoot.wT -= 1 * Time.deltaTime;
    }
#endif

/*
*   If this isn't on Android, then 
*   just remove the firebutton on play
*/
#if UNITY_STANDALONE_WIN
    private void Awake()
    {
        Destroy(gameObject);
        Destroy(this);
    }
#endif

}