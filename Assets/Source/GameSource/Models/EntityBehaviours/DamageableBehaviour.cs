public class DamageableBehaviour : MapEntityBehaviour
{
    public void DealDamage(int damage)
    {
        _attachedEntity.CurrentHealth -= damage;
        _attachedEntity.EntityViewModel.UpdateHealthBar(_attachedEntity.CurrentHealth/_attachedEntity.Data.health);
    }
}