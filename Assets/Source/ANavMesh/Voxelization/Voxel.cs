using UnityEngine;

public struct Voxel<T>
{
    public Vector3 Position { get; private set; }
    public int Depth { get; private set; }
    public T voxelData;

    public Voxel(Vector3 position, int depth, T data)
    {
        Position = position;
        Depth = depth;
        voxelData = data;
    }
}