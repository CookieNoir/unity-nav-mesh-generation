using System.Collections.Generic;

public interface IPolygonSimplifier
{
    public List<Polygon> SimplifyPolygons(List<Polygon> polygons);
}