using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitUIView : EntityUIView, IPointerClickHandler
{
    public bool IsClickable { get; set; }
    public MapEntity parentBuilding { get; set; }
    
    [SerializeField] private TextMeshProUGUI damageTxt;
    [SerializeField] private TextMeshProUGUI rangeTxt;
    [SerializeField] private TextMeshProUGUI speedTxt;
    private UnitData currentData;
    
    public override void SetData(MapEntityData data)
    {
        base.SetData(data);
        currentData = data as UnitData;
        damageTxt.text = $"ATK: {currentData.damage.ToString()}";
        rangeTxt.text = $"RNG: {currentData.range.ToString()}";
        speedTxt.text = $"SPD: {currentData.moveSpeed.ToString()}";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!IsClickable) return;
        SpawnerBehaviour spawner = parentBuilding.TryGetEntityBehaviour<SpawnerBehaviour>();
        bool isSpawned = spawner.TrySpawnUnit(currentData, GridHandler.Grid);
        IsClickable = isSpawned;
    }
}