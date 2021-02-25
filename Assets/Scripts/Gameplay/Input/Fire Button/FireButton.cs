using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : MonoBehaviour
{
#if UNITY_ANDROID
    [Header("References")]
    [SerializeField] private GameObject fireButton;

    private PlayerShoot m_playerShoot;

    private float wT;

    private Vector2 startingPoint;
    private int leftTouch = 99;

    // Start is called before the first frame update
    void Start()
    {
        m_playerShoot = GameObject.Find("Pistol").GetComponent<PlayerShoot>();
        wT = m_playerShoot.waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Firing") && wT <= 0)
        //{
        //    m_playerShoot.Shoot();
        //    //PlayerInfo.ammo = PlayerInfo.ammo - 2;
        //    wT = m_playerShoot.waitTime;
        //}

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {

            if (wT <= 0)
            {
                m_playerShoot.Shoot();
                wT = m_playerShoot.waitTime;
            }
        }
        else if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {

        }

        wT -= 1 * Time.deltaTime;
    }
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
