using UnityEngine;

public class RasterizerVoxelGridShower : MonoBehaviour
{
    [SerializeField] private Transform _gridParent;
    [SerializeField] private GameObject _voxelPrefab;

    public void ShowVoxelGrid(VoxelGrid<float> voxelGrid)
    {
        _ClearParent();
        Vector3 voxelSize = voxelGrid.GridProperties.VoxelSize;
        for (int w = 0; w < voxelGrid.Width; ++w)
        {
            for (int h = 0; h < voxelGrid.Height; ++h)
            {
                Voxel<float> voxel = voxelGrid.GetVoxel(w, h, 0);
                GameObject newVoxel = Instantiate(_voxelPrefab, _gridParent);
                newVoxel.transform.position = voxel.Position;
                newVoxel.transform.localScale = voxelSize;
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