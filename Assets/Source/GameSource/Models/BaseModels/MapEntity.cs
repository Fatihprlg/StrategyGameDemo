using System;
using System.Linq;
using UnityEngine;

public class MapEntity : MonoBehaviour
{
    public MapEntityData Data => data;
    public MapEntityViewModel EntityViewModel => _entityViewModel;
    public Teams Team => team;
    [NonSerialized] public Vector2Int Position;
    [NonSerialized] public int CurrentHealth;
    [SerializeField] private MapEntityData data;
    [SerializeField] private Teams team;
    [SerializeField] private MapEntityViewModel _entityViewModel;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private MapEntityBehaviour[] behaviours;
    
    
    public void SetData(MapEntityData entityData, Teams _team, int health = -1)
    {
        data = entityData;
        team = _team;
        CurrentHealth = health < 1 ? entityData.health : health;
        const float cellScale = Constants.Numerical.CELL_SCALE_AS_UNIT;
        _entityViewModel.SetTeamColor(team);
        _collider.size = new Vector3(entityData.width * cellScale, entityData.height * cellScale, 1);
        _entityViewModel.Init(data);
        foreach (MapEntityBehaviour mapEntityBehaviour in behaviours)
        {
            mapEntityBehaviour.Init(this);
        }
    }

    public void DestroyEntity()
    {
        GridHelper.DestructItemOnGrid(data.width, data.height, Position, GridHandler.Grid);
        gameObject.SetActive(false);
    }


    public T TryGetEntityBehaviour<T>() where T : MapEntityBehaviour
    {
        return behaviours.FirstOrDefault(b => b.GetType() == typeof(T)) as T;
    }
}