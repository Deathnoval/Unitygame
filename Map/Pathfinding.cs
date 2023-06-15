using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public int xPos;
    public int yPos;
    public int gValue;
    public int hValue;
    public PathNode parentNode;
    public int fValue
    {
        get
        {
            return gValue + hValue;
        }
    }

    public void Clear()
    {
        gValue = 0;
        hValue = 0;
        parentNode = null;
    }
    public PathNode(int xPos, int yPos)
    {

        this.xPos = xPos;
        this.yPos = yPos;
    }
}

[RequireComponent(typeof(GridMap))]
public class Pathfinding : MonoBehaviour
{
    GridMap grid;
    PathNode[,] pathNodes;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (grid == null) { grid = GetComponent<GridMap>(); }

        pathNodes = new PathNode[grid.length, grid.height];

        for (int x = 0; x < grid.length; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                pathNodes[x, y] = new PathNode(x, y);
            }
        }
    }
    public void CalculateWalkableTerrain(int startX, int startY, int range, ref List<PathNode> toHighlight)
    {
        range = range * 10 + range;
        PathNode startNote = pathNodes[startX, startY];
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();
        openList.Add(startNote);

        while (openList.Count > 0)
        {
            PathNode currentNode = openList[0];

            openList.Remove(currentNode);

            closedList.Add(currentNode);
            List<PathNode> neighbors = new List<PathNode>();
            if (currentNode.yPos % 2 == 0)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        if (x == 0 && y == 0)
                        {
                            continue;
                        }
                        if (x == 1 && y == -1)
                        {
                            continue;
                        }
                        if (x == 1 && y == 1)
                        {
                            continue;
                        }
                        if (grid.CheckPosition(currentNode.xPos + x, currentNode.yPos + y) == false)
                        {
                            continue;
                        }
                        neighbors.Add(pathNodes[currentNode.xPos + x, currentNode.yPos + y]);
                    }
                }
            }
            else
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        if (x == 0 && y == 0)
                        {
                            continue;
                        }
                        if (x == -1 && y == -1)
                        {
                            continue;
                        }
                        if (x == -1 && y == 1)
                        {
                            continue;
                        }
                        if (grid.CheckPosition(currentNode.xPos + x, currentNode.yPos + y) == false)
                        {
                            continue;
                        }
                        neighbors.Add(pathNodes[currentNode.xPos + x, currentNode.yPos + y]);
                    }
                }
            }
            for (int i = 0; i < neighbors.Count; i++)
            {
                if (closedList.Contains(neighbors[i]))
                {
                    continue;
                }
                if (grid.CheckWalkable(neighbors[i].xPos, neighbors[i].yPos) == false)
                {
                    continue;
                }
                int moveCost = currentNode.gValue + CalculateDistance(currentNode, neighbors[i]);
                if (moveCost > range)
                {
                    continue;
                }
               
                if (openList.Contains(neighbors[i]) == false || moveCost < neighbors[i].gValue)
                {
                    neighbors[i].gValue = moveCost;
                    neighbors[i].parentNode = currentNode;
                    if (openList.Contains(neighbors[i]) == false)
                    {
                        openList.Add(neighbors[i]);
                    }
                }
            }
        }
        if (toHighlight != null)
        {
            toHighlight.AddRange(closedList);
        }

    }
    internal void Clear()
    {
        for (int x = 0; x < grid.length; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                pathNodes[x, y].Clear();
            }
        }
    }
    public List<PathNode> TrackBackPath(Player selectedPlayer, int x, int y)
    {
        List<PathNode> path = new List<PathNode>();

        if (!grid.CheckPosition(x, y) || pathNodes[x, y] == null || pathNodes[x, y].parentNode == null)
        {
            return null;
        }
        PathNode currentNode = pathNodes[x, y];
        if (grid.CheckAttackRange(selectedPlayer, currentNode, currentNode.parentNode) == selectedPlayer.range && grid.CheckPositionPlayer(x, y) || grid.CheckPositionMonster(x, y))
       {
            selectedPlayer.shouldAttack = true;
            return null;
       }
        else if ((grid.CheckPositionPlayer(x, y)) && grid.CheckAttackRange(selectedPlayer, currentNode, currentNode.parentNode) != selectedPlayer.range)
        {
            return null;
        }
        while (currentNode.parentNode != null)
        {
       
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        return path;
    }
    public int CalculateDistance(PathNode current, PathNode target)
    {
        int distX = Mathf.Abs(target.xPos - current.xPos);
        int distY = Mathf.Abs(target.yPos - current.yPos);
        if (distX > distY)
        {
            return 11 * distY + 10 * (distX - distY);

        }
        return 11 * distX + 10 * (distY - distX);
    }

    public int CalculateDistance2(PathNode current, PathNode target)
    {
        int distX = Mathf.Abs(target.xPos - current.xPos);
        int distY = Mathf.Abs(target.yPos - current.yPos);
        int distance = Mathf.RoundToInt(Mathf.Sqrt(distX * distX + distY * distY));
        return distance;
    }
}