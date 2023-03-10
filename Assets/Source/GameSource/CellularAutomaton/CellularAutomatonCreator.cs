using UnityEngine;

public static class CellularAutomatonCreator
{
    
    public static int[,] CreateAutomata(int _width, int _height, float _fillPercent = .65f, int stepCount = 15, int liveNeighboursRequired = 4)
    {
        
        var _cellularAutomata = new int[_width, _height];
        for (int x = 0; x < _width; ++x)
        {
            for (int y = 0; y < _height; ++y)
            {
                _cellularAutomata[x, y] = Random.value > _fillPercent ? 0 : 1;
            }
        }

        for (int i = 0; i < stepCount; i++)
        {
            Step(_width, _height, liveNeighboursRequired, _cellularAutomata);
        }
        
        return _cellularAutomata;
    }

    private static void Step(int _width, int _height, int _liveNeighboursRequired, int[,] _cellularAutomata)
    {
        int[,] caBuffer = new int[_width, _height];

        for (int x = 0; x < _width; ++x)
        {
            for (int y = 0; y < _height; ++y)
            {
                int liveCellCount = _cellularAutomata[x, y] + GetNeighbourCellCount(x, y, _width, _height, _cellularAutomata);
                caBuffer[x, y] = liveCellCount > _liveNeighboursRequired ? 1 : 0;
            }
        }

        for (int x = 0; x < _width; ++x)
        {
            for (int y = 0; y < _height; ++y)
            {
                _cellularAutomata[x, y] = caBuffer[x, y];
            }
        }
    }

    
    private static int GetNeighbourCellCount(int x, int y, int _width, int _height, int[,] _cellularAutomata)
    {
        int neighbourCellCount = 0;
        if (x > 0)
        {
            neighbourCellCount += _cellularAutomata[x - 1, y];
            if (y > 0)
            {
                neighbourCellCount += _cellularAutomata[x - 1, y - 1];
            }
        }

        if (y > 0)
        {
            neighbourCellCount += _cellularAutomata[x, y - 1];
            if (x < _width - 1)
            {
                neighbourCellCount += _cellularAutomata[x + 1, y - 1];
            }
        }

        if (x < _width - 1)
        {
            neighbourCellCount += _cellularAutomata[x + 1, y];
            if (y < _height - 1)
            {
                neighbourCellCount += _cellularAutomata[x + 1, y + 1];
            }
        }

        if (y < _height - 1)
        {
            neighbourCellCount += _cellularAutomata[x, y + 1];
            if (x > 0)
            {
                neighbourCellCount += _cellularAutomata[x - 1, y + 1];
            }
        }

        return neighbourCellCount;
    }
}