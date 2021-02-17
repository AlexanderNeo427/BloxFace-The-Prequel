using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Attach this script to the Terrain
 */
[RequireComponent (typeof(NavMeshSurface))]
public class NavMeshBaker : MonoBehaviour
{
    private void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        NavMeshSurface navMeshSurface = terrain.GetComponent<NavMeshSurface>();

        navMeshSurface.BuildNavMesh();

        Destroy( this );
    }
}
