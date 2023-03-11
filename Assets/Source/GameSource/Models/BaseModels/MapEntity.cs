using UnityEngine;

public class MapEntity : MonoBehaviour
{
    public MapEntityData Data => data;
    [SerializeField] private MapEntityData data;
    [SerializeField] private MapEntityViewModel _entityViewModel;
    private int currentHealth;
    
    public void SetData(MapEntityData entityData)
    {
        data = entityData;
        currentHealth = entityData.health;
        _entityViewModel.Init(data);
    }

    public void DealDamage(int damage)
    {
        currentHealth -= damage;
        _entityViewModel.UpdateHealthBar(currentHealth/data.health);
    }
}