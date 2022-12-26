using UnityEngine;
[CreateAssetMenu(fileName = "Pavlidis Algorithm Polygon Detector", menuName = "Scriptable Objects/Polygon Detectors/Theo Pavlidis Algorithm")]
public class PavlidisAlgorithmPolygonDetectorHandler : InstanceFitterPolygonDetector
{
    public override IPolygonDetector GetInstance()
    {
        return new PavlidisAlgorithmPolygonDetector();
    }
}