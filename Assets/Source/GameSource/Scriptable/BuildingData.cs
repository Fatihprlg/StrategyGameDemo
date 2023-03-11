using UnityEngine;

[CreateAssetMenu(menuName = "Create BuildingData", fileName = "BuildingData", order = 0)]
public class BuildingData : MapEntityData
{
    public UnitData[] capableUnits;
}