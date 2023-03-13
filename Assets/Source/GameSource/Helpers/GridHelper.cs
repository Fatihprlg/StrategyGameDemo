using System.Collections.Generic;
using UnityEngine;

public static class GridHelper
{
    public static bool CheckOverlappingAreaIsAvailable(int width, int height, Vector2Int startPosition, CellModel[,] grid)
    {
        for (int i = startPosition.x; i < startPosition.x + width; i++)
        {
            for (int j = startPosition.y; j < startPosition.y + height; j++)
            {
                if (!grid[i, j].isEmpty || grid[i,j].CellType == CellTypes.NotWalkable) return false;
            }
        }

        return true;
    }
    
    public static void PlaceItemOnGrid(int width, int height, Vector2Int position, CellModel[,] grid)
    {
        for (int i = position.x; i < position.x + width; i++)
        {
            for (int j = position.y; j < position.y + height; j++)
            {
                grid[i, j].isEmpty = false;
            }
        }
    }
    public static void DestructItemOnGrid(int width, int height, Vector2Int position, CellModel[,] grid)
    {
        for (int i = position.x; i < position.x + width; i++)
        {
            for (int j = position.y; j < position.y + height; j++)
            {
                grid[i, j].isEmpty = true;
            }
        }
    }


    public static Vector3 WorldToGridPosition(this Vector3 pos, Vector2Int gridSize, int width = 1, int height = 1)
    {
        Vector2 gridLoc = WorldToGridCoordinates(pos, width, height, gridSize);
        return GetUnitWorldPosition((int)gridLoc.x, (int)gridLoc.y, width, height);
    }
    
    public static Vector2Int WorldToGridCoordinates(Vector3 worldPos, Vector2Int gridSize)
    {
        if (worldPos.x < 0) worldPos.x = 0;
        if (worldPos.y < 0) worldPos.y = 0;
        Vector2Int gridPos = new()
        {
            x = Mathf.FloorToInt(worldPos.x / Constants.Numerical.CELL_SCALE_AS_UNIT),
            y = Mathf.FloorToInt(worldPos.y / Constants.Numerical.CELL_SCALE_AS_UNIT)
        };
        gridPos.x = Mathf.Clamp(gridPos.x,0, gridSize.x);
        gridPos.y = Mathf.Clamp(gridPos.y,0, gridSize.y);
        return gridPos;
    }
    public static Vector2Int WorldToGridCoordinates(Vector3 worldPos, int width, int height)
    {
        if (worldPos.x < 0) worldPos.x = 0;
        if (worldPos.y < 0) worldPos.y = 0;
        
        float xOffset = (width * Constants.Numerical.CELL_SCALE_AS_UNIT / 2);
        float yOffset = (height * Constants.Numerical.CELL_SCALE_AS_UNIT / 2);
        worldPos.x -= xOffset;
        worldPos.y -= yOffset;
        Vector2Int gridPos = new()
        {
            x = Mathf.FloorToInt(worldPos.x / Constants.Numerical.CELL_SCALE_AS_UNIT),
            y = Mathf.FloorToInt(worldPos.y / Constants.Numerical.CELL_SCALE_AS_UNIT)
        };
        return gridPos;
    }
    public static Vector2Int WorldToGridCoordinates(Vector3 worldPos, int width, int height, Vector2Int gridSize)
    {
        if (worldPos.x < 0) worldPos.x = 0;
        if (worldPos.y < 0) worldPos.y = 0;
        
        float xOffset = (width * Constants.Numerical.CELL_SCALE_AS_UNIT / 2);
        float yOffset = (height * Constants.Numerical.CELL_SCALE_AS_UNIT / 2);
        worldPos.x -= xOffset;
        worldPos.y -= yOffset;
        Vector2Int gridPos = new()
        {
            x = Mathf.FloorToInt(worldPos.x / Constants.Numerical.CELL_SCALE_AS_UNIT),
            y = Mathf.FloorToInt(worldPos.y / Constants.Numerical.CELL_SCALE_AS_UNIT)
        };
        gridPos.x = Mathf.Clamp(gridPos.x,0, gridSize.x - width);
        gridPos.y = Mathf.Clamp(gridPos.y,0, gridSize.y - height);
        return gridPos;
    }
    public static Vector3 GetCellWorldPosition(int x, int y)
    {
        Vector3 worldPos = new ()
        {
            x = Constants.Numerical.CELL_SCALE_AS_UNIT * x,
            y = Constants.Numerical.CELL_SCALE_AS_UNIT * y
        };
        return worldPos;
    }

