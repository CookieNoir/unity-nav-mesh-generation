using UnityEngine;

public class RasterizerTraceShower : MonoBehaviour
{
    [SerializeField] private RasterizerVoxelGridShower _voxelGridShower;
    [SerializeField] private ObstacleLayerShower _roughLayerShower;
    [SerializeField] private ObstacleLayerShower _refinedLayerShower;
    [SerializeField] private PolygonShower _roughLayerPolygonsShower;
    [SerializeField] private PolygonShower _simplifiedLayerPolygonsShower;

    public void Show(ANavMG_Trace<float> trace)
    {
        _voxelGridShower.ShowVoxelGrid(trace.VoxelGrid);
        _roughLayerShower.ShowObstacleLayers(trace.RoughLayers, trace.VoxelGrid.GridProperties.VoxelSize);
        _refinedLayerShower.ShowObstacleLayers(trace.RefinedLayers, trace.VoxelGrid.GridProperties.VoxelSize);
        _roughLayerPolygonsShower.ShowPolygons(trace.LayerRoughPolygons);
        _simplifiedLayerPolygonsShower.ShowPolygons(trace.LayerSimplifiedPolygons);
    }
}