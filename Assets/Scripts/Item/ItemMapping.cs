using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "ItemMapping", menuName = "Custom Storages/Item Mapping")]
public class ItemMapping : ScriptableObject
{
    public ItemMappingItem[] ItemMappings;

    public GameObject MarkerToPrefab(TileBase marker)
    {
        foreach (var mapping in ItemMappings)
            if (mapping.Match(marker)) return mapping.itemPrefab;
        return null;
    }
}
