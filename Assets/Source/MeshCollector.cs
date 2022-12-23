using System.Collections.Generic;
using UnityEngine;

public class MeshCollector : MonoBehaviour
{
    [SerializeField] private Transform _parentObject;

    public List<FilterRendererPair> CollectMeshFilters()
    {
        List<FilterRendererPair> result = new List<FilterRendererPair>();
        _FindPairs(_parentObject, result);
        return result;
    }

    private void _FindPairs(Transform target, List<FilterRendererPair> pairList)
    {
        MeshFilter meshFilter = target.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = target.GetComponent<MeshRenderer>();
        if (meshFilter && meshRenderer) pairList.Add(new FilterRendererPair(meshFilter, meshRenderer));
        foreach (Transform child in target)
        {
            _FindPairs(child, pairList);
        }
    }
}