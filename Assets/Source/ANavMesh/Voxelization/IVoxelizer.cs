using System.Collections.Generic;
using UnityEngine;

public interface IVoxelizer<T>
{
    public VoxelGrid<T> Voxelize(List<FilterRendererPair> filterRendererPairs);
}