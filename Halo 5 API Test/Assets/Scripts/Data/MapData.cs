using UnityEngine;
using System;

[CreateAssetMenu]
public class MapData : ScriptableObject
{
    [SerializeField] private Guid guid;
    [SerializeField] private NamedArea[] areas;

    public Guid Guid
    {
        get {   return guid;    }
    }


}

[Serializable]
public struct NamedArea
{
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 size;
    [SerializeField] private float zRotation;
    [SerializeField] private String name;
}
