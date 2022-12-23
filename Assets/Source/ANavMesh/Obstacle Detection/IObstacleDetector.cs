using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacleDetector<T>
{
    public ObstacleLayer[] DetectObstacles(VoxelGrid<T> voxelGrid);
}