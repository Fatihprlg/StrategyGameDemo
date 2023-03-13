using System.Linq;
using UnityEngine;

public class SpawnerBehaviour : MapEntityBehaviour
{
    [SerializeField] private Transform spawnPoint;

    public bool TrySpawnUnit(UnitData data, CellModel[,] grid)
    {
        if (!isActiveAndEnabled) return false; 
        Vector2Int spawnGridCoordinates = GridHelper.WorldToGridCoordinates(spawnPoint.position,
            new Vector2Int(GridHandler.Grid.GetLength(0), GridHandler.Grid.GetLength(1)));
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
}