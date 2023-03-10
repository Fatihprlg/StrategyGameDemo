using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    [SerializeField] private GridViewModel gridView;


    [EditorButton]
    public void CreateGrid(int x, int y)
    {
        var grid = GridHelper.CreateGrid(x, y);
        gridView.InitializeGridView(grid);
    }

    [EditorButton]
    public void CreateGridWithAutomaton(int x, int y)
    {
        var automaton = CellularAutomatonCreator.CreateAutomata(x, y);
        var grid = GridHelper.CreateGrid(automaton);
        gridView.InitializeGridView(grid);
    }

    


    [EditorButton]
    public void ConstructionState(bool state)
    {
        if (state)
            gridView.ConstructionState();
        else
        {
            gridView.IdleState();
        }
    }
}