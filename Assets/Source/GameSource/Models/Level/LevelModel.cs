using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelModel
{
    public int index;
    public string name;
    public CellModel[,] grid;
    public List<PoolItemDataModel> poolItems;
}
