using System.Collections.Generic;
using UnityEngine;

public static class LevelAdapter
{
    private static SpriteRenderer gridRenderer;
    public static void Init(SpriteRenderer _gridRenderer) => gridRenderer = _gridRenderer;
    public static void LoadLevel(LevelModel level)
    {
        ClearScene();
        Texture2D tex = Helpers.Other.LoadTexture($"{Constants.Strings.RENDERED_TEXTURES_PATH}{level.name}.png");
        gridRenderer.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        ActivatePoolObjects(level.poolItems.ToArray());
    }
    public static void ActivatePoolObjects(IEnumerable<PoolItemDataModel> items)
    {
        foreach (PoolItemDataModel itemData in items)
        {
            MapEntity item = EntityFactory.GetMapEntity(itemData);
            item.SetActiveGameObject(true);
        }
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
