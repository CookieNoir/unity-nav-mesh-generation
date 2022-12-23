using UnityEngine;

public class ScaledMesh
{
    public Vector3[] Vertices { get; private set; }
    public int[] Triangles { get; private set; }

    public ScaledMesh(MeshFilter meshFilter)
    {
        Transform transform = meshFilter.transform;
        Mesh mesh = meshFilter.mesh;
        Vertices = mesh.vertices;
        Triangles = mesh.triangles;
        for (int i = 0; i < Vertices.Length; ++i)
        {
            Vertices[i] = transform.TransformPoint(Vertices[i]);
        }
    }
}