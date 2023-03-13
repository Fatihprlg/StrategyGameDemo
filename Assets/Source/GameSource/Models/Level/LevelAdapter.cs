using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class LevelAdapter
{
    public static List<MapEntity> LoadLevel(LevelModel level, GridViewModel gridView, bool editorMode = false)
    {
        ClearScene();
        Texture2D tex = Helpers.Other.LoadTexture($"{Constants.Strings.RENDERED_TEXTURES_PATH}{level.name}.png");
        gridView.SetGridView(tex);
        if(!editorMode)
        {
            LevelEntityDataList savedEntityData =
                LevelDataModel.Data.levelEntityDatas.FirstOrDefault(dataList => dataList.levelIndex == level.index);
            if (savedEntityData is { entityDatas: { Count: > 0 } })
            {
                return ActivatePoolObjects(savedEntityData.entityDatas);
            }
        }
        return ActivatePoolObjects(level.poolItems.ToArray());
    }
    private static List<MapEntity> ActivatePoolObjects(IEnumerable<PoolItemDataModel> items)
    {
        var placedEntities = new List<MapEntity>();
        
        
        foreach (PoolItemDataModel itemData in items)
        {
            MapEntity item = EntityFactory.GetMapEntity(itemData);
            item.SetActiveGameObject(true);
            placedEntities.Add(item);
        }

        return placedEntities;
    }
    private static List<MapEntity> ActivatePoolObjects(IEnumerable<LevelEntityData> items)
    {
        var placedEntities = new List<MapEntity>();
        
        
        foreach (LevelEntityData itemData in items)
        {
            MapEntity item = EntityFactory.GetMapEntity(itemData);
            item.SetActiveGameObject(true);
            placedEntities.Add(item);
        }

        return placedEntities;
    }
    
    public static void ClearScene()
    {
        var poolItems = Object.FindObjectsOfType<PoolItemModel>();
        foreach (PoolItemModel poolItemModel in poolItems)
        {
            poolItemModel.SetActiveGameObject(false);
        }
    }
}
