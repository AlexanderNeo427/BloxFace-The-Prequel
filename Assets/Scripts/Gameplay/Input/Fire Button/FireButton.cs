using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : MonoBehaviour
{
#if UNITY_ANDROID
    [Header("References")]
    [SerializeField] private GameObject fireButton;

    private PlayerShoot m_playerShoot;

    private Vector2 m_bgPos;
    private Vector2 m_buttonPos;
    private PlayerMove m_playerMove;

    private float m_screenWidth;
    private float m_screenHeight;

    // Don't let the joystick button move too
    // far from the background image
    private float m_maxDist;

    private float wT;

    private Vector2 startingPoint;
    private int leftTouch = 99;

    // Start is called before the first frame update
    void Start()
    {
        m_screenWidth = Screen.width;
        m_screenHeight = Screen.height;

        m_playerShoot = GameObject.Find("Player").GetComponent<PlayerShoot>();
        wT = m_playerShoot.waitTime;

        m_bgPos = new Vector2(fireButton.transform.position.x,
                              fireButton.transform.position.y);

        float bgScale = fireButton.GetComponent<Transform>().localScale.x;
        float bgWidth = fireButton.GetComponent<RectTransform>().rect.width;

        m_maxDist = bgScale * bgWidth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Firing") && wT <= 0)
        {
            m_playerShoot.Shoot();
            //PlayerInfo.ammo = PlayerInfo.ammo - 2;
            wT = m_playerShoot.waitTime;
        }

        wT -= 1 * Time.deltaTime;

        //    int i = 0;
        //    while (i < Input.touchCount)
        //    {
        //        Touch t = Input.GetTouch(i);
        //        Vector2 touchPos = getTouchPosition(t.position); // * -1 for perspective cameras
        //        if (t.phase == TouchPhase.Began)
        //        {
        //            if (t.position.x > Screen.width / 2)
        //            {
        //                m_playerShoot.Shoot();
        //            }
        //            else
        //            {
        //                leftTouch = t.fingerId;
        //                startingPoint = touchPos;
        //            }
        //        }
        //    }
    }

    //Vector2 getTouchPosition(Vector2 touchPosition)
    //{
    //    return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    //}
#endif

/*
*   If this isn't on Android, then 
*   just remove the joystick on play
*/
#if UNITY_STANDALONE_WIN
    private void Awake()
    {
        Destroy(gameObject);
        Destroy(this);
    }
#endif

}
