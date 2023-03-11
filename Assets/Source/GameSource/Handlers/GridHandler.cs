using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    [SerializeField] private GridViewModel gridView;
    private CellModel[,] grid;

    [EditorButton]
    public void CreateGrid(int x, int y)
    {
        grid = GridHelper.CreateGrid(x, y);
        gridView.InitializeGridView(grid);
    }

    [EditorButton]
    public void CreateGridWithAutomaton(int x, int y)
    {
        var automaton = CellularAutomatonCreator.CreateAutomata(x, y);
        grid = GridHelper.CreateGrid(automaton);
        gridView.InitializeGridView(grid);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (CellModel cellModel in grid)
        {
            var pos = GridHelper.GetCellWorldPosition(cellModel.Position.x, cellModel.Position.y);
            Gizmos.DrawWireSphere(pos, .05f);
        }
    }


    /*[EditorButton]
    public void ConstructionState(bool state)
    {
        if (state)
            gridView.ConstructionState();
        else
        {
            gridView.IdleState();
        }
    }*/
}