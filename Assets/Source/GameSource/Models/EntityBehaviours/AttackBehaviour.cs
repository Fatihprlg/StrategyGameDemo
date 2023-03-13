using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AttackBehaviour : MapEntityBehaviour
{
    private MovementBehaviour _movementBehaviour;
    private Coroutine attackCoroutine;
    private Vector2Int targetPosOnLastFrame;
    private bool isTargetInRange;
    public override void Init(MapEntity attachedEntity)
    {
        base.Init(attachedEntity);
        _movementBehaviour = _attachedEntity.TryGetEntityBehaviour<MovementBehaviour>();
    }

    public void Attack(DamageableBehaviour damageableTarget)
    {
        if(!isActiveAndEnabled) return;
        if(damageableTarget.GetTeam == _attachedEntity.Team) return;
        ResetAttack();
        attackCoroutine = StartCoroutine(AttackCoroutine(damageableTarget));
    }

    private void ResetAttack()
    {
        if (attackCoroutine is not null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }

        DOTween.Kill(this);
    }
    
    private IEnumerator AttackCoroutine(DamageableBehaviour target)
    {
        UnitData unitData = (UnitData)_attachedEntity.Data;
        bool isTargetDead = target.isDead;
        while (!isTargetDead)
        {
            CheckRange(target.GetPosition());
            if (!isTargetInRange)
            {
                if (!_movementBehaviour)
                {
                    yield break;
                }
                targetPosOnLastFrame = target.GetPosition();
                _movementBehaviour.Move(GridHandler.Grid[target.GetPosition().x, target.GetPosition().y], ()=>CheckRangeWhileMoving(target));
                yield return new WaitUntil(() => isTargetInRange);
            }
            isTargetDead = target.DealDamage(unitData.damage);
            yield return new WaitForSeconds(unitData.attackSpeed);
        } 

        attackCoroutine = null;
    }

    private void CheckRangeWhileMoving(DamageableBehaviour target)
    {
        if (targetPosOnLastFrame != target.GetPosition())
        {
            _movementBehaviour.Move(GridHandler.Grid[target.GetPosition().x, target.GetPosition().y], ()=>CheckRangeWhileMoving(target));
            targetPosOnLastFrame = target.GetPosition();
            return;
        }
        if (CheckRange(target.GetPosition()))
        {
            _movementBehaviour.ResetMovement();
        }
        targetPosOnLastFrame = target.GetPosition();
    }
    
    private bool CheckRange(Vector2Int targetPos)
    {
        var range = ((UnitData)_attachedEntity.Data).range;
        isTargetInRange = Helpers.Vectors.IsPointInAreaRange(targetPos, _attachedEntity.Position, range);
        return isTargetInRange;
    }

    private void OnDisable()
    {
        ResetAttack();
    }
}