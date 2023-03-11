using UnityEngine;

public static class GridHelper
{

    public static Vector3 GetCellWorldPosition(int x, int y)
    {
        Vector3 worldPos = new ()
        {
            x = Constants.Numerical.CELL_SCALE_AS_UNIT * x,
            y = Constants.Numerical.CELL_SCALE_AS_UNIT * y
        };
        return worldPos;
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

        return grid;
    }

    public static CellModel[,] CreateGrid(int[,] referencedAutomaton)
    {
        int xLen = referencedAutomaton.GetLength(0);
        int yLen = referencedAutomaton.GetLength(0);
        
        var grid = new CellModel[xLen, yLen];
        for (int i = 0; i < xLen; i++)
        {
            for (int j = 0; j < yLen; j++)
            {
                CellModel cell = new (i, j, (CellTypes)referencedAutomaton[i,j]);
                grid[i, j] = cell;
            }
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