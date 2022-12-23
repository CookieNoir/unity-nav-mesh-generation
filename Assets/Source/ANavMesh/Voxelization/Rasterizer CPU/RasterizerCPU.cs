using System.Collections.Generic;
using UnityEngine;

public class RasterizerCPU : IVoxelizer<float>
{
    public Vector3 VoxelSize { get; private set; }
    public int DefaultDepth { get; private set; }
    public float DefaultValue { get; private set; }

    public RasterizerCPU(Vector3 voxelSize, int defaultDepth)
    {
        VoxelSize = voxelSize;
        DefaultDepth = defaultDepth;
        DefaultValue = 1f; // косинус угла, образованного нормалью треугольника и вектором forward
    }

    public VoxelGrid<float> Voxelize(List<FilterRendererPair> filterRendererPairs)
    {
        VoxelGrid<float> voxelGrid = _CreateGrid(filterRendererPairs);
        _FillGrid(voxelGrid, filterRendererPairs);
        return voxelGrid;
    }

    private VoxelGrid<float> _CreateGrid(List<FilterRendererPair> filterRendererPairs)
    {
        VoxelizationBounds voxelizationBounds = new VoxelizationBounds(filterRendererPairs);
        int offsetX = Mathf.FloorToInt(voxelizationBounds.Min.x / VoxelSize.x),
            offsetY = Mathf.FloorToInt(voxelizationBounds.Min.y / VoxelSize.y),
            offsetZ = Mathf.FloorToInt(voxelizationBounds.Min.z / VoxelSize.z),
            width = Mathf.CeilToInt(voxelizationBounds.Max.x / VoxelSize.x) - offsetX,
            height = Mathf.CeilToInt(voxelizationBounds.Max.z / VoxelSize.z) - offsetZ;
        Vector3Int offset = new Vector3Int(offsetX, offsetY, offsetZ);
        VoxelGrid<float> voxelGrid = new VoxelGrid<float>(VoxelSize, offset, width, height, DefaultDepth, DefaultValue);
        return voxelGrid;
    }

    private void _FillGrid(VoxelGrid<float> voxelGrid, List<FilterRendererPair> filterRendererPairs)
    {
        List<ScaledMesh> scaledMeshes = MeshScaler.Scale(filterRendererPairs);
        foreach (ScaledMesh mesh in scaledMeshes)
        {
            Vector3[] vertices = mesh.Vertices;
            int[] triangles = mesh.Triangles;
            int trianglesCount = triangles.Length / 3;
            int triangleIndex = -3;
            for (int i = 0; i < trianglesCount; ++i)
            {
                triangleIndex += 3;
                Vector3 v1 = vertices[triangles[triangleIndex]],
                        v2 = vertices[triangles[triangleIndex + 1]],
                        v3 = vertices[triangles[triangleIndex + 2]];
                Vector3 cross = Vector3.Cross(v2 - v1, v3 - v1);
                float cosUp = Vector3.Dot(cross.normalized, Vector3.up);
                if (cosUp <= 0f) continue;

                float minX = Mathf.Min(Mathf.Min(v1.x, v2.x), v3.x),
                      minZ = Mathf.Min(Mathf.Min(v1.z, v2.z), v3.z),
                      maxX = Mathf.Max(Mathf.Max(v1.x, v2.x), v3.x),
                      maxZ = Mathf.Max(Mathf.Max(v1.z, v2.z), v3.z);
                int minWidth = Mathf.FloorToInt(minX / VoxelSize.x) - voxelGrid.GridProperties.Offset.x,
                    maxWidth = Mathf.CeilToInt(maxX / VoxelSize.x) - voxelGrid.GridProperties.Offset.x,
                    minHeight = Mathf.FloorToInt(minZ / VoxelSize.z) - voxelGrid.GridProperties.Offset.z,
                    maxHeight = Mathf.CeilToInt(maxZ / VoxelSize.z) - voxelGrid.GridProperties.Offset.z;
                for (int w = minWidth; w < maxWidth; ++w)
                {
                    for (int h = minHeight; h < maxHeight; ++h)
                    {
                        Voxel<float> voxel = voxelGrid.GetVoxel(w, h, 0);
                        if (_IsPointInTriangle(voxel.Position, v1, v2, v3))
                        {
                            Vector3 barycentric = BarycentricCoordinates.GetCoordinates2D(voxel.Position, v1, v2, v3);
                            Vector3 projected = BarycentricCoordinates.Project3D(barycentric, v1, v2, v3);
                            int depth = Mathf.FloorToInt(projected.y / VoxelSize.y) - voxelGrid.GridProperties.Offset.y;
                            if (depth > voxelGrid.GetVoxel(w, h, 0).Depth)
                                voxelGrid.SetVoxelDepthAndData(w, h, 0, depth, cosUp);
                        }
                    }
                }
            }
        }
    }

    private bool _IsPointInTriangle(in Vector3 point, in Vector3 v1, in Vector3 v2, in Vector3 v3)
    {
        float s1 = _Sign(point, v1, v2),
              s2 = _Sign(point, v2, v3),
              s3 = _Sign(point, v3, v1);
        bool has_neg = (s1 < 0) || (s2 < 0) || (s3 < 0),
             has_pos = (s1 > 0) || (s2 > 0) || (s3 > 0);
        return !(has_neg && has_pos);
    }

    private float _Sign(in Vector3 p1, in Vector3 p2, in Vector3 p3)
    {
        return (p1.x - p3.x) * (p2.z - p3.z) - (p2.x - p3.x) * (p1.z - p3.z);
    }
}