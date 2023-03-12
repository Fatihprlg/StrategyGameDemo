using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MovementBehaviour : MapEntityBehaviour
{
    private Coroutine movementCoroutine;
    
    public void Move(CellModel destination)
    {
        ResetMovement();
        var path = PathFinder.FindPath(_attachedEntity.Position, destination.Position, GridHandler.Grid);
        movementCoroutine = StartCoroutine(MovementCoroutine(path));
    }

    private IEnumerator MovementCoroutine(List<Vector2Int> path)
    {
        yield return null;
        //TODO: Duration Will be set
        float duration = (_attachedEntity.Position - path[^1]).magnitude * ((UnitData)_attachedEntity.Data).moveSpeed;
        float durationPerWp = duration / path.Count;
        while (path.Count > 0)
        {
            if(!isActiveAndEnabled) yield break;
            GridHelper.DestructItemOnGrid(_attachedEntity.Data.width,_attachedEntity.Data.height, _attachedEntity.Position, GridHandler.Grid);
            Vector2Int wp = path[0];
            path.RemoveAt(0);
            Vector3 wpWorldPos = GridHelper.GetUnitWorldPosition(wp.x, wp.y, _attachedEntity.Data.width, _attachedEntity.Data.height);
            transform.DOMove(wpWorldPos, durationPerWp).SetEase(Ease.Linear).SetId(this);
            yield return new WaitForSeconds(durationPerWp);
            _attachedEntity.Position = wp;
            GridHelper.PlaceItemOnGrid(_attachedEntity.Data.width,_attachedEntity.Data.height, _attachedEntity.Position, GridHandler.Grid);
        }
    }

    public void ResetMovement()
    {
        if(movementCoroutine is not null)
            StopCoroutine(movementCoroutine);
        DOTween.Kill(this);
    }
    
    private void OnDisable()
    {
        ResetMovement();
    }
}