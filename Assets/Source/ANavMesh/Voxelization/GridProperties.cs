using UnityEngine;

public struct GridProperties
{
    public Vector3 VoxelSize { get; private set; }
    public Vector3 HalfVoxelSize { get; private set; }
    public Vector3Int Offset { get; private set; }

    public GridProperties(Vector3 voxelSize, Vector3Int offset)
    {
        VoxelSize = voxelSize;
        HalfVoxelSize = voxelSize / 2f;
        Offset = offset;
    }
}