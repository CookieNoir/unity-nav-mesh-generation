using UnityEngine;
[CreateAssetMenu(fileName = "Mock Obstacle Detector", menuName = "Scriptable Objects/Obstacle Detectors/Mock")]
public class MockObstacleDetectorHandler : InstanceFitterObstacleDetectorFloat
{
    public override IObstacleDetector<float> GetInstance()
    {
        return new MockObstacleDetector();
    }
}