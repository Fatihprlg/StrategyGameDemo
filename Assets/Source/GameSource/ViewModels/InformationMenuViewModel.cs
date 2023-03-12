using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationMenuViewModel : ScreenElement
{
    [SerializeField] private BuildingUIView buildingUI;
    [SerializeField] private UnitUIView unitUI;
    [SerializeField] private UnitUIView[] productionUnitUIViews;
    [SerializeField] private GameObject productions;

    public override void Initialize()
    {
        EventManager.OnMapEntitySelected.AddListener(ShowMapEntityInfo);
        EventManager.OnMapEntityDeselected.AddListener(entity => ClearInfoView());
    }

    private void ShowBuildingInfo(MapEntity entity)
    {
        ClearInfoView();
        BuildingData buildingData = entity.Data as BuildingData;
        buildingUI.SetData(buildingData);
        buildingUI.SetActiveGameObject(true);
        buildingUI.IsClickable = false;
        if(buildingData.capableUnits.Length == 0) return;
        productions.SetActive(true);
        for (int i = 0; i < buildingData.capableUnits.Length; i++)
        {
            productionUnitUIViews[i].SetData(buildingData.capableUnits[i]);
            productionUnitUIViews[i].IsClickable = true;
            productionUnitUIViews[i].parentBuilding = entity;
            productionUnitUIViews[i].SetActiveGameObject(true);
        }
    }

    private void ShowMapEntityInfo(MapEntity entity)
    {
        ClearInfoView();
        MapEntityData data = entity.Data;
        if (data.GetType() == typeof(UnitData))
        {
            ShowUnitInfo(data as UnitData);
        }
        else
        {
            ShowBuildingInfo(entity);
        }
    }
    
    private void ShowUnitInfo(UnitData unitData)
    {
        ClearInfoView();
        unitUI.SetData(unitData);
        unitUI.SetActiveGameObject(true);
        unitUI.IsClickable = false;
    }

    private void ClearInfoView()
    {
        unitUI.SetActiveGameObject(false);
        buildingUI.SetActiveGameObject(false);
        foreach (UnitUIView productionUnitUIView in productionUnitUIViews)
        {
            productionUnitUIView.SetActiveGameObject(false);
        }
        productions.SetActive(false);
    }
    
}
