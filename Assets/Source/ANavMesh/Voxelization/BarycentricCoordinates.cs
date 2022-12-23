using UnityEngine;

public static class BarycentricCoordinates
{
    public static Vector3 GetCoordinates2D(in Vector3 m, in Vector3 v1, in Vector3 v2, in Vector3 v3)
    {
        float h1left = (v2.z - v3.z) * (m.x - v3.x),
              h1right = (v3.x - v2.x) * (m.z - v3.z),
              h2left = (v3.z - v1.z) * (m.x - v3.x),
              h2right = (v1.x - v3.x) * (m.z - v3.z),
              denominator = (v2.z - v3.z) * (v1.x - v3.x) + (v3.x - v2.x) * (v1.z - v3.z);
        float h1 = (h1left + h1right) / denominator;
        float h2 = (h2left + h2right) / denominator;
        return new Vector3(h1, h2, 1f - h1 - h2);
    }

    public static Vector3 Project3D(in Vector3 barycentric, in Vector3 v1, in Vector3 v2, in Vector3 v3)
    {
        return barycentric.x * v1 + barycentric.y * v2 + barycentric.z * v3;
    }
}