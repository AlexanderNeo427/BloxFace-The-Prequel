using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Used to bake Waypoints onto the grid after the maze is generated
 *  To be used by the AI patrol system
 */
public class WaypointManager : Singleton<WaypointManager>
{
    [Header("References")]
    [SerializeField] private GridManager gridManager;

    private List<Vector3> m_wayPoints;

    private void Awake()
    {
        m_wayPoints = new List<Vector3>( 300 );
    }

    public void BakeWayPoints()
    {
        Grid grid     = gridManager.m_grid;
        int numTilesX = grid.NumGridX;
        int numTilesZ = grid.NumGridZ;

        for (int x = 0; x < numTilesX; ++x)
        {
            for (int z = 0; z < numTilesZ; ++z)
            {
                if (grid.GetContent(x, z) == TILE_CONTENT.EMPTY)
                {
                    Vector3 waypoint = grid.GridToWorld( x, z );
                    m_wayPoints.Add( waypoint );
                }
            }
        }
    }

    public Vector3 GetRandomWaypoint()
    {
        return m_wayPoints[Random.Range(0, m_wayPoints.Count - 1)];
    }

/*    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        foreach (Vector3 waypoint in m_wayPoints)
            Gizmos.DrawSphere(waypoint, 0.12f);
    }*/
}
