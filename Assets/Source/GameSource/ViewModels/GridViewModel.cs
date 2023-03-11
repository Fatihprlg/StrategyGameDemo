using UnityEngine;


public class GridViewModel : MonoBehaviour
{
    [SerializeField] private PoolModel cellViewPool;
    [SerializeField] private SpriteRenderer gridView;
    
    [SerializeField] private Texture2D WalkableRef;
    [SerializeField] private Texture2D NonWalkableRef;
    private CellModel[,] currentGrid;
    
    public void InitializeGridView(CellModel[,] grid)
    {
        currentGrid = grid;
        int xLen = grid.GetLength(0);
        int yLen = grid.GetLength(1);
        gridView.sprite = createTexture(grid);
    }
    private Sprite createTexture(CellModel[,] grid)
    {
        int x = grid.GetLength(0);
        int y = grid.GetLength(1);
        
        Texture2D tex = new (x * 32, y * 32);
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
        SaveTexture(tex);
        Sprite sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        return sp;
    }
    private void SaveTexture(Texture2D texture)
    {
        byte[] bytes = texture.EncodeToPNG();
        var dirPath = Application.dataPath + "/RenderOutput";
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        System.IO.File.WriteAllBytes(dirPath + "/R_" + Random.Range(0, 100000) + ".png", bytes);
        Debug.Log(bytes.Length / 1024 + "Kb was saved as: " + dirPath);
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
    /*public void ConstructionState()
    {
        foreach (CellViewModel cellViewModel in cellViews)
        {
             cellViewModel.ConstructionState(true);
        }
    }

    public void IdleState()
    {
        foreach (CellViewModel cellViewModel in cellViews)
        {
            cellViewModel.ConstructionState(false);
        }
    }

    public void SetCellViewConstructionStateColor(int xPos, int yPos, bool isAvailable)
    {
        cellViews[xPos, yPos].ChangeMaskColor(isAvailable);
    }*/
}