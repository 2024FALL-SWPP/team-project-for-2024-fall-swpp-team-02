using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TrashMapping", menuName = "Custom Storage/Trash Mapping")]
public class TrashMapping : ScriptableObject
{
    public TrashMappingItem[] items;

    public TileBase SubtypeToMarker(TrashType type)
    {
        if (type == TrashType.None) return null;

        foreach (var item in items)
            if (item.MatchObject(type)) return item.markerTile;
        return null;
    }

    public GameObject SubtypeToPrefab(TrashType type)
    {
        if (type == TrashType.None) return null;
        foreach (var item in items)
        {
            var prefab = item.GetObject(type);
            if (prefab != null) return prefab;
        }
        return null;
    }

    public GameObject MarkerToPrefab(TileBase marker)
    {
        if (marker == null) return null;

        foreach (var item in items)
            if (item.MatchTile(marker)) return item.GetRandomPrefab();
        return null;
    }
}
