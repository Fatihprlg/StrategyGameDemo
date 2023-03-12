using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionView : ScreenElement
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private ScreenElement[] subElements;

    public override void Initialize()
    {
        base.Initialize();
        EventManager.OnBuildingUISelected.AddListener(Show);
        EventManager.OnConstructionEnd.AddListener(Hide);
        foreach (ScreenElement screenElement in subElements)
        {
            screenElement.Initialize();
        }
    }

    public void OnCloseButtonClicked()
    {
        EventManager.OnConstructionEnd?.Invoke();
    }

    public void Show(BuildingData data)
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}