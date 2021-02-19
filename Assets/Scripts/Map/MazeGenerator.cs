using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator
{
    public static void NaiveGenerate(GridManager gridManager)
    {
        Grid grid = gridManager.m_grid;
        grid.Reset();

        int numGridX = grid.NumGridX;
        int numGridZ = grid.NumGridZ;

        for (int x = 0; x < numGridX; ++x)
        {
            for (int z = 0; z < numGridZ; ++z)
            {
                if (grid.GetContent(x, z) == TILE_CONTENT.EMPTY)
                {
                    float rand = Random.Range(0f, 100f);

                    if (rand <= 88f)
                        grid.SetContent(x, z, TILE_CONTENT.EMPTY);
                    else
                        grid.SetContent(x, z, TILE_CONTENT.WALL);
                }
            }
        }

        Vector2Int middleCell = new Vector2Int(numGridX / 2, numGridZ / 2);
        Vector2Int[] cellsToClear = GetNeighbours( middleCell );

        foreach (Vector2Int cell in cellsToClear)
            grid.SetContent(cell.x, cell.y, TILE_CONTENT.EMPTY);

        gridManager.ReloadGrid();
    }

    public static void MrTangsAlgorithm(int key, Vector2Int start, float wallLoad, GridManager gridManager)
    {
        Grid grid = gridManager.m_grid;
        grid.Reset();

        int numGridX = grid.NumGridX;
        int numGridZ = grid.NumGridZ;

        // Maze Generation here....

        Vector2Int middleCell = new Vector2Int(numGridX / 2, numGridZ / 2);
        Vector2Int[] cellsToClear = GetNeighbours(middleCell);

        foreach (Vector2Int cell in cellsToClear)
            grid.SetContent(cell.x, cell.y, TILE_CONTENT.EMPTY);

        gridManager.ReloadGrid();
    }

    public static void DungeonGeneration(int seed, GridManager gridManager)
    {
        Grid grid = gridManager.m_grid;
        grid.Reset();

        int numGridX = grid.NumGridX;
        int numGridZ = grid.NumGridZ;

        // Maze Generation here....

        Vector2Int middleCell = new Vector2Int(numGridX / 2, numGridZ / 2);
        Vector2Int[] cellsToClear = GetNeighbours(middleCell);

        foreach (Vector2Int cell in cellsToClear)
            grid.SetContent(cell.x, cell.y, TILE_CONTENT.EMPTY);

        gridManager.ReloadGrid();
    }

    // Helper
    private static Vector2Int[] GetNeighbours(Vector2Int middleCell)
    {
        Vector2Int[] neighbours = new Vector2Int[9];
        neighbours[0] = middleCell;

        for (int i = 1; i < 9; ++i)
        {
            int nextX = middleCell.x;
            int nextZ = middleCell.y;

            switch ( i )
            {
                case 1:          ++nextZ; break;
                case 2: ++nextX; ++nextZ; break;
                case 3: ++nextX;          break;
                case 4: ++nextX; --nextZ; break;
                case 5:        ; --nextZ; break;
                case 6: --nextX; --nextZ; break;
                case 7: --nextX;          break;
                case 8: --nextX; --nextZ; break;
            }

            neighbours[i] = new Vector2Int(nextX, nextZ);
        }

        return neighbours;
    }
}
