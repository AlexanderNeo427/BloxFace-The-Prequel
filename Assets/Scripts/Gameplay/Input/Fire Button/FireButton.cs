using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : MonoBehaviour
{
    private PlayerShoot m_playerShoot;

    private float m_screenWidth;
    private float m_screenHeight;

    private float wT;

    // Start is called before the first frame update
    void Start()
    {
        m_screenWidth = Screen.width;
        m_screenHeight = Screen.height;

        m_playerShoot = GameObject.Find("Player").GetComponent<PlayerShoot>();
        wT = m_playerShoot.waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) /*&& PlayerInfo.ammo > 0*/ && wT <= 0 /*&& !PlayerInfo.reloadAffirm*/)
        {
            m_playerShoot.Shoot();
            //PlayerInfo.ammo = PlayerInfo.ammo - 2;
            wT = m_playerShoot.waitTime;
        }

        wT -= 1 * Time.deltaTime;
    }

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
