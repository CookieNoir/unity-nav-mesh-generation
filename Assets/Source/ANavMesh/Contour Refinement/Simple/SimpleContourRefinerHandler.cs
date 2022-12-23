using UnityEngine;
[CreateAssetMenu(fileName = "Simple Contour Refiner", menuName = "Scriptable Objects/Contour Refiners/Simple")]
public class SimpleContourRefinerHandler : InstanceFitterContourRefiner
{
    public override IContourRefiner GetInstance()
    {
        return new SimpleContourRefiner();
    }
}