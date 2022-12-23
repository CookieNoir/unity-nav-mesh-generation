using UnityEngine;

public class ObstacleLayerShower : MonoBehaviour
{
    [SerializeField] private Transform _gridParent;
    [SerializeField] private Transform _layerParentPrefab;
    [SerializeField] private GameObject _freeVoxelPrefab;
    [SerializeField] private GameObject _obstacleVoxelPrefab;

    public void ShowObstacleLayers(ObstacleLayer[] obstacleLayers, Vector3 voxelSize)
    {
        _ClearParent();
        foreach (ObstacleLayer layer in obstacleLayers)
        {
            Transform layerParent = Instantiate(_layerParentPrefab, _gridParent);
            for (int w = 0; w < layer.Width; ++w)
            {
                for (int h = 0; h < layer.Height; ++h)
                {
                    GameObject prefab = layer.isObstacle[w, h] ? _obstacleVoxelPrefab : _freeVoxelPrefab;
                    GameObject newVoxel = Instantiate(prefab, layerParent);
                    newVoxel.transform.position = layer.Positions[w, h];
                    newVoxel.transform.localScale = voxelSize;
                }
            }
        }
    }

    private void _ClearParent()
    {
        foreach (Transform child in _gridParent)
        {
            Destroy(child.gameObject);
        }
    }
}