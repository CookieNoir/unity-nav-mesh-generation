using System.Collections.Generic;
using UnityEngine;

public class MockVoxelizer : IVoxelizer<float>
{
    public VoxelGrid<float> Voxelize(List<FilterRendererPair> filterRendererPairs)
    {
        return new VoxelGrid<float>(Vector3.one, Vector3Int.zero, 16, 16, 0, 0f);
    }
}