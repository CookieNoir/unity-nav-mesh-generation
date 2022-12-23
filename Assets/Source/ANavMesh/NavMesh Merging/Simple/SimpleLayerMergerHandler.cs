using UnityEngine;
[CreateAssetMenu(fileName = "Simple Layer Merger", menuName = "Scriptable Objects/Layer Mergers/Simple")]
public class SimpleLayerMergerHandler : InstanceFitterLayerMerger
{
    public override ILayerMerger GetInstance()
    {
        return new SimpleLayerMerger();
    }
}