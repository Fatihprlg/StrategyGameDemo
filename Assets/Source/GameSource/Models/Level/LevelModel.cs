using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelModel : ISerializationCallbackReceiver
{
    public int index;
    public string name;
    public int[,] grid;
    [SerializeField] private int[] gridToSerialize;
    [SerializeField] private int gridXLen;
    [SerializeField] private int gridYLen;
    public List<PoolItemDataModel> poolItems;
    public void OnBeforeSerialize()
    {
        gridXLen = grid.GetLength(0);
        gridYLen = grid.GetLength(1);
        int index = 0;
        gridToSerialize = new int[gridXLen * gridYLen];
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                gridToSerialize[index] = grid[i, j];
                index++;
            }
        }
    }

    public void OnAfterDeserialize()
    {
        grid = new int[gridXLen, gridYLen];
        int index = 0;
        for (int i = 0; i < gridXLen; i++)
        {
            for (int j = 0; j < gridYLen; j++)
            {
                grid[i, j] = gridToSerialize[index];
                index++;
            }
        }
    }
}
