using UnityEngine;
[CreateAssetMenu(fileName = "Simple Polygon Simplifier", menuName = "Scriptable Objects/Polygon Simplifiers/Simple")]
public class SimplePolygonSimplifierHandler : InstanceFitterPolygonSimplifier
{
    public override IPolygonSimplifier GetInstance()
    {
        return new SimplePolygonSimplifier();
    }
}