using System.Collections.Generic;

public interface IPolygonDetector
{
    public List<Polygon> DetectPolygons(ObstacleLayer obstacleLayer);
}