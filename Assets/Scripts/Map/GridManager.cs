using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 *  Responsible for the grid walls generation
 *  Attach this script to an empty GameObject  
 */
public class GridManager : Singleton<GridManager>
{
    [Header ("Set grid parameters before running game")]

    [SerializeField] [Range(5, 60)]
    private int NumTilesX = 17;

    [SerializeField] [Range(5, 60)]
    private int NumTilesZ = 17;

    [SerializeField] [Range (1.5f, 4f)]
    private float TileSize = 1.85f;

    [SerializeField] [Range (0.1f, 2f)]
    private float WallHeight = 0.8f;

    [Header("References")]
    [SerializeField] private GameObject unitCubeWall;
    [SerializeField] private Material opaqueMaterial;
    [SerializeField] private Material translucentMaterial;

    public Grid   m_grid { get; private set; }
    private int   m_numTilesX;
    private int   m_numTilesZ;
    private float m_tileSize;
    private float m_wallHeight;

    private void Awake()
    {
        m_numTilesX  = NumTilesX;
        m_numTilesZ  = NumTilesZ;
        m_tileSize   = TileSize;
        m_wallHeight = WallHeight;

        m_grid = new Grid(m_numTilesX, m_numTilesZ, m_tileSize);
        MazeGenerator.NaiveGenerate( this );
    }

    public void ReloadGrid()
    {
        for (int x = 0; x < NumTilesX; ++x)
        {
            for (int z = 0; z < NumTilesZ; ++z)
            {
                if (m_grid.GetContent(x, z) == TILE_CONTENT.WALL)
                {
                    GameObject wall = Instantiate(unitCubeWall, m_grid.GridToWorld(x, z), Quaternion.identity);
                    wall.transform.localScale = new Vector3(m_tileSize, m_wallHeight, m_tileSize);
                    wall.transform.position += new Vector3(0, m_wallHeight * 0.5f, 0);

                    // If cube is outer wall cube, set to translucent
                    if (x == 0 || x == NumTilesX - 1 ||
                        z == 0 || z == NumTilesZ - 1)
                    {
                        wall.GetComponent<Renderer>().material = translucentMaterial;
                    }
                    else
                    {
                        wall.GetComponent<Renderer>().material = opaqueMaterial;
                    }
                }
            }
        }
    }

/*   
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        for (int x = 0; x < m_grid.NumGridX; ++x)
        {
            for (int z = 0; z < m_grid.NumGridZ; ++z)
            {
                Vector3 tilePos = m_grid.GridToWorld( x, z );
                Vector3 tileSize = new Vector3(m_tileSize, 0f, m_tileSize);
                // Gizmos.DrawWireCube(tilePos, tileSize); 

                switch (m_grid.GetContent(x, z))
                {
                    case TILE_CONTENT.EMPTY: Gizmos.DrawWireCube(tilePos, tileSize); break;
                    case TILE_CONTENT.WALL: Gizmos.DrawCube(tilePos, tileSize); break;
                }
            }
        }
    }*/
}
