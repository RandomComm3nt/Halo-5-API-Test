using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class MapDataList : ScriptableObject
{
    public List<MapData> mapData;

    public MapData FindFromGuid(System.Guid guid)
    {
        for (int i = 0; i < mapData.Count; i++)
        {
            if (mapData[i].Guid == guid)
                return mapData[i];
        }

        return null;
    }
}
