using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static readonly UnityEvent<BuildingData> OnBuildingUISelected = new ();
    public static readonly UnityEvent<MapEntity> OnMapEntitySelected = new ();
    public static readonly UnityEvent<MapEntity> OnMapEntityDeselected = new ();
    public static readonly UnityEvent<MapEntity> OnMapEntityDestroyed = new();
    public static readonly UnityEvent<MapEntity> OnEntityPlacedOnMap = new();
    public static readonly UnityEvent OnLevelLoaded = new();
    public static readonly UnityEvent OnLevelEnded = new();

    public static readonly UnityEvent OnConstructionEnd = new();
    
    
    /*
    public static void Reset()
    {
        OnBuildingUISelected.RemoveAllListeners();
        OnMapEntitySelected.RemoveAllListeners();
        OnMapEntityDeselected.RemoveAllListeners();
        OnConstructionEnd.RemoveAllListeners();
    }*/
}
