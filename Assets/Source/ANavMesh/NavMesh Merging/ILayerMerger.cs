using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILayerMerger
{
    public ANavMG_NavMesh MergeLayers(ANavMG_NavMesh[] navMeshes);
}