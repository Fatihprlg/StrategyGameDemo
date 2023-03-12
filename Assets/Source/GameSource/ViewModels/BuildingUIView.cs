using UnityEngine.EventSystems;

public class BuildingUIView : EntityUIView, IPointerClickHandler
{
    public bool IsClickable { get; set; } = true;
    private BuildingData currData;
    public override void SetData(MapEntityData data)
    {
        base.SetData(data);
        currData = data as BuildingData;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!IsClickable) return;
        EventManager.OnBuildingUISelected?.Invoke(currData);
    }
}