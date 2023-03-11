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
        EventManager.OnBuildingUISelected.AddListener(ShowBuildingInfo);
        EventManager.OnMapEntitySelected.AddListener(ShowMapEntityInfo);
    }

    public void ShowBuildingInfo(BuildingData buildingData)
    {
        ClearInfoView();
        buildingUI.SetData(buildingData);
        buildingUI.SetActiveGameObject(true);
        if(buildingData.capableUnits.Length == 0) return;
        productions.SetActive(true);
        for (int i = 0; i < buildingData.capableUnits.Length; i++)
        {
            productionUnitUIViews[i].SetData(buildingData.capableUnits[i]);
            productionUnitUIViews[i].SetActiveGameObject(true);
        }
    }

    private void ShowMapEntityInfo(MapEntityData data)
    {
        ClearInfoView();
        if (data.GetType() == typeof(UnitData))
        {
            ShowUnitInfo(data as UnitData);
        }
        else
        {
            ShowBuildingInfo(data as BuildingData);
        }
    }
    
    public void ShowUnitInfo(UnitData unitData)
    {
        ClearInfoView();
        unitUI.SetData(unitData);
        unitUI.SetActiveGameObject(true);
    }

    public void ClearInfoView()
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
