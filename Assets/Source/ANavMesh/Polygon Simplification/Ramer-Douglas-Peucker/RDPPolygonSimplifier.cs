using System.Collections.Generic;
using UnityEngine;

public class RDPPolygonSimplifier : IPolygonSimplifier
{
    private float _thresholdSquared;

    public RDPPolygonSimplifier(float threshold)
    {
        _thresholdSquared = threshold * threshold;
    }

    public List<Polygon> SimplifyPolygons(List<Polygon> polygons)
    {
        List<Polygon> simplifiedPolygons = new List<Polygon>();
        foreach (Polygon polygon in polygons)
        {
            simplifiedPolygons.Add(_RamerDouglasPeuckerSimplification(polygon.Vertices));
        }
        return simplifiedPolygons;
    }

    private Polygon _RamerDouglasPeuckerSimplification(Vector3[] vertices)
    {
        bool[] removed = new bool[vertices.Length];
        int survived = vertices.Length;

        void _RDPPartial(int start, int end)
        {
            if (end - start < 2) return;
            int endIndex = end % vertices.Length;
            float maxDistance = 0;
            int maxIndex = start + 1;
            for (int i = start + 1; i < end; ++i)
            {
                float distance = _DistanceSquared(vertices[start], vertices[endIndex], vertices[i]);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    maxIndex = i;
                }
            }
            if (maxDistance > _thresholdSquared)
            {
                _RDPPartial(start, maxIndex);
                _RDPPartial(maxIndex, end);
            }
            else
            {
                for (int i = start + 1; i < end; ++i)
                {
                    removed[i] = true;
                    survived--;
                }
            }
        }

        int midPoint = vertices.Length / 2;
        _RDPPartial(0, midPoint);
        _RDPPartial(midPoint, vertices.Length);

        List<Vector3> newVertices = new List<Vector3>(survived);
        for (int i = 0; i < removed.Length; ++i)
        {
            if (!removed[i]) newVertices.Add(vertices[i]);
        }
        Polygon newPolygon = new Polygon(newVertices);
        return newPolygon;
    }

    // http://paulbourke.net/geometry/pointlineplane/
    private float _DistanceSquared(in Vector3 start, in Vector3 end, in Vector3 point)
    {
        float a = point.x - start.x;
        float b = point.z - start.z;
        float c = end.x - start.x;
        float d = end.z - start.z;

        float u = -1f;
        float dot = a * c + b * d;
        float lengthSquared = c * c + d * d;
        if (lengthSquared > 0f) u = dot / lengthSquared;
        float projectedX, projectedZ;
        if (u < 0f)
        {
            projectedX = start.x;
            projectedZ = start.z;
        }
        else if (u > 1f)
        {
            projectedX = end.x;
            projectedZ = end.z;
        }
        else
        {
            projectedX = start.x + u * c;
            projectedZ = start.z + u * d;
        }
        float dx = point.x - projectedX;
        float dz = point.z - projectedZ;
        return dx * dx + dz * dz;
    }
}