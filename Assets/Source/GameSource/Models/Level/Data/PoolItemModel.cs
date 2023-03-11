using System;
using UnityEngine;

[System.Serializable, RequireComponent(typeof(MapEntity))]
public class PoolItemModel : ItemModel<PoolItemDataModel>
{
    public int multiplePoolIndex;
    public int poolIndex;
    [SerializeField] private MapEntity entityData;
    public override void SetValues()
    {
        id = transform.GetSiblingIndex();
        multiplePoolIndex = transform.parent.parent.GetSiblingIndex();
        poolIndex = transform.parent.GetSiblingIndex();
    }
    public override PoolItemDataModel GetData()
    {
        transform.position = GridHelper.WorldToGridPosition(transform.position, entityData.Data.width, entityData.Data.height);
        SetValues();
        PoolItemDataModel dataModel = new ()
        {
            Id = id,
            poolIndex = poolIndex,
            multiplePoolIndex = multiplePoolIndex,
            Position = transform.position,
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
    public int multiplePoolIndex;
    public int poolIndex;
    public int width;
    public int height;
    public string guid;
    public Vector3 Position;
}