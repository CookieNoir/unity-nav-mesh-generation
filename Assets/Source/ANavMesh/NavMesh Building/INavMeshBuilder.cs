using System.Collections.Generic;

public interface INavMeshBuilder
{
    public ANavMG_NavMesh BuildNavMesh(List<Polygon> polygons);
}