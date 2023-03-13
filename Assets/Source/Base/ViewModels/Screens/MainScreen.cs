using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class MainScreen : ScreenModel
{
    public void PlayLastGameButton()
    {
        
    }

    public void NewGameButton()
    {
        
    }

    public void LoadGameButton()
    {
        
    }

    public void BackButton()
    {
        
    }
}

public class MapsView : ScreenElement
{
    [SerializeField] private float offset;
    [SerializeField] private float spacing;
    [SerializeField] private float elementWidth;
    [SerializeField] private Object levels;
    [SerializeField] private MapSelectionVisual _mapSelectionVisualPrefab;
    [SerializeField] private RectTransform _scrollRect;
    
    public override void Initialize()
    {
        base.Initialize();
        RectTransform rectTransform = GetComponent<RectTransform>();
        LevelList levelModels = JsonHelper.LoadJson<LevelList>(levels.ToString());
        float topX = rectTransform.rect.height * .5f;
        for (int index = 0; index < levelModels.list.Count; index++)
        {
            MapSelectionVisual visual = Instantiate(_mapSelectionVisualPrefab, _scrollRect);
            Vector2 childPos = visual.transform.localPosition;
            childPos.y = 0;
            childPos.x = topX - (index - 1) * (elementWidth + spacing);
            visual.transform.localPosition = childPos;
            LevelModel level = levelModels.list[index];
            Texture2D tex = Helpers.Other.LoadTexture($"{Constants.Strings.RENDERED_TEXTURES_PATH}{level.name}.png");
            Sprite sprite = Sprite.Create(tex, new Rect(0,0,tex.width,tex.height), new Vector2(0,0));
            visual.SetData(sprite, level, ()=>PlayerDataModel.Data.LevelIndex = level.index);
        }
    }
}

public class MapSelectionVisual : MonoBehaviour
{
    [SerializeField] private Button mapButton;
    [SerializeField] private TextMeshProUGUI mapName;
    [SerializeField] private Image mapImage;

    public void SetData(Sprite sprite, LevelModel lv, UnityAction onClick)
    {
        mapImage.sprite = sprite;
        mapName.text = $"Map {lv.index}";
        mapButton.onClick.RemoveAllListeners();
        mapButton.onClick.AddListener(onClick);
    }
}

public class LoadGameView : ScreenElement
{
    [SerializeField] private Button[] loadButtons;
    
    public override void Initialize()
    {
        base.Initialize();
        for (int i = 0; i < loadButtons.Length; i++)
        {
            if(LevelDataModel.Data.levelEntityDatas.Count >= i)
            {
                loadButtons[i].interactable = true;
                loadButtons[i].onClick.RemoveAllListeners();
                loadButtons[i].onClick.AddListener(() =>
                    PlayerDataModel.Data.LevelIndex = LevelDataModel.Data.levelEntityDatas[i].levelIndex);
                loadButtons[i].SetText($"Level {i}");
            }
            else
            {
                loadButtons[i].SetText("<Empty Save Slot>");
                loadButtons[i].interactable = false;
            }
        }
    }
}
