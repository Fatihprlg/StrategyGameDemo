using System.Collections.Generic;
using UnityEngine;

public class ProductionMenuViewModel : ScreenElement
{
    [SerializeField] private List<BuildingData> _listingBuildings;
    
    [Header("References")]
    [SerializeField] private BuildingUIView _buildingUIPrefab;
    [SerializeField] private RectTransform _layoutGroup;
    [SerializeField] private InfiniteScrollView _infiniteScrollView;
    
    public override void Initialize()
    {
        base.Initialize();
        ListBuildings(_infiniteScrollView.GetNecessaryElementCount());
        _infiniteScrollView.Init();
    }
    
    private void ListBuildings(int atLeast)
    {
        
        int iteration = Mathf.CeilToInt((atLeast + 2) / _listingBuildings.Count);

        for (int i = 0; i < iteration; i++)
            foreach (BuildingData buildingType in _listingBuildings)
            {
                BuildingUIView buildingUI = Instantiate(_buildingUIPrefab, _layoutGroup);
                buildingUI.SetData(buildingType);
            }
    }
}