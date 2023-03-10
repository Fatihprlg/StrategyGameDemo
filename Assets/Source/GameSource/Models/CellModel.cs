
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CellModel
{
    public CellModel[] AdjacentCells { get; private set; }
    public CellTypes CellType => cellType;
    public Vector2Int Position => new (x, y);
    public bool isEmpty = true;
    private readonly CellTypes cellType;
    private readonly int x;
    private readonly int y;

    public CellModel(int xPos, int yPos, CellTypes type)
    {
        x = xPos;
        y = yPos;
        cellType = type;
        AdjacentCells = new CellModel[8];
    }

    public void SetAdjacencyArray(CellModel[,] grid)
    {
        AdjacentCells = GridHelper.GetAdjacent(grid, x, y);
    }
    
}