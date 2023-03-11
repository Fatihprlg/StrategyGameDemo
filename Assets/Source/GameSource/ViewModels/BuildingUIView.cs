using UnityEngine.EventSystems;

public class BuildingUIView : EntityUIView, IPointerClickHandler
{
    private BuildingData currData;
    public override void SetData(MapEntityData data)
    {
        base.SetData(data);
        currData = data as BuildingData;
    }

    public BuildingData GetData() => currData;
    public void OnPointerClick(PointerEventData eventData)
    {
        EventManager.OnBuildingUISelected?.Invoke(this.GetData());
    }
}