using System.Collections.Generic;
using UnityEngine;

public class PolygonShower : MonoBehaviour
{
    [SerializeField] private Transform _polygonParent;
    [SerializeField] private Transform _layerParentPrefab;
    [SerializeField] private GameObject _lineRendererPrefab;

    public void ShowPolygons(List<Polygon>[] layerPolygons)
    {
        foreach (List<Polygon> polygons in layerPolygons)
        {
            Transform layerParent = Instantiate(_layerParentPrefab, _polygonParent);
            foreach (Polygon polygon in polygons)
            {
                GameObject rendererObject = Instantiate(_lineRendererPrefab, layerParent);
                LineRenderer lineRenderer = rendererObject.GetComponent<LineRenderer>();
                lineRenderer.positionCount = polygon.Vertices.Length;
                lineRenderer.SetPositions(polygon.Vertices);
            }
        }
    }
}