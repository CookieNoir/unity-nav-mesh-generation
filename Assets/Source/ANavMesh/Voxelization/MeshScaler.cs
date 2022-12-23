using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshScaler
{
    public static List<ScaledMesh> Scale(List<FilterRendererPair> filterRendererPairs)
    {
        List<ScaledMesh> scaledMeshes = new List<ScaledMesh>();
        foreach (FilterRendererPair filterRendererPair in filterRendererPairs)
        {
            ScaledMesh scaledMesh = new ScaledMesh(filterRendererPair.MeshFilter);
            scaledMeshes.Add(scaledMesh);
        }
        return scaledMeshes;
    }
}