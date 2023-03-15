using System.Linq;
using UnityEngine;

public class SpawnerBehaviour : MapEntityBehaviour
{
    [SerializeField] private Transform spawnPoint;
    
    public bool TrySpawnUnit(UnitData data, CellModel[,] grid)
    {
        if (!isActiveAndEnabled) return false;
        Vector2Int spawnGridCoordinates = GetEmptySpawnPoint(grid);
        if (spawnGridCoordinates.x < 0)
        {
            return false;
        }
        BuildingData buildingData = _attachedEntity.Data as BuildingData;
        if (!buildingData) return false;
        if (!buildingData.capableUnits.FirstOrDefault(unit => unit.Guid == data.Guid)) return false;
        MapEntity spawnedUnit = EntityFactory.GetUnit(data, _attachedEntity.Team);
        spawnedUnit.transform.position = GridHelper.GetUnitWorldPosition(spawnGridCoordinates.x, spawnGridCoordinates.y,
            data.width, data.height);
        spawnedUnit.Position = spawnGridCoordinates;
        GridHandler.PlaceEntityOnGrid(spawnedUnit);
        spawnedUnit.SetActiveGameObject(true);
        return true;
    }

    private Vector2Int GetEmptySpawnPoint(CellModel[,] grid)
    {
        Vector2Int spawnGridCoordinates = GridHelper.WorldToGridCoordinates(spawnPoint.position,grid);
        if (grid[spawnGridCoordinates.x, spawnGridCoordinates.y].isEmpty) return spawnGridCoordinates;
        var perimeter = GridHelper.GetPerimeterOfArea(_attachedEntity.Data.width, _attachedEntity.Data.height,
            _attachedEntity.Position, GridHandler.Grid);

        foreach (CellModel cellModel in perimeter)
        {
            if (cellModel.isEmpty) return cellModel.Position;
        }
        
        return Vector2Int.one * -1;
    }
}