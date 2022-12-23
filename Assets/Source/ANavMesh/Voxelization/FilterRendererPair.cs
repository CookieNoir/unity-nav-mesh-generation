using UnityEngine;

public struct FilterRendererPair
{
    public MeshFilter MeshFilter { get; private set; }
    public MeshRenderer MeshRenderer { get; private set; }

    public FilterRendererPair(MeshFilter meshFilter, MeshRenderer meshRenderer)
    {
        MeshFilter = meshFilter;
        MeshRenderer = meshRenderer;
    }
}