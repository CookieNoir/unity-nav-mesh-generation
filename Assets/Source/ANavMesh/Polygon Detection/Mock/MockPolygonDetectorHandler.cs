using UnityEngine;
[CreateAssetMenu(fileName = "Mock Polygon Detector", menuName = "Scriptable Objects/Polygon Detectors/Mock")]
public class MockPolygonDetectorHandler : InstanceFitterPolygonDetector
{
    public override IPolygonDetector GetInstance()
    {
        return new MockPolygonDetector();
    }
}