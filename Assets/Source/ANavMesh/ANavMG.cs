using System.Collections.Generic;

public class ANavMG<T>
{
    public ANavMG_NavMesh NavMesh { get; private set; }
    public ANavMG_Trace<T> Trace { get; private set; }

    private IVoxelizer<T> _voxelizer;
    private IObstacleDetector<T> _obstacleDetector;
    private IContourRefiner _contourRefiner;
    private IPolygonDetector _polygonDetector;
    private IPolygonSimplifier _polygonSimplifier;
    private INavMeshBuilder _navMeshBuilder;
    private ILayerMerger _layerMerger;

    public ANavMG(
        List<FilterRendererPair> filterRendererPairs,
        IVoxelizer<T> voxelizer,
        IObstacleDetector<T> obstacleDetector,
        IContourRefiner contourRefiner,
        IPolygonDetector polygonDetector,
        IPolygonSimplifier polygonSimplifier,
        INavMeshBuilder navMeshBuilder,
        ILayerMerger layerMerger)
    {
        _voxelizer = voxelizer;
        _obstacleDetector = obstacleDetector;
        _contourRefiner = contourRefiner;
        _polygonDetector = polygonDetector;
        _polygonSimplifier = polygonSimplifier;
        _navMeshBuilder = navMeshBuilder;
        _layerMerger = layerMerger;

        _CreateMesh(filterRendererPairs);
    }

    private void _CreateMesh(List<FilterRendererPair> filterRendererPairs)
    {
        VoxelGrid<T> voxelGrid = _voxelizer.Voxelize(filterRendererPairs);
        ObstacleLayer[] roughLayers = _obstacleDetector.DetectObstacles(voxelGrid);

        ObstacleLayer[] refinedLayers = new ObstacleLayer[roughLayers.Length];
        List<Polygon>[] layerRoughPolygons = new List<Polygon>[roughLayers.Length];
        List<Polygon>[] layerSimplifiedPolygons = new List<Polygon>[roughLayers.Length];
        ANavMG_NavMesh[] layerNavMeshes = new ANavMG_NavMesh[roughLayers.Length];
        for (int i = 0; i < roughLayers.Length; ++i)
        {
            refinedLayers[i] = _contourRefiner.Refine(roughLayers[i]);
            layerRoughPolygons[i] = _polygonDetector.DetectPolygons(refinedLayers[i]);
            layerSimplifiedPolygons[i] = _polygonSimplifier.SimplifyPolygons(layerRoughPolygons[i]);
            layerNavMeshes[i] = _navMeshBuilder.BuildNavMesh(layerSimplifiedPolygons[i]);
        }
        NavMesh = _layerMerger.MergeLayers(layerNavMeshes);

        Trace = new ANavMG_Trace<T>(voxelGrid, roughLayers, refinedLayers, layerRoughPolygons, layerSimplifiedPolygons, layerNavMeshes);
    }
}