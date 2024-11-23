using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class TrashMappingItem
{
    public TileBase markerTile;
    public GameObject[] trashPrefabs;

    public bool MatchTile(TileBase tile)
    {
        return markerTile == tile;
    }

    public bool MatchObject(TrashSubtype subtype)
    {
        var ret = false;
        foreach (var prefab in trashPrefabs)
            ret = ret || prefab.GetComponent<TrashInfo>().trashSubtype == subtype;
        return ret;
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