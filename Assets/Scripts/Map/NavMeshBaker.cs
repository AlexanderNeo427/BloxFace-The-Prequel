using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Script to bake the NavMesh
 * at runtime  
 * 
 * i.e. So it can be baked after
 *      maze generation
 * 
 * Attach this script to the Terrain
 * 
 */
[RequireComponent (typeof(NavMeshSurface))]
public class NavMeshBaker : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;

    private void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        navMeshSurface = terrain.GetComponent<NavMeshSurface>();
        navMeshSurface.BuildNavMesh();
    }

    public void BakeNavigationMesh()
    {
        if (navMeshSurface != null)
            navMeshSurface.BuildNavMesh();
    }
}
