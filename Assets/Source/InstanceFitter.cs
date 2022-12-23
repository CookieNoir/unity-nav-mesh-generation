using UnityEngine;

public abstract class InstanceFitter<T> : ScriptableObject
{
    public abstract T GetInstance();
}