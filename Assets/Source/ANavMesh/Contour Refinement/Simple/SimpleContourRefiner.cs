public class SimpleContourRefiner : IContourRefiner
{
    public ObstacleLayer Refine(ObstacleLayer obstacleLayer)
    {
        return obstacleLayer;
    }
}