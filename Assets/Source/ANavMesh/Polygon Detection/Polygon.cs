using System.Collections.Generic;
using UnityEngine;

public class Polygon
{
    public Vector3[] Vertices { get; private set; }

    public Polygon(List<Vector3> vertices)
    {
        Vertices = vertices.ToArray();
    }
}