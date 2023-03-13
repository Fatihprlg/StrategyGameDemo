using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MapSelectionVisual : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Button mapButton;
    [SerializeField] private TextMeshProUGUI mapName;
    [SerializeField] private Image mapImage;
    
    public void SetData(Sprite sprite, int lvIndex, UnityAction onClick)
    {
        mapImage.sprite = sprite;
        mapName.text = $"Map {lvIndex+1}";
        mapButton.onClick.RemoveAllListeners();
        mapButton.onClick.AddListener(() =>PlayerDataModel.Data.LevelIndex = lvIndex);
        mapButton.onClick.AddListener(onClick);
    }

    public void SetAnchoredPosition(Vector2 pos)
    {
        _rectTransform.anchoredPosition = pos;
    }
}