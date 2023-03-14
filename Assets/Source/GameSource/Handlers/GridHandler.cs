
using System.Collections.Generic;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    public static CellModel[,] Grid { get; private set; }
    private static List<MapEntity> EntitiesOnGrid;

    public static void InitializeGrid(int[,] referenceAutomaton)
    {
        Grid = GridHelper.CreateGrid(referenceAutomaton);
        EntitiesOnGrid = new List<MapEntity>();
    }

    public static List<MapEntity> GetAllItemsOnGrid()
    {
        return EntitiesOnGrid;
    }

    public static void PlaceEntityOnGrid(MapEntity mapEntityData)
    {
        GridHelper.PlaceItemOnGrid(mapEntityData.Data.width,mapEntityData.Data.height, mapEntityData.Position, Grid);
        EntitiesOnGrid.Add(mapEntityData);
        EventManager.OnEntityPlacedOnMap?.Invoke(mapEntityData);
    } 

    public static void RemoveEntityOnGrid(MapEntity mapEntityData)
    {
        GridHelper.DestructItemOnGrid(mapEntityData.Data.width, mapEntityData.Data.height, mapEntityData.Position, Grid);
        EntitiesOnGrid.Remove(mapEntityData);
    }
    
    public static void PlaceEntitiesOnGrid(IEnumerable<MapEntity> entities)
    {
        foreach (MapEntity mapEntityData in entities)
        {
            GridHelper.PlaceItemOnGrid(mapEntityData.Data.width,mapEntityData.Data.height, mapEntityData.Position, Grid);
            EntitiesOnGrid.Add(mapEntityData);
            EventManager.OnEntityPlacedOnMap?.Invoke(mapEntityData);
        }
    }

    private void OnDrawGizmos()
    {
        if (Grid is not { Length: > 0 }) return;
        foreach (CellModel cellModel in Grid)
        {
            Gizmos.color = !cellModel.isEmpty || cellModel.CellType == CellTypes.NotWalkable
                ? Color.red
                : Color.green;
            Gizmos.DrawWireSphere(GridHelper.GetCellWorldPosition(cellModel.Position.x, cellModel.Position.y), .1f);
        }
    }

}