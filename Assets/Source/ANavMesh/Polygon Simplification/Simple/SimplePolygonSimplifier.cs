using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePolygonSimplifier : IPolygonSimplifier
{
    public List<Polygon> SimplifyPolygons(List<Polygon> polygons)
    {
        return new List<Polygon>(polygons);
    }
}