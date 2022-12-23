using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private MeshCollector _meshCollector;
    [SerializeField] private ANavMGRasterizerHandler _ANavMGHandler;
    [SerializeField] private RasterizerTraceShower _traceShower;

    private void Start()
    {
        BuildNavMesh();
    }

    public void BuildNavMesh()
    {
        List<FilterRendererPair> meshes = _meshCollector.CollectMeshFilters();
        _ANavMGHandler.Build(meshes);
        _traceShower.Show(_ANavMGHandler.ANavMG.Trace);
    }
}