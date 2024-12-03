using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class ItemMappingItem
{
    public TileBase markerTile;
    public GameObject itemPrefab;

    public bool Match(TileBase other)
    {
        return markerTile == other;
    }
}
