
using UnityEngine;

public class ProductionView : ScreenElement
{
    [SerializeField] private CanvasGroup _canvasGroup;
    
    [SerializeField] private ScreenElement[] subElements;
    
    public override void Initialize()
    {
        base.Initialize();
        EventManager.OnBuildingUISelected.AddListener(Hide);
        foreach (ScreenElement screenElement in subElements)
        {
            screenElement.Initialize();
        }
    }

    public void Hide(BuildingData data)
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
