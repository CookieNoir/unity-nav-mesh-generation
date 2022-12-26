using UnityEngine;
[CreateAssetMenu(fileName = "Ramer-Douglas-Peucker Polygon Simplifier", menuName = "Scriptable Objects/Polygon Simplifiers/Ramer-Douglas-Peucker Algorithm")]
public class RDPPolygonSimplifierHandler : InstanceFitterPolygonSimplifier
{
    [SerializeField, Min(0.001f)] private float _threshold;

    public override IPolygonSimplifier GetInstance()
    {
        return new RDPPolygonSimplifier(_threshold);
    }
}