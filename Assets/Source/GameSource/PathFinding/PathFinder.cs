using System.Collections.Generic;
using UnityEngine;

public static class PathFinder
{
    public static List<Vector2Int> FindPath(Vector2Int startPos, Vector2Int endPos, CellModel[,] grid)
    {
        CellModel startNode = grid[startPos.x, startPos.y];
        CellModel endNode = grid[endPos.x, endPos.y];
        CellModel lowestHNode = startNode;
        int xLen = grid.GetLength(0);
        int yLen = grid.GetLength(1);
        
        BinaryHeap _openList = new (128);
        List<CellModel> _closedList =  new ();
        _openList.Add(startNode);

        for (int x = 0; x < xLen; x++)
        {
            for (int y = 0; y < yLen; y++)
            {
                CellModel node = grid[x,y];
                node.PathOptions.ResetValues();
            }
        }

        startNode.PathOptions.g = 0;
        startNode.PathOptions.h = CalculateDistanceCost(startNode, endNode);
        startNode.PathOptions.CalculateF();

        while (_openList.Count > 0)
        {
            CellModel currentNode = _openList.ExtractMin();
            if (currentNode == endNode)
            {
               return CalculatePath(endNode);
            }

            _closedList.Add(currentNode);
            
            foreach (CellModel adj in currentNode.AdjacentCells)
            {
                if (_closedList.Contains(adj)) continue;
                if(adj == null) continue;
                if (!adj.isEmpty || adj.CellType != CellTypes.Walkable)
                {
                    _closedList.Add(adj);
                    continue;
                }
                
                int tempG = currentNode.PathOptions.g + CalculateDistanceCost(currentNode, adj);
                if (tempG < adj.PathOptions.g)
                {
                    adj.PathOptions.cameFromNode = currentNode;
                    adj.PathOptions.g = tempG;
                    adj.PathOptions.h = CalculateDistanceCost(adj, endNode);
                    adj.PathOptions.CalculateF();
                    if (adj.PathOptions.h < lowestHNode.PathOptions.h)
                    {
                        lowestHNode = adj;
                    }
                    _openList.Add(adj);
                }
            }
        }
        
        return FindPath(startPos, lowestHNode.Position, grid);
    }
     
    private static List<Vector2Int> CalculatePath(CellModel endNode)
    {
        List<Vector2Int> wayPoints = new() { endNode.Position };
        CellModel currentNode = endNode;

        while (currentNode.PathOptions.cameFromNode != null)
        {
            wayPoints.Add(currentNode.PathOptions.cameFromNode.Position); 
            currentNode = currentNode.PathOptions.cameFromNode;
        }

        wayPoints.Reverse();
        wayPoints.RemoveAt(0);
        return wayPoints;
    }

    private static int CalculateDistanceCost(CellModel a, CellModel b)
    {
        int xDif = Mathf.Abs(a.Position.x - b.Position.x);
        int yDif = Mathf.Abs(a.Position.y - b.Position.y);
        int remaining = Mathf.Abs(xDif - yDif);
        return Mathf.Min(xDif, yDif) * 14 + remaining * 10;
    }
}