using System;
using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;

public class BuildingConstructor : MonoBehaviour, IInitializable
{
    [SerializeField] private int buildingOverlapOffset;
    [SerializeField] private SpriteRenderer ghost;
    [SerializeField] private InputManager _inputManager;
    private Vector2Int gridSize;
    private BuildingData buildingToConstruct;
    private Camera mainCam;
    private bool constructionState;
    private bool isLocationAvailable;
    private bool isHoveringExitButton;
    
    public void Initialize()
    {
        EventManager.OnBuildingUISelected.AddListener(OnBuildingUISelected);
        EventManager.OnConstructionEnd.AddListener(Reset);
        mainCam = CameraController.MainCamera; 
        gridSize = new Vector2Int(GridHandler.Grid.GetLength(0), GridHandler.Grid.GetLength(1));
    }

    public void HoveringExitButton(bool hoverState) => isHoveringExitButton = hoverState;

    private void Update()
    {
        if(!constructionState) return;
        _inputManager.PointerUpdate();
    }

    private void Reset()
    {
        buildingToConstruct = null;
        constructionState = false;
        isLocationAvailable = false;
        isHoveringExitButton = false;
        ghost.SetActiveGameObject(false);
    }

    public void ConstructBuilding()
    {
        if(!isLocationAvailable || isHoveringExitButton) return;
        Vector2Int gridPos = GridHelper.WorldToGridCoordinates(ghost.transform.position, buildingToConstruct.width, buildingToConstruct.height, gridSize);
        MapEntity entity = EntityFactory.GetBuilding(buildingToConstruct, PlayerDataModel.Data.PlayerTeam);
        entity.Position = gridPos;
        entity.transform.position = GridHelper.GetUnitWorldPosition(gridPos.x, gridPos.y, buildingToConstruct.width, buildingToConstruct.height);
        GridHandler.PlaceEntityOnGrid(entity);
        entity.SetActiveGameObject(true);
        EventManager.OnConstructionEnd?.Invoke();
    }
    
    public void MoveGhostOnMap()
    {
        Vector3 worldPos = Helpers.Vectors.ScreenToWorldPoint(_inputManager.PointerIdlePosition, mainCam);
        ghost.transform.position = worldPos.WorldToGridPosition(gridSize, buildingToConstruct.width, buildingToConstruct.height);
        CheckLocationIsAvailable();
    }

    private void CheckLocationIsAvailable()
    {
        Vector2Int location = GridHelper.WorldToGridCoordinates(ghost.transform.position, buildingToConstruct.width, buildingToConstruct.height, gridSize);
        isLocationAvailable = GridHelper.CheckOverlappingAreaIsAvailable(buildingToConstruct.width,
            buildingToConstruct.height, location, GridHandler.Grid, buildingOverlapOffset);
        ghost.color = isLocationAvailable && !isHoveringExitButton ? Color.white : Color.red;
    }
    
    private void OnBuildingUISelected(BuildingData data)
    {
        ghost.sprite = data.visual;
        buildingToConstruct = data;
        ghost.SetActiveGameObject(true);
        constructionState = true;
    }
    
}
