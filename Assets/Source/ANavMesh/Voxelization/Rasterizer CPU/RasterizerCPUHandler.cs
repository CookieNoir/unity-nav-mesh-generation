using UnityEngine;
[CreateAssetMenu(fileName = "Rasterizer CPU", menuName = "Scriptable Objects/Voxelizers/Rasterizer CPU")]
public class RasterizerCPUHandler : InstanceFitterVoxelizerFloat
{
    [SerializeField] private Vector3 VoxelSize;
    [SerializeField] private int defaultDepth;

    public override IVoxelizer<float> GetInstance()
    {
        return new RasterizerCPU(VoxelSize, defaultDepth);
    }
}