public class SimpleLayerMerger : ILayerMerger
{
    public ANavMG_NavMesh MergeLayers(ANavMG_NavMesh[] navMeshes)
    {
        return navMeshes[0];
    }
}