using System.Linq;
using UnityEngine;

public class SpawnerBehaviour : MapEntityBehaviour
{
    [SerializeField] private Transform spawnPoint;
    private Vector2Int spawnGridCoordinates;

    public override void Init(MapEntity attachedEntity)
    {
        base.Init(attachedEntity);
        spawnGridCoordinates = GridHelper.WorldToGridCoordinates(spawnPoint.position,
            new Vector2Int(GridHandler.Grid.GetLength(0), GridHandler.Grid.GetLength(1)));
    }

    public bool TrySpawnUnit(UnitData data, CellModel[,] grid)
    {
        BuildingData buildingData = _attachedEntity.Data as BuildingData;
        if (!buildingData) return false;
        if (!buildingData.capableUnits.FirstOrDefault(unit => unit.Guid == data.Guid)) return false;
        MapEntity spawnedUnit = EntityFactory.GetUnit(data);
        spawnedUnit.transform.position = GridHelper.GetUnitWorldPosition(spawnGridCoordinates.x, spawnGridCoordinates.y,
            data.width, data.height);
        spawnedUnit.Position = spawnGridCoordinates;
        GridHelper.PlaceItemOnGrid(data.width, data.height, spawnedUnit.Position, grid);
        spawnedUnit.SetActiveGameObject(true);
        return true;
    }
}