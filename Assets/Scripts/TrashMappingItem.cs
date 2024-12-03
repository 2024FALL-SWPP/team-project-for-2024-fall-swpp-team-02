using System;
using System.Linq;
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

    public bool MatchObject(TrashType type)
    {
        return trashPrefabs.Any(
            prefab => prefab.GetComponent<TrashInfo>().trashType == type
            );
    }

    public GameObject GetObject(TrashType type)
    {
        foreach (var prefab in trashPrefabs)
        {
            if (prefab.GetComponent<TrashInfo>().trashType == type)
            {
                return prefab;
            }
        }
        return null;
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