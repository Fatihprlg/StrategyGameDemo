using System.Collections.Generic;
using UnityEngine;

public class MapEntitiesController : ControllerBase
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private LayerMask entityLayerMask;
    [Dependency] private GameController _gameController;
    private Ray leftClickRay;
    private Ray rightClickRay;
    private MapEntity selectedEntity;
    private List<MapEntity> enemyEntities;
    private List<MapEntity> allyEntities;
    private Teams allyTeam;

    public override void Initialize()
    {
        this.Inject();
        allyTeam = PlayerDataModel.Data.PlayerTeam;
        EventManager.OnEntityPlacedOnMap.AddListener(OnEntityPlacedOnMap);
        EventManager.OnMapEntityDestroyed.AddListener(OnEntityDestroyed);
    }

    public override void ControllerUpdate(GameStates currentState)
    {
        if(currentState != GameStates.Game) return;
        _inputManager.PointerUpdate();
    }

    public void OnPointerDown()
    {
        if(_inputManager.IsPointerOverUIElement()) return;
        DeselectEntity();
        leftClickRay = CameraController.MainCamera.ScreenPointToRay(_inputManager.PointerDownPosition);
        if (!Physics.Raycast(leftClickRay, out RaycastHit hit, 20, entityLayerMask)) return;
        if (!hit.collider.TryGetComponent(out MapEntity entity)) return;
        SelectEntity(entity);
    }

    public void OnRightClick()
    {
        if(!selectedEntity || _inputManager.IsPointerOverUIElement()) return;
        MovementBehaviour movement = selectedEntity.TryGetEntityBehaviour<MovementBehaviour>();
        AttackBehaviour attack = selectedEntity.TryGetEntityBehaviour<AttackBehaviour>();
        rightClickRay =CameraController.MainCamera.ScreenPointToRay(_inputManager.RightPointerDownPosition);
        if (attack && Physics.Raycast(rightClickRay, out RaycastHit hit, 20, entityLayerMask))
        {
            if (!hit.collider.TryGetComponent(out DamageableBehaviour entity)) return;
            attack.Attack(entity);
        }
        else if (movement)
        {
            Vector3 worldPos = Helpers.Vectors.ScreenToWorldPoint(_inputManager.RightPointerDownPosition, CameraController.MainCamera);
            CellModel dest = GridHelper.GetCellOnPosition(worldPos, GridHandler.Grid);
            movement.Move(dest);
        }
    }

    private void SelectEntity(MapEntity entity)
    {
        if(entity.Team != PlayerDataModel.Data.PlayerTeam) return;
        selectedEntity = entity;
        EventManager.OnMapEntitySelected?.Invoke(entity);
    }

    private void DeselectEntity()
    {
        EventManager.OnMapEntityDeselected?.Invoke(selectedEntity);
        selectedEntity = null;
    }

    private void OnEntityPlacedOnMap(MapEntity entity)
    {
        if(entity.Team == allyTeam)
            allyEntities.Add(entity);
        else
            enemyEntities.Add(entity);
    }

    private void OnEntityDestroyed(MapEntity entity)
    {
        if (entity.Team == allyTeam)
        {
            allyEntities.Remove(entity);
            if (allyEntities.Count == 0)
            {
                _gameController.EndState(false);
            }
        }
        else
        {
            enemyEntities.Remove(entity);
            if (enemyEntities.Count == 0)
            {
                _gameController.EndState(true);
            }
            //TODO: Give notification when any team destroyed
        }
        
    }
    
}