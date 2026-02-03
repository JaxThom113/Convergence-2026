using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;

/*

ALGORITHM DESCRIPTION

This maze generation algorithm uses a combination of Flood Fill (DFS) maze generation, A* pathfinding,
and reucursion to dynamically create new dungeons as the player progresses through the game. 

Procedure:
1) DFS is used to create maze (0 = floor, 1 = wall)
2) Random start/end points are picked at the bottom/top of the grid to create a path through maze
3) A* pathfinding is used to find the optimal path
4) Maze floor tiles separated (2 = correct path, 3 = extra path)
5) Large amount of enemies are placed along correct path
6) Loot is spawned on areas furthest from main path
7) Additional enemies spawned to protect loot
8) Player is placed in start cell, and game begins

*/

public class ProcGen2 : MonoBehaviour
{
    [Header("Generation Settings")]
    [Range(0, 1000)]
    public int seed = 0;
    [TextArea(15, 20)]
    public string gridDebug;

    [Header("Tilemap References")]
    public Tilemap wallTilemap;
    public Tilemap edgeTilemap;
    public Tilemap floorTilemap;
    public TileBase wallTile;
    public TileBase edgeTile;
    public TileBase floorTile;
    public TileBase highlightFloorTile;

    [Header("Entity References")]
    public GameObject player;
    public GameObject enemy;
    public GameObject chest;

    // 0 = floor, 1 = wall, 2 = correct path
    private List<List<int>> grid = new List<List<int>>();
    private List<int> bottomEdge;
    private List<int> topEdge;

    // maze parameters
    private const int gridSize = 15;
    private Vector2Int start = new Vector2Int(0, 0);
    private Vector2Int end = new Vector2Int(0, gridSize - 1);

    void Start()
    {
        Random.InitState(seed);
        GenerateLevel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateLevel();
        }
    }

    void UpdateGridDebug()
    {
        // display the grid in a text box in the inspector panel
        gridDebug = "";
        for (int y = gridSize - 1; y >= 0; y--)
        {
            for (int x = 0; x < gridSize; x++)
            {
                if (grid[x][y] == 2)
                    gridDebug += "▣ ";

                else if (grid[x][y] == 1)
                    gridDebug += "□ ";
                else
                    gridDebug += "■ ";
            }
            gridDebug += "\n";
        }
    }

    /*
        Main generation function
    */

    void GenerateLevel()
    {
        // Step #1: Initialize grid full of walls (1's)
        CreateMaze();

        // Step #2: Flood Fill / DFS maze generation
        GenerateMaze();

        // Step #3: Select random start / finish, edit edge tiles
        StartFinish();

        // Step #4: Use A* pathfinding to find that most direct route start -> finish
        GeneratePath();

        // Step #5: Draw the grid matrix to the tilemap
        DrawGrid();

        // Step #6: Place enemies along correct path
        SpawnEnemiesAlongPath();

        // Step #7: Place loot at dead ends
        SpawnLootDeadEnds();

        // Step #8: Place enemies in front of loot / in branching paths
        SpawnEnemiesBranchingPaths();

    }

    /*
        Helper functions for GenerateLevel()
    */

    void CreateMaze()
    {
        // fill grid with walls
        grid = new List<List<int>>();
        for (int y = 0; y < gridSize; y++)
        {
            grid.Add(new List<int>());
            for (int x = 0; x < gridSize; x++)
            {
                grid[y].Add(1);
            }
        }
    }

    void GenerateMaze()
    {
        grid = Dfs.DfsMazeGenerate(grid, gridSize, 0, 0);

        Debug.Log("New maze - Start: " + start + " End: " + end);
    }

    void StartFinish()
    {
        // find open tiles for entrance/exit (after maze is generated)
        List<int> bottomOpenTiles = new List<int>();
        List<int> topOpenTiles = new List<int>();

        for (int x = 0; x < gridSize; x++)
        {
            if (grid[x][gridSize - 1] == 0)
                topOpenTiles.Add(x);
            if (grid[x][0] == 0)
                bottomOpenTiles.Add(x);
        }

        // select random start
        if (bottomOpenTiles.Count > 0)
            start.x = bottomOpenTiles[Random.Range(0, bottomOpenTiles.Count)];
        else
            start.x = 0;

        //Debug.Log("[" + string.Join(", ", bottomOpenTiles) + "]");

        // select random end
        if (topOpenTiles.Count > 0)
            end.x = topOpenTiles[Random.Range(0, topOpenTiles.Count)];
        else
            end.x = 0;

        //Debug.Log("[" + string.Join(", ", topOpenTiles) + "]");


        // set up edges
        bottomEdge = new List<int>();
        topEdge = new List<int>();

        for (int x = 0; x < gridSize; x++)
        {
            bottomEdge.Add(1);
            topEdge.Add(1);
        }

        bottomEdge[start.x] = 0;
        topEdge[end.x] = 0;
    }

    void GeneratePath()
    {
        List<Vector2Int> path = AStar.AStarFindPath(grid, gridSize, start, end);

        if (path != null && path.Count > 0)
        {
            Debug.Log("Path found with " + path.Count + " tiles");

            // Highlight the path on the floor tilemap
            foreach (Vector2Int pos in path)
            {
                grid[pos.x][pos.y] = 2;
            }
        }
        else
        {
            Debug.LogWarning("No path found between start and end!");
        }
    }

    void DrawGrid()
    {
        Vector3Int playerPos = new Vector3Int(0, 0, 0);

        // take the values from the grid array and draw them to the tilegrid
        for (int x = 0; x < gridSize; x++)
        {
            // top edge tiles
            Vector3Int topEdgePos = new Vector3Int(x + 1, gridSize + 1, 0);
            if (topEdge[x] == 0)
            {
                // this for loop carves a line after the exit tile
                for (int y = gridSize + 1; y <= 25; y++)
                {
                    topEdgePos.y = y;
                    edgeTilemap.SetTile(topEdgePos, null);
                }
            }
            else
            {
                for (int y = gridSize + 1; y <= 25; y++)
                {
                    topEdgePos.y = y;
                    edgeTilemap.SetTile(topEdgePos, edgeTile);
                }
            }

            // bottom edge tiles
            Vector3Int bottomEdgePos = new Vector3Int(x + 1, 0, 0);
            if (bottomEdge[x] == 0)
            {
                // this for loop carves a line leading up to the entrance tile
                for (int y = 0; y >= -9; y--)
                {
                    bottomEdgePos.y = y;
                    edgeTilemap.SetTile(bottomEdgePos, null);
                }

                // put the player in the entrance tile
                playerPos = new Vector3Int(x + 1, 0, 0);
            }
            else
            {
                for (int y = 0; y >= -9; y--)
                {
                    bottomEdgePos.y = y;
                    edgeTilemap.SetTile(bottomEdgePos, edgeTile);
                }
            }

            // place the player at the entrance
            player.transform.position = edgeTilemap.GetCellCenterWorld(playerPos);

            // place walls
            for (int y = 0; y < gridSize; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);

                if (grid[x][y] == 0 || grid[x][y] == 2)
                {
                    wallTilemap.SetTile(pos, null);
                }
                else
                {
                    wallTilemap.SetTile(pos, wallTile);
                }

                if (grid[x][y] == 2)
                {
                    floorTilemap.SetTile(pos, highlightFloorTile);
                }
                else
                {
                    floorTilemap.SetTile(pos, floorTile);
                }

            }
        }

        UpdateGridDebug();
    }

    void SpawnEnemiesAlongPath()
    {

    }

    void SpawnLootDeadEnds()
    {

    }

    void SpawnEnemiesBranchingPaths()
    {

    }
}