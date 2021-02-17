using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerMove : MonoBehaviour, Entity
{
    [SerializeField] [Range(1f, 10f)]
    private float moveSpeed = 5f;

    private float m_screenWidth;
    private float m_screenHeight;

    private Vector3 m_up;
    private Vector3 m_down;
    private Vector3 m_left;
    private Vector3 m_right;
    private Vector3 m_upLeft;
    private Vector3 m_upRight;
    private Vector3 m_downLeft;
    private Vector3 m_downRight;

    private Vector3[] m_dirList = new Vector3[8];

    private Vector3 m_moveForce;
    private CharacterController m_controller;

    private PlayerInfo m_playerInfo;

#if UNITY_ANDROID
    [Header("References")]
    [SerializeField] private GameObject joystickButton;
    [SerializeField] private GameObject joystickBG;

    // private float m_maxJoystickDeviation;

    private Vector2 m_joystickOrigin;
#endif

    private void Awake()
    {
        m_screenWidth  = Screen.width;
        m_screenHeight = Screen.height;

        m_up        = m_dirList[0] = new Vector3( 0f, 0f, 1f).normalized;
        m_down      = m_dirList[1] = new Vector3( 0f, 0f,-1f).normalized;
        m_left      = m_dirList[2] = new Vector3(-1f, 0f, 0f).normalized;
        m_right     = m_dirList[3] = new Vector3( 1f, 0f, 0f).normalized;
        m_upLeft    = m_dirList[4] = new Vector3(-1f, 0f, 1f).normalized;
        m_upRight   = m_dirList[5] = new Vector3( 1f, 0f, 1f).normalized;
        m_downLeft  = m_dirList[6] = new Vector3(-1f, 0f,-1f).normalized;
        m_downRight = m_dirList[7] = new Vector3( 1f, 0f,-1f).normalized;

#if UNITY_ANDROID

        // m_maxJoystickDeviation = joystickBG.localScale.x * 0.5f;
        m_joystickOrigin = Vector2.zero;
#endif
        m_moveForce = Vector3.zero;
        m_controller = GetComponent<CharacterController>();

        m_playerInfo = GetComponent<PlayerInfo>();
    }

    private void Update()
    {
#if UNITY_STANDALONE_WIN
        float moveZ = Input.GetAxisRaw("Vertical");
        float moveX = Input.GetAxisRaw("Horizontal");

        bool hasInput = (Mathf.Abs(moveZ) > 0.015f || Mathf.Abs(moveX) > 0.015f);

        if ( hasInput )
        {
            m_moveForce = Vector3.zero;

            if (moveZ > 0f)
                m_moveForce += m_up;
            if (moveZ < 0f)
                m_moveForce += m_down;
            
            if (moveX > 0f)
                m_moveForce += m_right;
            if (moveX < 0f)
                m_moveForce += m_left;

            float minVal = 9999f;
            Vector2 dir = new Vector2(moveX, moveZ);
            for (int i = 0; i < 8; ++i)
            {
                Vector3 tempDir = new Vector3(-dir.x, 0, -dir.y);

                float dot = Vector3.Dot(tempDir, m_dirList[i]);
                if (dot < minVal)
                {
                    minVal = dot;
                    m_moveForce = m_dirList[i];
                }
            }

            transform.forward = m_moveForce.normalized;
            m_controller.Move(m_moveForce.normalized * moveSpeed * Time.deltaTime);
        }
#elif UNITY_ANDROID    
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch( 0 );
            Vector2 touchPos = NormalizedCoords( touch.position );

            // Movement input
            if (touchPos.x < 0.5f)
            {
                switch ( touch.phase )
                {
                    case TouchPhase.Began:
                        m_joystickOrigin = NormalizedCoords( touch.position );
                        break;
                    case TouchPhase.Moved:
                        Vector2 pos = NormalizedCoords( touch.position );
                        Vector3 dir = pos - m_joystickOrigin;
                        m_moveForce = Vector2.zero;

                        float minVal = 9999f;
                        for (int i = 0; i < 8; ++i)
                        {
                            Vector3 tempDir = new Vector3(-dir.x, 0, -dir.y);

                            float dot = Vector3.Dot( tempDir, m_dirList[i] );
                            if (dot < minVal)
                            {
                                minVal = dot;
                                m_moveForce = m_dirList[i];
                            }
                        }

                        transform.forward = m_moveForce.normalized;
                        m_controller.Move(m_moveForce.normalized * moveSpeed * Time.deltaTime);
                        break;
                }
            }
            // Shooting/switching weapon input
            else
            {
                if (touchPos.y <= 0.5f)
                {
                    // Shoot
                }
                else if (touchPos.y >= 0.5f)
                {
                    // Switch weapon   
                }
            }
        }
#endif

        Debug.DrawRay(transform.position, transform.forward * 3f, Color.green);
    }

    /*
     *  Helper function to normalize 
     *  the x and y pixel coords
     *  
     *  i.e. X and Y input will be normalized from 0 - 1,
     *       regardless of screen size/aspect ratio
     */
    private Vector2 NormalizedCoords(Vector2 pos)
    {
        float normalizedX = pos.x / m_screenWidth;
        float normalizedY = pos.y / m_screenHeight;

        return new Vector2(normalizedX, normalizedY);
    }

    public float GetMaxHP()
    {
        return m_playerInfo.MaxHP;
    }

    public float GetCurrentHP()
    {
        return m_playerInfo.HP;
    }
}
