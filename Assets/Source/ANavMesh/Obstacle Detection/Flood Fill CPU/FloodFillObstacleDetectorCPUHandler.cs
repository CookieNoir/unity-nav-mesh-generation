using UnityEngine;
[CreateAssetMenu(fileName = "Flood Fill Obstacle Detector CPU", menuName = "Scriptable Objects/Obstacle Detectors/Flood Fill CPU")]
public class FloodFillObstacleDetectorCPUHandler : InstanceFitterObstacleDetectorFloat
{
    [SerializeField, Min(0.001f)] private float _maxAllowedDistance = 1f;
    [SerializeField, Range(0f, 1f)] private float _minAllowedUpCos = 0.5f;
    [SerializeField, Range(0f, 1f)] private float _minAllowedNeighborCos = 0.5f;

    public override IObstacleDetector<float> GetInstance()
    {
        return new FloodFillObstacleDetectorCPU(_maxAllowedDistance, _minAllowedUpCos, _minAllowedNeighborCos);
    }
}