using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TrashMapping", menuName = "Custom Storage/Trash Mapping")]
public class TrashMapping : ScriptableObject
{
    public TrashMappingItem[] items;

    public TileBase SubtypeToMarker(TrashSubtype subtype)
    {
        if (subtype == TrashSubtype.None) return null;

        foreach (var item in items)
            if (item.MatchObject(subtype)) return item.markerTile;
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
