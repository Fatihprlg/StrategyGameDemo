using System;
using UnityEngine;

[System.Serializable, RequireComponent(typeof(MapEntity))]
public class PoolItemModel : ItemModel<PoolItemDataModel>
{
    public int poolIndex;
    [SerializeField] private MapEntity entityData;
    public override void SetValues()
    {
        id = transform.GetSiblingIndex();
        poolIndex = transform.parent.GetSiblingIndex();
    }
    public override PoolItemDataModel GetData()
    {
        Vector2Int gridPos = GridHelper.WorldToGridCoordinates(transform.position, entityData.Data.width, entityData.Data.height);
        SetValues();
        PoolItemDataModel dataModel = new ()
        {
            Id = id,
            poolIndex = poolIndex,
            Position = gridPos,
            width = entityData.Data.width,
            height = entityData.Data.height,
            guid = entityData.Data.Guid
        };
        return dataModel;
    }

    private void Reset()
    {
        entityData = GetComponent<MapEntity>();
    }
}

[System.Serializable]
public class PoolItemDataModel : ItemDataModel
{
    public int poolIndex;
    public int width;
    public int height;
    public string guid;
    public Teams team;
    public Vector2Int Position;
}