using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapsView : ScreenElement
{
    [SerializeField] private float spacing;
    [SerializeField] private float elementWidth;
    [SerializeField] private Object levels;
    [SerializeField] private MapSelectionVisual _mapSelectionVisualPrefab;
    [SerializeField] private RectTransform _scrollRect;
    [Dependency] private GameController _gameController;

    public override void Initialize()
    {
        this.Inject();
        RectTransform rectTransform = GetComponent<RectTransform>();
        var textures = Helpers.Other.LoadAllTextures(Constants.Strings.RENDERED_TEXTURES_PATH);
        /*var files = System.IO.Directory.GetFiles(
            $"{Application.dataPath}{Constants.Strings.RENDERED_TEXTURES_PATH}", "*.png").ToList();
        files.RemoveAt(0);
        var textures = new List<Texture2D>();
        foreach (string file in files)
        {
            var path = Helpers.String.GetRightPartOfPath(file,
                Constants.Strings.RENDERED_TEXTURES_PATH.Replace("/", ""));
            Texture2D textureOnPath = Helpers.Other.LoadTexture($"/{path}");
            textures.Add(textureOnPath);
        }*/
        
        float left = -rectTransform.rect.width * .5f;
        for (int index = 0; index < textures.Length; index++)
        {
            MapSelectionVisual visual = Instantiate(_mapSelectionVisualPrefab, _scrollRect);
            Vector2 childPos = new()
            {
                y = 0,
                x = left + (index) * (elementWidth + spacing) + elementWidth /2 + 10
            };
            visual.SetAnchoredPosition(childPos);
            Texture2D tex = textures[index];
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));
            visual.SetData(sprite, index, () => _gameController.ChangeState(GameStates.Game));
        }
    }
}