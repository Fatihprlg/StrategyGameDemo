using UnityEngine;

public class MapEntityViewModel : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Icon;
    [SerializeField] private Transform healthBar;
    
    public void Init(MapEntityData data)
    {
        Icon.sprite = data.visual;
        healthBar.localScale = Vector3.one;
        UpdateHealthBar(1);
    }

    public void UpdateHealthBar(float percentage)
    {
        Vector3 currentSize = healthBar.localScale;
        currentSize.x = percentage;
        healthBar.localScale = currentSize;
    }
}