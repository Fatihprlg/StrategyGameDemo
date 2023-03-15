using System.Collections.Generic;
using UnityEngine;

public static class GridHandler
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
        GridHelper.PlaceItemOnGrid(mapEntityData.Data.width, mapEntityData.Data.height, mapEntityData.Position, Grid);
        EntitiesOnGrid.Add(mapEntityData);
        EventManager.OnEntityPlacedOnMap?.Invoke(mapEntityData);
    }

    public static void RemoveEntityOnGrid(MapEntity mapEntityData)
    {
        GridHelper.DestructItemOnGrid(mapEntityData.Data.width, mapEntityData.Data.height, mapEntityData.Position,
            Grid);
        EntitiesOnGrid.Remove(mapEntityData);
    }

    public static void PlaceEntitiesOnGrid(IEnumerable<MapEntity> entities)
    {
        foreach (MapEntity mapEntityData in entities)
        {
            GridHelper.PlaceItemOnGrid(mapEntityData.Data.width, mapEntityData.Data.height, mapEntityData.Position,
                Grid);
            EntitiesOnGrid.Add(mapEntityData);
            EventManager.OnEntityPlacedOnMap?.Invoke(mapEntityData);
        }
    }
}