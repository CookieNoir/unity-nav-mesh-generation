using System.Collections.Generic;
using UnityEngine;

public class PavlidisAlgorithmPolygonDetector : IPolygonDetector
{
    private List<Polygon> _polygons;

    private static Vector2Int[] _directionsLeft = new Vector2Int[4]
    {
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
    };

    private static Vector2Int[,] _directions = new Vector2Int[4, 3]
    {
        { new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(1, 1), },
        { new Vector2Int(1, 1), new Vector2Int(1, 0), new Vector2Int(1, -1),},
        { new Vector2Int(1, -1), new Vector2Int(0, -1), new Vector2Int(-1, -1),},
        { new Vector2Int(-1, -1), new Vector2Int(-1, 0), new Vector2Int(-1, 1), },
    };
    private Vector3[,] _allVertices;
    private bool[,] _obstacles;
    private bool[,] _visited;

    public List<Polygon> DetectPolygons(ObstacleLayer obstacleLayer)
    {
        _polygons = new List<Polygon>();
        _allVertices = obstacleLayer.Positions;
        _obstacles = obstacleLayer.isObstacle;
        _visited = new bool[obstacleLayer.Width, obstacleLayer.Height];

        for (int w = 0; w < obstacleLayer.Width; ++w)
        {
            for (int h = 0; h < obstacleLayer.Height; ++h)
            {
                if (!_visited[w, h])
                {
                    _visited[w, h] = true;
                    if (_obstacles[w, h])
                    {
                        int direction = 0;
                        while (direction < 4)
                        {
                            if (!_IsBlack(w + _directionsLeft[direction].x, h + _directionsLeft[direction].y)) break;
                            direction++;
                        }
                        if (direction < 4)
                        {
                            _CreatePolygon(w, h, direction);
                        }
                    }
                }
            }
        }

        return _polygons;
    }

    private bool _IsBlack(in int w, in int h)
    {
        if (w < 0 || w >= _obstacles.GetLength(0)) return false;
        if (h < 0 || h >= _obstacles.GetLength(1)) return false;
        return _obstacles[w, h];
    }

    private void _CreatePolygon(in int w, in int h, in int direction)
    {
        int currentW = w, currentH = h;
        int currentDirection = direction;
        List<Vector3> polygonVertices = new List<Vector3>();
        polygonVertices.Add(_allVertices[currentW, currentH]);
        int rotations = 0;

        void _Move(in int newW, in int newH)
        {
            currentW = newW;
            currentH = newH;
            polygonVertices.Add(_allVertices[currentW, currentH]);
            _visited[currentW, currentH] = true;
            rotations = 0;
        }

        while (true)
        {
            int leftW = currentW + _directions[currentDirection, 0].x,
                leftH = currentH + _directions[currentDirection, 0].y,
                forwardW = currentW + _directions[currentDirection, 1].x,
                forwardH = currentH + _directions[currentDirection, 1].y,
                rightW = currentW + _directions[currentDirection, 2].x,
                rightH = currentH + _directions[currentDirection, 2].y;
            if (_IsBlack(leftW, leftH))
            {
                if (leftW == w && leftH == h) break;
                else
                {
                    _Move(leftW, leftH);
                    currentDirection = (currentDirection + 3) % 4;
                }
            }
            else if (_IsBlack(forwardW, forwardH))
            {
                if (forwardW == w && forwardH == h) break;
                else
                {
                    _Move(forwardW, forwardH);
                }
            }
            else if (_IsBlack(rightW, rightH))
            {
                if (rightW == w && rightH == h) break;
                else
                {
                    _Move(rightW, rightH);
                }
            }
            else if (rotations == 3)
            {
                break;
            }
            else
            {
                currentDirection = (currentDirection + 1) % 4;
                rotations++;
            }
        }

        Polygon polygon = new Polygon(polygonVertices);
        _polygons.Add(polygon);
    }
}