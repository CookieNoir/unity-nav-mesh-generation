using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthCounter
{
    private int _width;
    private int _height;
    private bool[,] _obstacles;
    int[,,] _layerValues;

    public int[,] GetDepths(ObstacleLayer obstacleLayer)
    {
        _width = obstacleLayer.Width;
        _height = obstacleLayer.Height;
        _obstacles = obstacleLayer.isObstacle;
        _layerValues = new int[4, _width, _height];

        for (int w = 0; w < _width; ++w)
        {
            for (int h = 0; h < _height; ++h)
            {
                if (IsObstacle(w - 1, h) == IsObstacle(w, h - 1))
                {
                    if (IsObstacle(w - 1, h) != IsObstacle(w, h))
                    {
                        _layerValues[0, w, h] = Math.Min(GetValue(0, w - 1, h), GetValue(0, w, h - 1)) + 1;
                    }
                    else
                    {
                        _layerValues[0, w, h] = Math.Min(GetValue(0, w - 1, h), GetValue(0, w, h - 1));
                    }
                }
                else
                {
                    if (IsObstacle(w - 1, h) != IsObstacle(w, h))
                    {
                        _layerValues[0, w, h] = GetValue(0, w, h - 1);
                    }
                    else
                    {
                        _layerValues[0, w, h] = GetValue(0, w - 1, h);
                    }
                }
            }
        }

        for (int h = _height - 1; h >= 0; --h)
        {
            for (int w = 0; w < _width; ++w)
            {
                if (IsObstacle(w - 1, h) == IsObstacle(w, h + 1))
                {
                    if (IsObstacle(w - 1, h) != IsObstacle(w, h))
                    {
                        _layerValues[1, w, h] = Math.Min(GetValue(1, w - 1, h), GetValue(1, w, h + 1)) + 1;
                    }
                    else
                    {
                        _layerValues[1, w, h] = Math.Min(GetValue(1, w - 1, h), GetValue(1, w, h + 1));
                    }
                }
                else 
                {
                    if (IsObstacle(w - 1, h) != IsObstacle(w, h))
                    {
                        _layerValues[1, w, h] = GetValue(1, w, h + 1);
                    }
                    else
                    {
                        _layerValues[1, w, h] = GetValue(1, w - 1, h);
                    }
                }
            }
        }

        for (int w = _width - 1; w >= 0; --w)
        {
            for (int h = _height - 1; h >= 0; --h)
            {
                if (IsObstacle(w + 1, h) == IsObstacle(w, h + 1))
                {
                    if (IsObstacle(w + 1, h) != IsObstacle(w, h))
                    {
                        _layerValues[2, w, h] = Math.Min(GetValue(2, w + 1, h), GetValue(2, w, h + 1)) + 1;
                    }
                    else
                    {
                        _layerValues[2, w, h] = Math.Min(GetValue(2, w + 1, h), GetValue(2, w, h + 1));
                    }
                }
                else
                {
                    if (IsObstacle(w + 1, h) != IsObstacle(w, h))
                    {
                        _layerValues[2, w, h] = GetValue(2, w, h + 1);
                    }
                    else
                    {
                        _layerValues[2, w, h] = GetValue(2, w + 1, h);
                    }
                }
            }
        }

        for (int h = 0; h < _height; ++h)
        {
            for (int w = _width - 1; w >= 0; --w)
            {
                if (IsObstacle(w + 1, h) == IsObstacle(w, h - 1))
                {
                    if (IsObstacle(w + 1, h) != IsObstacle(w, h))
                    {
                        _layerValues[3, w, h] = Math.Min(GetValue(3, w + 1, h), GetValue(3, w, h - 1)) + 1;
                    }
                    else
                    {
                        _layerValues[3, w, h] = Math.Min(GetValue(3, w + 1, h), GetValue(3, w, h - 1));
                    }
                }
                else
                {
                    if (IsObstacle(w + 1, h) != IsObstacle(w, h))
                    {
                        _layerValues[3, w, h] = GetValue(3, w, h - 1);
                    }
                    else
                    {
                        _layerValues[3, w, h] = GetValue(3, w + 1, h);
                    }
                }
            }
        }

        int[,] result = new int[_width, _height];
        for (int w = 0; w < _width; ++w)
        {
            for (int h = 0; h < _height; ++h)
            {
                int min = _layerValues[0, w, h];
                for (int i = 1; i < 4; ++i)
                {
                    if (_layerValues[i, w, h] < min) min = _layerValues[i, w, h];
                }
                result[w, h] = min;
            }
        }
        return result;
    }

    private bool IsObstacle(in int w, in int h)
    {
        if (w < 0 || w >= _width) return false;
        if (h < 0 || h >= _height) return false;
        return _obstacles[w, h];
    }

    private int GetValue(in int layer, in int w, in int h)
    {
        if (w < 0 || w >= _width) return 0;
        if (h < 0 || h >= _height) return 0;
        return _layerValues[layer, w, h];
    }
}