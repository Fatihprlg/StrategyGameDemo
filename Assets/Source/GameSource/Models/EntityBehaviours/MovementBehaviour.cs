using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEngine;

public class MovementBehaviour : MapEntityBehaviour
{
    private Coroutine movementCoroutine;
    
    public void Move(CellModel destination, Action onStepComplete = null)
    {
        if(!isActiveAndEnabled) return;
        ResetMovement();
        var path = PathFinder.FindPath(_attachedEntity.Position, destination.Position, GridHandler.Grid);
        movementCoroutine = StartCoroutine(MovementCoroutine(path, onStepComplete));
    }

    private IEnumerator MovementCoroutine(IList<Vector2Int> path, Action onStepComplete)
    {
        float durationPerWp = 1 / ((UnitData)_attachedEntity.Data).moveSpeed;
        while (path.Count != 0)
        {
            if(!transform) yield break;
            Vector2Int wp = path[0];
            path.RemoveAt(0);
            Vector3 wpWorldPos = GridHelper.GetUnitWorldPosition(wp.x, wp.y, _attachedEntity.Data.width, _attachedEntity.Data.height);
            transform.DOMove(wpWorldPos, durationPerWp).SetEase(Ease.Linear).SetId(this);
            yield return new WaitForSeconds(durationPerWp);
            GridHelper.DestructItemOnGrid(_attachedEntity.Data.width,_attachedEntity.Data.height, _attachedEntity.Position, GridHandler.Grid);
            _attachedEntity.Position = wp;
            GridHelper.PlaceItemOnGrid(_attachedEntity.Data.width,_attachedEntity.Data.height, _attachedEntity.Position, GridHandler.Grid);
            onStepComplete?.Invoke();
        }
        yield return null;
        movementCoroutine = null;
    }

    public void ResetMovement()
    {
        if (movementCoroutine is not null)
        {
            StopCoroutine(movementCoroutine);
            movementCoroutine = null;
        }
        DOTween.Kill(this);
    }
    
    private void OnDisable()
    {
        ResetMovement();
    }
}