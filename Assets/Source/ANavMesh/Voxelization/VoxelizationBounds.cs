using System.Collections.Generic;
using UnityEngine;

public class VoxelizationBounds
{
    public Vector3 Min { get; private set; }
    public Vector3 Max { get; private set; }

    public VoxelizationBounds(List<FilterRendererPair> filterRendererPairs)
    {
        float minX = float.PositiveInfinity,
              minY = float.PositiveInfinity,
              minZ = float.PositiveInfinity,
              maxX = float.NegativeInfinity,
              maxY = float.NegativeInfinity,
              maxZ = float.NegativeInfinity;
        foreach (FilterRendererPair filterRendererPair in filterRendererPairs)
        {
            MeshRenderer renderer = filterRendererPair.MeshRenderer;
            Bounds bounds = renderer.bounds;
            Vector3 min = bounds.min;
            if (min.x < minX) minX = min.x;
            if (min.y < minY) minY = min.y;
            if (min.z < minZ) minZ = min.z;
            Vector3 max = bounds.max;
            if (max.x > maxX) maxX = max.x;
            if (max.y > maxY) maxY = max.x;
            if (max.z > maxZ) maxZ = max.x;
        }
        Min = new Vector3(minX, minY, minZ);
        Max = new Vector3(maxX, maxY, maxZ);
    }
}