using UnityEngine;


public class GridViewModel : MonoBehaviour
{
    [SerializeField] private SpriteRenderer gridRenderer;

    public void SetGridView(Texture2D tex)
    {
        gridRenderer.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
    }
}