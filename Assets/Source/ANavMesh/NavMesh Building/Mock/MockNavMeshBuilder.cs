using System.Collections.Generic;

public class MockNavMeshBuilder : INavMeshBuilder
{
    public ANavMG_NavMesh BuildNavMesh(List<Polygon> polygons)
    {
        return new ANavMG_NavMesh();
    }
}