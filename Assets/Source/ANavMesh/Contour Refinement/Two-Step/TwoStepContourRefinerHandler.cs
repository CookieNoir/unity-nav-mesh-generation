using UnityEngine;
[CreateAssetMenu(fileName = "Two-Step Contour Refiner", menuName = "Scriptable Objects/Contour Refiners/Two-Step")]
public class TwoStepContourRefinerHandler : InstanceFitterContourRefiner
{
    public override IContourRefiner GetInstance()
    {
        return new TwoStepContourRefiner();
    }
}