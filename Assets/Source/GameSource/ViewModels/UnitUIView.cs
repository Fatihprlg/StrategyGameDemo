using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitUIView : EntityUIView, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI damageTxt;
    [SerializeField] private TextMeshProUGUI rangeTxt;
    [SerializeField] private TextMeshProUGUI speedTxt;

    public override void SetData(MapEntityData data)
    {
        base.SetData(data);
        UnitData unitData = data as UnitData;
        damageTxt.text = $"ATK: {unitData.damage.ToString()}";
        rangeTxt.text = $"RNG: {unitData.range.ToString()}";
        speedTxt.text = $"SPD: {unitData.moveSpeed.ToString()}";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EventManager.OnUnitUIClicked?.Invoke(this);
    }
}