using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Health bar renderer script, to be attached to the HP bar prefab
 */
public class HealthBarRenderer : Singleton<HealthBarRenderer>
{
    [Header("Customisations (set before play)")]

    [SerializeField] [Range(1.3f, 5f)]
    private float yOffset;

    [SerializeField] [Range(1.5f, 4f)]
    private float length;

    [SerializeField] [Range(0.1f, 0.85f)]
    private float width;

    [Header ("References")]
    [SerializeField] private Material matPlayerTeamateHP;
    [SerializeField] private Material matEnemyHP;

    private GameObject m_rootGO;
    private Entity     m_owner;
    private Camera     m_camera;

    private float      m_originalLength;
    private float      m_maxHP;

    private void Start()
    {
        m_rootGO = transform.root.gameObject;
        m_owner = m_rootGO.GetComponent<Entity>();
        if (!(m_owner is Entity))
        {
            Debug.LogWarning( "HP bar must be attached to an object that implements the Entity interface" );
            Destroy( this );
        }

        m_camera = Camera.main;

        m_originalLength = length;
        m_maxHP = m_owner.GetMaxHP();

        transform.localScale = new Vector3(length, width, 0.01f);

        Zombie zombie = m_rootGO.GetComponent<Zombie>();
        if (zombie != null)
        {
            GetComponent<Renderer>().material = matEnemyHP;
        }
        else
        {
            GetComponent<Renderer>().material = matPlayerTeamateHP;
        }
    }

    private void LateUpdate()
    {
        transform.position = m_rootGO.transform.position + new Vector3(0f, yOffset, 0f);
        transform.LookAt( m_camera.transform, Vector3.up );

        float health = m_owner.GetCurrentHP();
        health = Mathf.Max( 0.085f, health );

        float ratio = health / m_maxHP;
        float newLength = ratio * length;
        transform.localScale = new Vector3(newLength, width, 0.01f);
    }
}
