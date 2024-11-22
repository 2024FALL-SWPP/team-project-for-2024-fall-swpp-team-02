using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class TrashMappingItem
{
    public TileBase markerTile;
    public GameObject[] trashPrefabs;

    public bool Match(TileBase other)
    {
        return markerTile == other;
    }

    /// <summary>
    /// Gets random trash prefab. Returns null if there's no prefab in the mapping.
    /// </summary>
    /// <returns>Random prefab of the mapping</returns>
    public GameObject GetRandomPrefab()
    {
        if (trashPrefabs.Length == 0) return null;
        
        var index = UnityEngine.Random.Range(0, trashPrefabs.Length);
        return trashPrefabs[index];
    }
}