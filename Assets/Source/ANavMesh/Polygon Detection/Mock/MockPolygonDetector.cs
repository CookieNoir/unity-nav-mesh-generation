using System.Collections.Generic;

public class MockPolygonDetector : IPolygonDetector
{
    public List<Polygon> DetectPolygons(ObstacleLayer obstacleLayer)
    {
        return new List<Polygon>();
    }
}