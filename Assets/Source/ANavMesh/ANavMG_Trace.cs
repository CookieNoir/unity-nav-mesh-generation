using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANavMG_Trace<T>
{
    public VoxelGrid<T> VoxelGrid { get; private set; }
    public ObstacleLayer[] RoughLayers { get; private set; }
    public ObstacleLayer[] RefinedLayers { get; private set; }
    public List<Polygon>[] LayerRoughPolygons { get; private set; }
    public List<Polygon>[] LayerSimplifiedPolygons { get; private set; }
    public ANavMG_NavMesh[] LayerNavMeshes { get; private set; }

    public ANavMG_Trace(
        VoxelGrid<T> voxelGrid,
        ObstacleLayer[] roughLayers, 
        ObstacleLayer[] refinedLayers,
        List<Polygon>[] layerRoughPolygons,
        List<Polygon>[] layerSimplifiedPolygons,
        ANavMG_NavMesh[] layerNavMeshes) 
    {
        VoxelGrid = voxelGrid;
        RoughLayers = roughLayers;
        RefinedLayers = refinedLayers;
        LayerRoughPolygons = layerRoughPolygons;
        LayerSimplifiedPolygons = layerSimplifiedPolygons;
        LayerNavMeshes = layerNavMeshes;
    }
}