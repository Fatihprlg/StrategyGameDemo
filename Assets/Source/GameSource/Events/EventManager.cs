using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent<BuildingData> OnBuildingUISelected = new ();
    public static UnityEvent<UnitUIView> OnUnitUIClicked = new ();
    public static UnityEvent<MapEntity> OnMapEntitySelected = new ();
    public static UnityEvent<MapEntity> OnMapEntityDeselected = new ();
    public static UnityEvent OnConstructionEnd = new();


    public static void Reset()
    {
        OnBuildingUISelected.RemoveAllListeners();
        OnUnitUIClicked.RemoveAllListeners();
        OnMapEntitySelected.RemoveAllListeners();
        OnMapEntityDeselected.RemoveAllListeners();
        OnConstructionEnd.RemoveAllListeners();
    }
}
