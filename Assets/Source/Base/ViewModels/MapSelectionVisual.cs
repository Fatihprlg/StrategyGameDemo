using System.Linq;
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
        mapButton.onClick.AddListener(() => 
        {
            PlayerDataModel.Data.LevelIndex = lvIndex;

        });
        mapButton.onClick.AddListener(ClearOldSave);
        mapButton.onClick.AddListener(onClick);
    }

    public void SetAnchoredPosition(Vector2 pos)
    {
        _rectTransform.anchoredPosition = pos;
    }

    private static void ClearOldSave()
    {
        LevelEntityDataList save =LevelDataModel.Data.levelEntityDatas.FirstOrDefault(d => d.levelIndex == PlayerDataModel.Data.LevelIndex);
        if (save is not null)
        {
            LevelDataModel.Data.levelEntityDatas.Remove(save);
        }
    }
}