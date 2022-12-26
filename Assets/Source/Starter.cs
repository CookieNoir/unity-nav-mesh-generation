using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private MeshCollector _meshCollector;
    [SerializeField] private ANavMGRasterizerHandler _ANavMGHandler;
    [SerializeField] private RasterizerTraceShower _traceShower;
    // [SerializeField] private DepthShower _depthShower;

    private void Start()
    {
        BuildNavMesh();
    }

    public void BuildNavMesh()
    {
        List<FilterRendererPair> meshes = _meshCollector.CollectMeshFilters();
        _ANavMGHandler.Build(meshes);
        _traceShower.Show(_ANavMGHandler.ANavMG.Trace);
        // _depthShower.ShowDepths(_ANavMGHandler.ANavMG.Trace.RefinedLayers);
    }
}