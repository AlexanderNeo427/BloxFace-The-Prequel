using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Attach this script to a plane
 */
public class DisplayHealth : MonoBehaviour
{
    private float      m_maxHP;
    private float      m_currentHP;
    private Vector3    m_originalScale;
    private PlayerInfo m_playerInfo;
    private Camera     m_camera;

    private void Start()
    {
        // TODO : Find all gameObjects in scene
        //        insert into list<>
        //        render HP

        m_camera = Camera.main;

        m_playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
        m_maxHP = m_playerInfo.HP;
        
        m_originalScale = new Vector3();
        m_originalScale.x = transform.localScale.x;
        m_originalScale.y = transform.localScale.y;
        m_originalScale.z = transform.localScale.z;
    }

    private void Update()
    {
        m_currentHP = m_playerInfo.HP;
        float ratio = m_currentHP / m_maxHP;

        Vector3 newScale = new Vector3(m_originalScale.x * ratio, m_originalScale.y, m_originalScale.z);
        Vector3 newPos = m_playerInfo.pos + new Vector3(0, 2, 0);

        transform.localScale = newScale;
        transform.position   = newPos;

        Vector3 dir = transform.position - m_camera.transform.position;
        Quaternion rotation = Quaternion.LookRotation( dir, Vector3.up );
        transform.rotation = rotation;
        transform.Rotate(new Vector3(1, 0), -90f);
    }
}
