using UnityEngine;

public class DamageableBehaviour : MapEntityBehaviour
{
    public bool isDead { get; private set; }
    public Teams GetTeam => _attachedEntity.Team;
    public Vector2Int GetPosition() => _attachedEntity.Position;
    public Vector2Int GetSize() => new Vector2Int(_attachedEntity.Data.width, _attachedEntity.Data.height);
    public bool DealDamage(int damage)
    {
        if (!isActiveAndEnabled) return true;
        _attachedEntity.CurrentHealth -= damage;
        _attachedEntity.EntityViewModel.UpdateHealthBar((float)_attachedEntity.CurrentHealth/(float)_attachedEntity.Data.health);
        print($"{gameObject.name} deal damage: {damage}, current health: {_attachedEntity.CurrentHealth}");
        isDead = _attachedEntity.CurrentHealth <= 0;
        if (isDead)
        {
            _attachedEntity.DestroyEntity();
            EventManager.OnMapEntityDestroyed?.Invoke(_attachedEntity);
        }
        return isDead;
    }
}