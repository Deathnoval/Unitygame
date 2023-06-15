using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;




[RequireComponent(typeof(Tilemap))]
[RequireComponent(typeof(GridMap))]
public class GridManager : MonoBehaviour
{
    Tilemap tile;
    GridMap grid;
    public int masses = 0;
    public int plainmasses = 0;
    public int eventplant = 0;

    [SerializeField] private TileSet tileSet;
  
        private void Awake()
        {
            GenerateTileMap();
        }

        private void GenerateTileMap()
        {
            tile = GetComponent<Tilemap>();
            grid = GetComponent<GridMap>();
            grid.Init(16, 16);
            GenerateMountains();
            GeneratePlains();
            GenerateIslandEvent();
            UpdateTileMap();
        }

        private void GenerateMountains()
        {
            int masseTemp = masses;
            int attempts = 0;

            while (masseTemp > 0 && attempts < masses * 2)
            {
                attempts++;
                int x = Random.Range(2, 14);
                int y = Random.Range(2, 14);
                SetMountant(x, y, 1);
          
                masseTemp--;
            }
        }

        private void GeneratePlains()
        {
            int plainTemp = plainmasses;
            int plain = 0;

            while (plainTemp > 0 && plain < plainmasses * 2)
            {
                plain++;
                int x = Random.Range(1, 15);
                int y = Random.Range(1, 15);
                SetIsland(x, y, 2);
                SetIsland(15 - x, 15 - y, 2);
                plainTemp--;
            }
        }
    private void GenerateIslandEvent()
    {
        int islandCount = eventplant;
        int island = 0;

        while (islandCount > 0 && island < eventplant * 2)
        {
            island++;
            int x = Random.Range(1, 15);
            int y = Random.Range(1, 15);
            SetIslandEvent(x, y, 3);
            SetIslandEvent(15 - x, 15 - y, 3);
            islandCount--;
        }
    }


    void UpdateTileMap()
    {
        for (int x = 0; x < grid.length; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                UpdateTile(x, y);
            }
        }
    }
    public void UpdateTile(int x, int y)
    {
        int tileId = grid.Get(x, y);
        if (tileId == -1)
        {
            return;
        }
        tile.SetTile(new Vector3Int(x, y, 0), tileSet.tiles[tileId]);
    }
    internal bool CheckPosition(int x, int y)
    {
        return grid.CheckPosition(x, y);
    }

    public void SetMountant(int x, int y, int to)
    {
        if (to != -1)
        {
            grid.Set(x, y, to);
            UpdateTile(x, y);
            grid.Set(15-x,15- y, to);
            UpdateTile(15-x,15- y);
        }
        for (int i = 0; i < 2; i++) 
        {
            int random = Random.Range(1, 5);

            if (to != -1)
            {
                if (CheckPosition(x - 1, y) && random == 1)
                {
                    grid.Set(x - 1, y, to);
                    UpdateTile(x - 1, y);
                    grid.Set(15-(x - 1), 15-y, to);
                    UpdateTile(15-(x - 1), 15-y);
                }
                if (CheckPosition(x + 1, y) && random == 2)
                {
                    grid.Set(x + 1, y, to);
                    UpdateTile(x + 1,  y);
                    grid.Set(15-(x + 1), 15-y, to);
                    UpdateTile(15-(x + 1),15- y);
                }
                if (CheckPosition(x, y - 1) && random == 3)
                {
                    grid.Set(x, y - 1, to);
                    UpdateTile(x, y - 1);
                    grid.Set(15-x, 15-(y - 1), to);
                    UpdateTile(15-x,15- (y - 1));
                }
                if (CheckPosition(x, y + 1) && random == 4)
                {
                    grid.Set(x, y + 1, to);
                    UpdateTile(x, y + 1);
                    grid.Set(15-x,15- (y + 1), to);
                    UpdateTile(15-x, 15-(y + 1));
                }
            }
        }
        
    }
    public void SetIsland(int x, int y, int to)
    {
        if (to != -1)
        {
            grid.Set(x, y, to);
            UpdateTile(x, y);
        }
    }
    public void SetIslandEvent(int x, int y, int to)
    {
        if (to != -1)
        {
      
            grid.Set(x, y, to);
            UpdateTile(x, y);
        }
    }

    public List<Player> GetAllPlayers()
    {
        List<Player> players = new List<Player>();

        for (int x = grid.length-1; x >=0; x--)
        {
            for (int y = 0; y < grid.height; y++)
            {
        
                Player player = grid.GetPlayer(x, y);

                if (player != null)
                {
                    players.Add(player);
                }
            }
        }
        return players;
    }
    public List<Monster> GetAllMonsters()
    {
        List<Monster> monsters = new List<Monster>();

        for (int x = grid.length - 1; x >= 0; x--)
        {
            for (int y = 0; y < grid.height; y++)
            {

                Monster monster = grid.GetMonster(x, y);

                if (monster != null)
                {
                    monsters.Add(monster);
                }
            }
        }
        return monsters;
    }

}
