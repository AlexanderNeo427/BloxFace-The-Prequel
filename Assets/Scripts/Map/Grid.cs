using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private TILE_CONTENT[,] m_gridArray;

    private int     m_noGridX;
    private int     m_noGridZ;
    private float   m_gridSize;

    public Vector3 m_botLeft { get; private set; }
    public Vector3 m_topRight { get; private set; }
    private float   m_lengthX;
    private float   m_lengthZ;

    // Getters
    public int NumGridX   { get { return m_noGridX; } }
    public int NumGridZ   { get { return m_noGridZ; } }
    public float GridSize { get { return m_gridSize; } }

    public Grid(int _numGridX, int _numGridZ, float _gridSize)
    {
        m_noGridX  = _numGridX;
        m_noGridZ  = _numGridZ;
        m_gridSize = _gridSize;

        m_lengthX = _numGridX * _gridSize;
        m_lengthZ = _numGridZ * _gridSize;

        m_botLeft = new Vector3(-m_lengthX * 0.5f, 0f, -m_lengthX * 0.5f);
        m_topRight = new Vector3(m_lengthZ * 0.5f, 0f, m_lengthZ * 0.5f);

        m_gridArray = new TILE_CONTENT[_numGridX, _numGridZ];

        for (int x = 0; x < _numGridX; ++x)
            for (int z = 0; z < _numGridZ; ++z)
            {
                if (x == 0 || x == m_noGridX - 1 ||
                    z == 0 || z == m_noGridZ - 1)
                {
                    m_gridArray[x, z] = TILE_CONTENT.WALL;
                }
                else
                {
                    m_gridArray[x, z] = TILE_CONTENT.EMPTY;
                }
            }
    }

    public void SetContent(int gridX, int gridZ, TILE_CONTENT tileContent)
    {
        if (gridX >= 0 && gridX < m_noGridX &&
            gridZ >= 0 && gridZ < m_noGridZ)
        {
            m_gridArray[gridX, gridZ] = tileContent;
            return;
        }

        Debug.LogError("Grid.cs, SetContent() out of bounds");
        Debug.LogError("X: " + gridX + ", " + "Z: " + gridZ);
    }

    public TILE_CONTENT GetContent(int gridX, int gridZ)
    {
        if (gridX >= 0 && gridX < m_noGridX &&
            gridZ >= 0 && gridZ < m_noGridZ)
        {
            return m_gridArray[gridX, gridZ];
        }

        Debug.LogError("Grid.cs, GetContent() out of bounds");
        Debug.LogError("X: " + gridX + ", " + "Z: " + gridZ);
        return TILE_CONTENT.EMPTY;
    }

    public void Reset()
    {
        for (int x = 0; x < m_noGridX; ++x)
            for (int z = 0; z < m_noGridZ; ++z)
            {
                if (x == 0 || x == m_noGridX - 1 ||
                    z == 0 || z == m_noGridZ - 1)
                {
                    m_gridArray[x, z] = TILE_CONTENT.WALL;
                }
                else
                {
                    m_gridArray[x, z] = TILE_CONTENT.EMPTY;
                }
            }
    }

    public Vector3 GridToWorld(int gridX, int gridZ)
    {
        if (gridX >= 0 && gridX < m_noGridX && 
            gridZ >= 0 && gridZ < m_noGridZ)
        {
            Vector3 worldPos = new Vector3(gridX * m_gridSize, 0f, gridZ * m_gridSize);
            worldPos.x -= Mathf.Floor(m_lengthX * 0.5f);
            worldPos.z -= Mathf.Floor(m_lengthZ * 0.5f);

            return worldPos;
        }

        Debug.LogError("Grid.cs, GridToWorld() out of bounds");
        Debug.LogError("X: " + gridX + ", " + "Z: " + gridZ);
        return new Vector3Int(-1, -1, -1);
    }

    public Vector3Int WorldToGrid(Vector3 worldPos)
    {
        if (worldPos.x >= m_botLeft.x && worldPos.x <= m_topRight.x &&
            worldPos.z >= m_botLeft.z && worldPos.z <= m_topRight.z)
        {
            worldPos.x += Mathf.Floor(m_lengthX * 0.5f);
            worldPos.z += Mathf.Floor(m_lengthZ * 0.5f);

            int gridX = (int)(worldPos.x / m_gridSize);
            int gridZ = (int)(worldPos.z / m_gridSize);
            return new Vector3Int( gridX, 0, gridZ );
        }

        Debug.LogError("Grid.cs, WorldToGrid() out of bounds");
        Debug.LogError("X: " + worldPos.x + ", " + "Z: " + worldPos.z);
        return new Vector3Int(-1, 0, -1);
    }
}
