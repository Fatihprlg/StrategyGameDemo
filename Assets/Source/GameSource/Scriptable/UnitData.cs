using UnityEngine;

[CreateAssetMenu(menuName = "Create UnitData", fileName = "UnitData", order = 0)]
public class UnitData : MapEntityData
{
    public int damage;
    public int range;
    public float moveSpeed;
    public float attackSpeed;
}