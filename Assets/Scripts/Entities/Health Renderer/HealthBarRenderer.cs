using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Singleton manager responsible for rendering
 *  HP bar of entities
 *  
 *  Attach to an empty GameObject
 * 
 */
public class HealthBarRenderer : Singleton<HealthBarRenderer>
{
    private IEnumerable<Entity> m_entityList;

    private void Awake()
    {
        // Get list of all entities
        m_entityList = FindObjectsOfType<MonoBehaviour>().OfType<Entity>();
    }

    private void LateUpdate()
    {
        // Render HP bar
    }
}
