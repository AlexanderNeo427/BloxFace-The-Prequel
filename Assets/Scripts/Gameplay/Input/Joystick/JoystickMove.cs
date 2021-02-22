using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{
#if UNITY_ANDROID 
    [Header ("References")]
    [SerializeField] private GameObject joystickBG;
    [SerializeField] private GameObject joystickButton;

    private Vector2    m_bgPos;
    private Vector2    m_buttonPos;
    private PlayerMove m_playerMove;

    private float m_screenWidth;
    private float m_screenHeight;

    // Don't let the joystick button move too
    // far from the background image
    private float m_maxDist; 

    private void Start()
    {
        m_screenWidth  = Screen.width;
        m_screenHeight = Screen.height;

        m_playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();

        m_bgPos = new Vector2(joystickBG.transform.position.x,
                              joystickBG.transform.position.y);

        float bgScale = joystickBG.GetComponent<Transform>().localScale.x;
        float bgWidth = joystickBG.GetComponent<RectTransform>().rect.width;

        m_maxDist = bgScale * bgWidth;
    }

    private void Update()
    {
        float screen_dx = m_playerMove.joystickDelta.x * m_screenWidth;
        float screen_dy = m_playerMove.joystickDelta.y * m_screenHeight;

        Vector2 dir = new Vector2(screen_dx, screen_dy);
        dir = Vector2.ClampMagnitude( dir, m_maxDist * 0.5f );

        m_buttonPos = m_bgPos + dir;
        joystickButton.transform.position = new Vector2(m_buttonPos.x, m_buttonPos.y);
    }
#endif

/*
 *   If this isn't on Android, then 
 *   just remove the joystick on play
 */
#if UNITY_STANDALONE_WIN
    private void Awake()
    {           
        Destroy( gameObject );
        Destroy( this );
    }
#endif

}
