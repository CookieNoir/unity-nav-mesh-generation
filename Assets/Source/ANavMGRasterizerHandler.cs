using System.Collections.Generic;
using UnityEngine;

public class ANavMGRasterizerHandler : MonoBehaviour
{
    [SerializeField] private InstanceFitterVoxelizerFloat _voxelizer;
    [SerializeField] private InstanceFitterObstacleDetectorFloat _obstacleDetector;
    [SerializeField] private InstanceFitterContourRefiner _contourRefiner;
    [SerializeField] private InstanceFitterPolygonDetector _polygonDetector;
    [SerializeField] private InstanceFitterPolygonSimplifier _polygonSimplifier;
    [SerializeField] private InstanceFitterNavMeshBuilder _navMeshBuilder;
    [SerializeField] private InstanceFitterLayerMerger _layerMerger;
    public ANavMG<float> ANavMG { get; private set; }

    public void Build(List<FilterRendererPair> filterRendererPairs)
    {
        ANavMG = new ANavMG<float>(
            filterRendererPairs,
            _voxelizer.GetInstance(),
            _obstacleDetector.GetInstance(),
            _contourRefiner.GetInstance(),
            _polygonDetector.GetInstance(),
            _polygonSimplifier.GetInstance(),
            _navMeshBuilder.GetInstance(),
            _layerMerger.GetInstance()
            );
    }
}