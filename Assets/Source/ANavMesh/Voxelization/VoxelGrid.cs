using System.Collections.Generic;
using UnityEngine;

public class VoxelGrid<T>
{
    public GridProperties GridProperties { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    private List<Voxel<T>>[,] _voxels;

    public VoxelGrid(Vector3 voxelSize, Vector3Int offset, int width, int height, int defaultDepth, T defaultData)
    {
        GridProperties = new GridProperties(voxelSize, offset);
        Width = width;
        Height = height;
        _voxels = new List<Voxel<T>>[Width, Height];
        for (int i = 0; i < Width; ++i)
        {
            for (int j = 0; j < Height; ++j)
            {
                _voxels[i, j] = new List<Voxel<T>>();
                _voxels[i, j].Add(new Voxel<T>(_GetVoxelPosition(i, j, defaultDepth), defaultDepth, defaultData));
            }
        }
    }

    private Vector3 _GetVoxelPosition(in int width, in int height, in int depth)
    {
        return new Vector3(
            GridProperties.VoxelSize.x * (width + GridProperties.Offset.x) + GridProperties.HalfVoxelSize.x,
            GridProperties.VoxelSize.y * (depth + GridProperties.Offset.y) + GridProperties.HalfVoxelSize.y,
            GridProperties.VoxelSize.z * (height + GridProperties.Offset.z) + GridProperties.HalfVoxelSize.z
            );
    }

    public void AddVoxel(in int width, in int height, in int depth, in T data)
    {
        _voxels[width, height].Add(new Voxel<T>(_GetVoxelPosition(width, height, depth), depth, data));
    }

    public void SetVoxelDepthAndData(in int width, in int height, in int index, in int newDepth, in T data)
    {
        Voxel<T> voxel = new Voxel<T>(_GetVoxelPosition(width, height, newDepth), newDepth, data);
        _voxels[width, height][index] = voxel;
    }

    public Voxel<T> GetVoxel(in int width, in int height, in int index)
    {
        return _voxels[width, height][index];
    }

    ~VoxelGrid()
    {
        for (int i = 0; i < Width; ++i)
        {
            for (int j = 0; j < Height; ++j)
            {
                _voxels[i, j].Clear();
            }
        }
    }
}