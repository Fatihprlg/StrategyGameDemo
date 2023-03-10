using System;
using System.Data;
using UnityEngine;


public class CellViewModel : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ground;
    [SerializeField] private SpriteRenderer constructionStateMask;
    public void SetCellView(CellModel data)
    {
        switch (data.CellType)
        {
            case CellTypes.Walkable:
                ground.color = Helpers.Colors.ToColor(Constants.Colors.WALKABLE_AREA_COLOR);
                constructionStateMask.color = Color.green;
                break;
            case CellTypes.NotWalkable:
                ground.color = Helpers.Colors.ToColor(Constants.Colors.NOT_WALKABLE_AREA_COLOR);
                constructionStateMask.color = Color.red;
                break;
            default:
                ground.color = Helpers.Colors.ToColor(Constants.Colors.WALKABLE_AREA_COLOR);
                constructionStateMask.color = Color.green;
                break;
        }
        transform.position = new Vector3(data.Position.x, data.Position.y, 0);
    }

    public void ChangeMaskColor(bool isAvailable)
    {
        constructionStateMask.color = isAvailable ? Color.green : Color.red;
    }

    public void ConstructionState(bool state)
    {
        constructionStateMask.SetActiveGameObject(state);
    }
}