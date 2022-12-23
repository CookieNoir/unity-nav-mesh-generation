using UnityEngine;
[CreateAssetMenu(fileName = "Mock Voxelizer", menuName = "Scriptable Objects/Voxelizers/Mock")]
public class MockVoxelizerHandler : InstanceFitterVoxelizerFloat
{
    public override IVoxelizer<float> GetInstance()
    {
        return new MockVoxelizer();
    }
}