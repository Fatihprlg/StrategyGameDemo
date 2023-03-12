
using System.Collections.Generic;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    public static CellModel[,] Grid { get; private set; }

    public static void InitializeGrid(int[,] referenceAutomaton)
    {
        Grid = GridHelper.CreateGrid(referenceAutomaton);
    }

    public static void PlaceEntitiesOnGrid(IEnumerable<MapEntity> entities)
    {
        foreach (MapEntity mapEntityData in entities)
        {
            GridHelper.PlaceItemOnGrid(mapEntityData.Data.width,mapEntityData.Data.height, mapEntityData.Position, Grid);
        }
    }
    

    private void OnDrawGizmos()
    {
        if (Grid is { Length: > 0 })
        {
            foreach (CellModel cellModel in Grid)
            {
                Gizmos.color = !cellModel.isEmpty || cellModel.CellType == CellTypes.NotWalkable
                    ? Color.red
                    : Color.green;
                Gizmos.DrawWireSphere(GridHelper.GetCellWorldPosition(cellModel.Position.x, cellModel.Position.y), .1f);
            }
        }
    }

    
}