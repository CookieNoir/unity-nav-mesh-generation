using UnityEngine;

public class ObstacleLayer
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public bool[,] isObstacle;
    public Vector3[,] Positions { get; private set; }

    public ObstacleLayer(Vector3[,] positions)
    {
        Width = positions.GetLength(0);
        Height = positions.GetLength(1);
        Positions = positions;
        isObstacle = new bool[Width, Height];
    }
}