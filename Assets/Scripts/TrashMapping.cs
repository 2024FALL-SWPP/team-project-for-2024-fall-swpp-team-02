using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TrashMapping", menuName = "Custom Storage/Trash Mapping")]
public class TrashMapping : ScriptableObject
{
    public TrashMappingItem[] items;

    public string TrashColor(TrashType type)
    {
        if (type == TrashType.None) return "";

        foreach (var item in items)
            if (item.MatchObject(type)) return item.markerTile.name.Replace("TrashMarker", "");
        throw new System.InvalidOperationException("No mapping found for " + type);
    }

    public GameObject MarkerToPrefab(TileBase marker)
    {
        if (marker == null) return null;

        foreach (var item in items)
            if (item.MatchTile(marker)) return item.GetRandomPrefab();
        return null;
    }
}
