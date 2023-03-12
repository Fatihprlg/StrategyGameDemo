using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class LevelAdapter
{
    public static List<MapEntity> LoadLevel(LevelModel level, GridViewModel gridView)
    {
        ClearScene();
        Texture2D tex = Helpers.Other.LoadTexture($"{Constants.Strings.RENDERED_TEXTURES_PATH}{level.name}.png");
        gridView.SetGridView(tex);
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
    
    public static void ClearScene()
    {
        var poolItems = Object.FindObjectsOfType<PoolItemModel>();
        foreach (PoolItemModel poolItemModel in poolItems)
        {
            poolItemModel.SetActiveGameObject(false);
        }
    }
}
