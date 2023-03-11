using UnityEngine;

public class MapEntityViewModel : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Icon;
    [SerializeField] private SpriteRenderer healthBar;
    
    public void Init(MapEntityData data)
    {
        Icon.sprite = data.visual;
        healthBar.transform.localScale = Vector3.one;
    }

    public void UpdateHealthBar(int percentage)
    {
        Vector3 currentSize = healthBar.transform.localScale;
        currentSize.x = percentage;
        healthBar.transform.localScale = currentSize;
    }
}