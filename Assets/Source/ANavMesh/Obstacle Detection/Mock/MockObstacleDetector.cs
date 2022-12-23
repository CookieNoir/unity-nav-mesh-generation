using UnityEngine;

public class MockObstacleDetector : IObstacleDetector<float>
{
    public ObstacleLayer[] DetectObstacles(VoxelGrid<float> voxelGrid)
    {
        Vector3[,] positions = new Vector3[voxelGrid.Width, voxelGrid.Height];
        for (int w = 0; w < voxelGrid.Width; ++w)
        {
            for (int h = 0; h < voxelGrid.Height; ++h)
            {
                positions[w, h] = voxelGrid.GetVoxel(w, h, 0).Position;
            }
        }
        return new ObstacleLayer[] { new ObstacleLayer(positions) };
    }
}