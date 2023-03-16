using System;
using UnityEngine;

public class MapEntityViewModel : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Icon;
    [SerializeField] private SpriteRenderer teamMark;
    [SerializeField] private SpriteRenderer selectedUI;
    [SerializeField] private Transform healthBar;
    
    public void Init(MapEntityData data)
    {
        Icon.sprite = data.visual;
        healthBar.localScale = Vector3.one;
        UpdateHealthBar(1);
    }

    public void SetSelectedUI(bool state)
    {
        selectedUI.SetActiveGameObject(state);
    }
    
    public void UpdateHealthBar(float percentage)
    {
        Vector3 currentSize = healthBar.localScale;
        currentSize.x = percentage;
        healthBar.localScale = currentSize;
    }

    public void SetTeamColor(Teams team)
    {
        teamMark.color = team switch
        {
            Teams.Blue => Helpers.Colors.ToColor(Constants.Colors.BLUE),
            Teams.Red => Helpers.Colors.ToColor(Constants.Colors.RED),
            Teams.Green => Helpers.Colors.ToColor(Constants.Colors.GREEN),
            _ => throw new ArgumentOutOfRangeException(nameof(team), team, "Team Null")
        };
    }
}