    public static Vector3 GetUnitWorldPosition(int x, int y, int width, int height)
    {
        float xOffset = (width * Constants.Numerical.CELL_SCALE_AS_UNIT / 2);
        float yOffset = (height * Constants.Numerical.CELL_SCALE_AS_UNIT / 2);
        Vector3 worldPos = new ()
        {
            x = (Constants.Numerical.CELL_SCALE_AS_UNIT * x) + xOffset,
            y = (Constants.Numerical.CELL_SCALE_AS_UNIT * y) + yOffset
        };
        return worldPos;
    }

    public static CellModel GetCellOnPosition(Vector3 position, CellModel[,]grid)
    {
        Vector2Int gridSize = new(grid.GetLength(0), grid.GetLength(1));
        Vector2Int cellPos = WorldToGridCoordinates(position, gridSize);
        return grid[cellPos.x, cellPos.y];
    }
    
    public static CellModel[,] CreateGrid(int xLength, int Length)
    {
        var grid = new CellModel[xLength, Length];
        for (int i = 0; i < xLength; i++)
        {
            for (int j = 0; j < Length; j++)
            {
                CellModel cell = new (i, j, CellTypes.Walkable);
                grid[i, j] = cell;
            }
        }

        foreach (CellModel cellModel in grid)
        {
            cellModel.SetAdjacencyArray(grid);
        }
        return grid;
    }

    public static List<LevelEntityData> ToLevelEntityDataList(this List<MapEntity> entityList)
    {
        var items = new List<LevelEntityData>();
        foreach (MapEntity mapEntity in entityList)
        {
            LevelEntityData item = new()
            {
                guid = mapEntity.Data.Guid,
                height = mapEntity.Data.height,
                width = mapEntity.Data.width,
                x = mapEntity.Position.x,
                y = mapEntity.Position.y,
                poolIndex = mapEntity.transform.parent.GetSiblingIndex(),
                health = mapEntity.CurrentHealth
            };
            items.Add(item);
        }
        return items;
    }

    public static CellModel[,] CreateGrid(int[,] referencedAutomaton)
    {
        int xLen = referencedAutomaton.GetLength(0);
        int yLen = referencedAutomaton.GetLength(1);
        
        var grid = new CellModel[xLen, yLen];
        for (int i = 0; i < xLen; i++)
        {
            for (int j = 0; j < yLen; j++)
            {
                CellModel cell = new (i, j, (CellTypes)referencedAutomaton[i,j]);
                grid[i, j] = cell;
            }
        }

        foreach (CellModel cellModel in grid)
        {
            cellModel.SetAdjacencyArray(grid);
        }

        return grid;
    }

    public static bool IsValidPos(int xPos, int yPos, int xLen, int yLen)
    {
        return xPos >= 0 && yPos >= 0 && xPos <= xLen - 1 && yPos <= yLen - 1;
    }
    
    public static CellModel[] GetAdjacent(CellModel[,] arr, int xPosition,
        int yPosition)
    {
        int xLength = arr.GetLength(0);
        int yLength = arr.GetLength(1);

        var adjacentCells = new CellModel[8];

        if (IsValidPos(xPosition - 1, yPosition, xLength, yLength)) //left
        {
            adjacentCells[0] = (arr[xPosition - 1, yPosition]);
        }
        
        if (IsValidPos(xPosition, yPosition + 1, xLength, yLength)) // up
        {
            adjacentCells[2] =(arr[xPosition, yPosition + 1]);
        }

        if (IsValidPos(xPosition + 1, yPosition, xLength, yLength)) // right
        {
            adjacentCells[4] =(arr[xPosition + 1, yPosition]);
        }
        
        if (IsValidPos(xPosition, yPosition - 1, xLength, yLength)) // down
        {
            adjacentCells[6] =(arr[xPosition, yPosition - 1]);
        }

        if (IsValidPos(xPosition - 1, yPosition + 1, xLength, yLength)) //up left
        {
            adjacentCells[1] = (arr[xPosition - 1, yPosition + 1]);
        }

        if (IsValidPos(xPosition + 1, yPosition + 1, xLength, yLength)) //up right
        {
            adjacentCells[3] = (arr[xPosition + 1, yPosition + 1]);
        }
        
        if (IsValidPos(xPosition + 1, yPosition - 1, xLength, yLength)) //down right
        {
            adjacentCells[5] = (arr[xPosition + 1, yPosition - 1]);
        }
        
        if (IsValidPos(xPosition - 1, yPosition - 1, xLength, yLength)) //down left
        {
            adjacentCells[7] = (arr[xPosition - 1, yPosition - 1]);
        }
        

        return adjacentCells;
    }

}