using UnityEngine;
[CreateAssetMenu(fileName = "Mock NavMesh Builder", menuName = "Scriptable Objects/NavMesh Builders/Mock")]
public class MockNavMeshBuilderHandler : InstanceFitterNavMeshBuilder
{
    public override INavMeshBuilder GetInstance()
    {
        return new MockNavMeshBuilder();
    }
}