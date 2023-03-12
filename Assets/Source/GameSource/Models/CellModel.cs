
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CellModel
{
    public CellPathOptions PathOptions;
    public CellModel[] AdjacentCells { get; private set; }
    public CellTypes CellType { get; }
    public Vector2Int Position => new (x, y);
    public bool isEmpty;
    private readonly int x;
    private readonly int y;

    public CellModel(int xPos, int yPos, CellTypes type)
    {
        x = xPos;
        y = yPos;
        CellType = type;
        AdjacentCells = new CellModel[8];
        PathOptions = new CellPathOptions();
        isEmpty = true;
    }

    public void SetAdjacencyArray(CellModel[,] grid)
    {
        AdjacentCells = GridHelper.GetAdjacent(grid, x, y);
    }
    
}