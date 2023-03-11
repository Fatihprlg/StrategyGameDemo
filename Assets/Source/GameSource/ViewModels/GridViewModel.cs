using UnityEngine;


public class GridViewModel : MonoBehaviour
{
    [SerializeField] private SpriteRenderer gridView;
    [SerializeField] private SpriteRenderer constructionView;
    
    [SerializeField] private Texture2D WalkableRef;
    [SerializeField] private Texture2D NonWalkableRef;
    
    
    public void InitializeGridView(CellModel[,] grid)
    {
        gridView.sprite = CreateTexture(grid);
    }
    private Sprite CreateTexture(CellModel[,] grid)
    {
        int x = grid.GetLength(0);
        int y = grid.GetLength(1);
        
        Texture2D tex = new (x * Constants.Numerical.CELL_HEIGHT_AS_PIXEL, y * Constants.Numerical.CELL_HEIGHT_AS_PIXEL);
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                tex.SetPixels(i * 32, j * 32, 32, 32,
                    grid[i, j].CellType == CellTypes.Walkable && grid[i, j].isEmpty
                        ? WalkableRef.GetPixels()
                        : NonWalkableRef.GetPixels());
            }
        }

        tex.Apply();
        Sprite sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        return sp;
    }
    
    public void ConstructionState()
    {
        constructionView.SetActiveGameObject(true);
    }

    public void IdleState()
    {
        constructionView.SetActiveGameObject(false);
    }
}