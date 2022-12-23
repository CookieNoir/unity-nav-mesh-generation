public struct VoxelData_MultiLayer
{
    public int fill;
    public int front;

    public bool IsFrontFace() => fill > 0 && front > 0;

    public bool IsBackFace() => fill > 0 && front < 1;

    public bool IsEmmpty() => fill < 1;
}