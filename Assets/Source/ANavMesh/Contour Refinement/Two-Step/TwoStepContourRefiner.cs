public class TwoStepContourRefiner : IContourRefiner
{
    public ObstacleLayer Refine(ObstacleLayer obstacleLayer)
    {
        bool[,] obstacles0 = obstacleLayer.isObstacle;

        bool[,] obstacles1 = (bool[,])obstacles0.Clone();
        int reducedWidth = obstacleLayer.Width - 1;
        int reducedHeight = obstacleLayer.Height - 1;
        for (int w = 0; w < obstacleLayer.Width; ++w)
        {
            for (int h = 0; h < obstacleLayer.Height; ++h)
            {
                if (obstacles0[w, h])
                {
                    if (w > 0) obstacles1[w - 1, h] = true;
                    if (w < reducedWidth) obstacles1[w + 1, h] = true;
                    if (h > 0) obstacles1[w, h - 1] = true;
                    if (h < reducedHeight) obstacles1[w, h + 1] = true;
                }
            }
        }

        bool[,] obstacles2 = (bool[,])obstacles1.Clone();
        for (int w = 0; w < obstacleLayer.Width; ++w)
        {
            for (int h = 0; h < obstacleLayer.Height; ++h)
            {
                if (!obstacles1[w, h])
                {
                    int neighbors = 0;
                    if (w == 0 || obstacles1[w - 1, h]) neighbors++;
                    if (w == reducedWidth || obstacles1[w + 1, h]) neighbors++;
                    if (h == 0 || obstacles1[w, h - 1]) neighbors++;
                    if (h == reducedHeight || obstacles1[w, h + 1]) neighbors++;
                    if (neighbors >= 3) obstacles2[w, h] = true;
                }
            }
        }

        ObstacleLayer refinedLayer = new ObstacleLayer(obstacleLayer.Positions);
        refinedLayer.isObstacle = obstacles2;
        return refinedLayer;
    }
}