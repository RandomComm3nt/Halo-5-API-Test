using UnityEngine;
using System;

[CreateAssetMenu]
public class MapData : ScriptableObject
{
    [SerializeField] private SerializableGuid guid;
    [SerializeField] private NamedArea[] areas;
    [SerializeField] private Sprite image;

    public SerializableGuid Guid
    {
        get {   return guid;    }
    }

    public Sprite Image
    {
        get{ return image; }
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
