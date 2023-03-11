using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent<BuildingData> OnBuildingUISelected = new ();
    public static UnityEvent<UnitUIView> OnUnitUIClicked = new ();
    public static UnityEvent<MapEntityData> OnMapEntitySelected = new ();
    public static UnityEvent OnConstructionStateBegin = new();
    public static UnityEvent OnConstructionStateEnd = new ();
}
