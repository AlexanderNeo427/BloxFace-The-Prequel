using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] [Range(1f, 10f)]
    private float moveSpeed = 5f;

    private float m_screenWidth;
    private float m_screenHeight;

    private Vector3 m_up;
    private Vector3 m_down;
    private Vector3 m_left;
    private Vector3 m_right;

    private Vector3 m_moveForce;
    private CharacterController m_controller;

#if UNITY_ANDROID
    [Header("References")]
    [SerializeField] private GameObject joystickButton;
    [SerializeField] private GameObject joystickBG;

    // private float m_maxJoystickDeviation;

    private Vector3 m_upLeft;
    private Vector3 m_upRight;
    private Vector3 m_downLeft;
    private Vector3 m_downRight;

    private Vector2 m_joystickOrigin;
    private Vector3[] m_dirList = new Vector3[ 8 ];
#endif

    private void Awake()
    {
        m_screenWidth  = Screen.width;
        m_screenHeight = Screen.height;

        m_up    = new Vector3( 0f, 0f, 1f).normalized;
        m_down  = new Vector3( 0f, 0f,-1f).normalized;
        m_left  = new Vector3(-1f, 0f, 0f).normalized;
        m_right = new Vector3( 1f, 0f, 0f).normalized;
#if UNITY_ANDROID
        m_upLeft    = new Vector3(-1f, 0f, 1f).normalized;
        m_upRight   = new Vector3( 1f, 0f, 1f).normalized;
        m_downLeft  = new Vector3(-1f, 0f,-1f).normalized;
        m_downRight = new Vector3( 1f, 0f,-1f).normalized;

        // m_maxJoystickDeviation = joystickBG.localScale.x * 0.5f;

        m_dirList[0] = m_up;
        m_dirList[1] = m_upRight;
        m_dirList[2] = m_right;
        m_dirList[3] = m_downRight;
        m_dirList[4] = m_down;
        m_dirList[5] = m_downLeft;
        m_dirList[6] = m_left;
        m_dirList[7] = m_upLeft;

        m_joystickOrigin = Vector2.zero;
#endif
        m_moveForce = Vector3.zero;
        m_controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
#if UNITY_STANDALONE_WIN
        float moveZ = Input.GetAxisRaw("Vertical");
        float moveX = Input.GetAxisRaw("Horizontal");

        bool hasInput = (Mathf.Abs(moveZ) > 0.03f || Mathf.Abs(moveX) > 0.03f);

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
}
