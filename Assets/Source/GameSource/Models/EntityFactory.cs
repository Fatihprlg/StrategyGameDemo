using UnityEngine;

public static class EntityFactory
{
    private static MultiplePoolModel entityPools;
    private static Registry registeredItems;
    public static void SetEntityPools(MultiplePoolModel _entityPools)
    {
        entityPools = _entityPools;
    }
    public static void SetRegistry(Registry _registry)
    {
        registeredItems = _registry;
    }

    public static MapEntity GetBuilding(BuildingData data, Teams team)
    {
        MapEntity entity = entityPools.GetDeactiveItem<MapEntity>(1);
        entity.SetData(data, team);
        return entity;
    }
    public static MapEntity GetUnit(UnitData data, Teams team)
    {
        MapEntity entity = entityPools.GetDeactiveItem<MapEntity>(0);
        entity.SetData(data, team);
        return entity;
    }

    public static MapEntity GetMapEntity(PoolItemDataModel itemData)
    {
        MapEntity entity = entityPools.GetDeactiveItem<MapEntity>(itemData.poolIndex);
        Debug.Log(itemData);
        MapEntityData data = registeredItems.FindByGuid(itemData.guid) as MapEntityData;
        entity.SetData(data, itemData.team);
        entity.transform.position = GridHelper.GetUnitWorldPosition((int)itemData.Position.x, (int)itemData.Position.y, itemData.width, itemData.height);
        entity.Position = itemData.Position;
        return entity;
    }
    public static MapEntity GetMapEntity(LevelEntityData itemData)
    {
        MapEntity entity = entityPools.GetDeactiveItem<MapEntity>(itemData.poolIndex);
        MapEntityData data = registeredItems.FindByGuid(itemData.guid) as MapEntityData;
        entity.SetData(data,itemData.team, itemData.health);
        entity.transform.position = GridHelper.GetUnitWorldPosition((int)itemData.x, (int)itemData.y, itemData.width, itemData.height);
        entity.Position = new Vector2Int((int)itemData.x, (int)itemData.y);
        return entity;
    }
}