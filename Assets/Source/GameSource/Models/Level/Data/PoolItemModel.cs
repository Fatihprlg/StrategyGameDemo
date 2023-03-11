using System;
using UnityEngine;

[System.Serializable, RequireComponent(typeof(MapEntity))]
public class PoolItemModel : ItemModel<PoolItemDataModel>
{
    public int multiplePoolIndex;
    public int poolIndex;
    private string guid;
    private MapEntityData entityData;
    public override void SetValues(PoolItemDataModel data)
    {
        id = transform.GetSiblingIndex();
        multiplePoolIndex = transform.parent.parent.GetSiblingIndex();
        poolIndex = transform.parent.GetSiblingIndex();
        transform.position = data.Position;
        guid = entityData.Guid;
    }
    public override PoolItemDataModel GetData()
    {
        PoolItemDataModel dataModel = new ()
        {
            Id = id,
            poolIndex = poolIndex,
            multiplePoolIndex = multiplePoolIndex,
            Position = transform.position,
            guid = this.guid
        };
        return dataModel;
    }

    private void Reset()
    {
        entityData = GetComponent<MapEntity>().Data;
    }
}

[System.Serializable]
public class PoolItemDataModel : ItemDataModel
{
    public int multiplePoolIndex;
    public int poolIndex;
    public string guid;
    public Vector3 Position;
}