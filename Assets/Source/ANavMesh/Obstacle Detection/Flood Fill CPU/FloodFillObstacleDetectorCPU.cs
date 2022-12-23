using UnityEngine;

public class FloodFillObstacleDetectorCPU : IObstacleDetector<float>
{
    private readonly float _maxAllowedDistanceSquared;
    private readonly float _minAllowedUpCos;
    private readonly float _minAllowedNeighborCos;
    private VoxelGrid<float> _voxelGrid;
    private Vector3[,] _positions;
    private bool[,] _isObstacle;

    public FloodFillObstacleDetectorCPU(float maxAllowedDistance, float minAllowedUpCos, float minAllowedNeighborCos)
    {
        _maxAllowedDistanceSquared = maxAllowedDistance * maxAllowedDistance;
        _minAllowedUpCos = minAllowedUpCos;
        _minAllowedNeighborCos = minAllowedNeighborCos;
    }

    public ObstacleLayer[] DetectObstacles(VoxelGrid<float> voxelGrid)
    {
        _voxelGrid = voxelGrid;
        _positions = new Vector3[_voxelGrid.Width, _voxelGrid.Height];
        _isObstacle = new bool[_voxelGrid.Width, _voxelGrid.Height];

        for (int h = 0; h < voxelGrid.Height; ++h)
        {
            Voxel<float> voxel = _voxelGrid.GetVoxel(0, h, 0);
            _positions[0, h] = voxel.Position;
            _isObstacle[0, h] = voxel.voxelData < _minAllowedUpCos;
        }
        float stepX = voxelGrid.GridProperties.VoxelSize.x,
              stepZ = voxelGrid.GridProperties.VoxelSize.z;
        for (int w = 1; w < voxelGrid.Width; ++w)
        {
            int prev_w = w - 1;
            for (int h = 0; h < voxelGrid.Height; ++h)
            {
                Voxel<float> voxel = _voxelGrid.GetVoxel(w, h, 0);
                _positions[w, h] = voxel.Position;
                _isObstacle[w, h] = voxel.voxelData < _minAllowedUpCos;

                if (!(_isObstacle[prev_w, h] || _isObstacle[w, h])) _VisitFromNeighbor(prev_w, h, w, h, stepX);
            }
        }

        for (int h = 1; h < voxelGrid.Height; ++h)
        {
            int prev_h = h - 1;
            for (int w = 0; w < voxelGrid.Width; ++w)
            {
                if (!(_isObstacle[w, prev_h] || _isObstacle[w, h])) _VisitFromNeighbor(w, prev_h, w, h, stepZ);
            }
        }
            ObstacleLayer obstacleLayer = new ObstacleLayer(_positions);
        obstacleLayer.isObstacle = _isObstacle;
        return new ObstacleLayer[] { obstacleLayer };
    }

    private void _VisitFromNeighbor(in int nw, in int nh, in int w, in int h, in float step)
    {
        float height = _positions[w, h].y - _positions[nw, nh].y;
        float directionSquared = step * step + height * height;
        float neighborCos = step / directionSquared;
        if (directionSquared > _maxAllowedDistanceSquared || neighborCos < _minAllowedNeighborCos)
        {
            if (height > 0f) _isObstacle[nw, nh] = true;
            else _isObstacle[w, h] = true;
        }
    }
}